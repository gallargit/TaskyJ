using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskyJ.DataRepo;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Services;

namespace TaskyJ.Business
{
    public static class TaskyJManager
    {
        static TaskyJManager() { }

        public static void SetRepoTask(IRepositoryBase<DBTaskJ> repo)
        {
            TaskyJService.repoTask = repo;
        }

        public static void SetRepoCat(IRepositoryBase<DBCategoryJ> repo)
        {
            TaskyJService.repoCat = repo;
        }

        public static void SetRepoUsr(IRepositoryBase<DBUserJ> repo)
        {
            TaskyJService.repoUsr = repo;
        }

        public static void PushTask(DBTaskJ t, DBSessionJ session)
        {
            t.CheckFinishTask();
            t.IDUser = session.IDUser;
            TaskyJService.Update(t);
        }

        public static void PushTasks(IEnumerable<DBTaskJ> lst, DBSessionJ session)
        {
            lst.ToList().ForEach(t => PushTask(t, session));
        }

        public static DBSessionJ GetDBSessionJ(string refreshToken)
        {
            return ValidSessions.FirstOrDefault(s => s.RefreshToken.ToString() == refreshToken);
        }

        public static IEnumerable<DBTaskJ> RetrieveTasks(DBSessionJ session)
        {
            var tmpList = new List<DBTaskJ>();
            DBUserJ taskowner = null;
            foreach (var item in TaskyJService.GetAllTasks(session.IDUser).Where(t => t.IDUser == session.IDUser))
            {
                if (item.IDUser.HasValue && item.IDUser > 0)
                {
                    if (taskowner == null)
                        taskowner = TaskyJService.GetUserById(item.IDUser.Value);
                    item.User = taskowner;
                }
                tmpList.Add(item);
            }
            return tmpList.AsEnumerable();
        }

        public static IEnumerable<DBTaskJ> RetrieveDeletedTasks(DBSessionJ session)
        {
            return TaskyJService.GetAllDeletedTasks(session.IDUser);
        }

        //TODO: use session
        public static DBTaskJ GetTaskById(int id)
        {
            var t = TaskyJService.GetById(id);
            if (t != null)
                if (t.IDCategory != null)
                    t.Category = GetCategoryById(t.IDCategory.Value);
            return t;
        }

        //TODO: set session
        public static bool RemoveTask(int id)
        {
            return TaskyJService.SetDeleted(GetTaskById(id));
        }

        public static bool RestoreTask(int id)
        {
            return TaskyJService.SetUndeleted(GetTaskById(id));
        }

        public static bool RemoveTask(DBTaskJ t)
        {
            return RemoveTask(t.ID);
        }

        public static bool ObliterateTask(DBTaskJ t)
        {
            return TaskyJService.Remove(t);
        }

        public static bool ObliterateTask(int id)
        {
            return RemoveTask(new DBTaskJ { ID = id });
        }

        public static bool ObliterateDeletedTasks(DBSessionJ session)
        {
            RetrieveDeletedTasks(session).ToList().ForEach(t => ObliterateTask(t));
            return true;
        }

        public static IEnumerable<DBCategoryJ> RetrieveCategories()
        {
            return TaskyJService.GetAllCategories();
        }

        public static DBCategoryJ GetCategoryById(int id)
        {
            return TaskyJService.GetCategoryById(id);
        }

        public static DBUserJ GetUserById(int id)
        {
            return TaskyJService.GetUserById(id);
        }

        public static DBUserJ GetUserByName(string userName)
        {
            return TaskyJService.GetAllUsers().SingleOrDefault(u => u.UserName == userName);
        }

        public static IEnumerable<DBUserJ> RetrieveUsers()
        {
            return TaskyJService.GetAllUsers();
        }

        public static void PushCategory(DBCategoryJ c)
        {
            TaskyJService.Update(c);
        }

        public static void PushUser(DBUserJ u)
        {
            TaskyJService.Update(u);
        }

        private static ConcurrentBag<DBSessionJ> ValidSessions = new ConcurrentBag<DBSessionJ>();

        public static DBSessionJ Authenticate(string userName, string password, string ipAddress)
        {
            var dbuser = GetUserByName(userName);
            if (dbuser != null)
            {
                var user = new DBUserJ(dbuser);
                user.Password = password;
                return Authenticate(user, ipAddress);
            }
            return null;
        }

        public static DBSessionJ Authenticate(DBUserJ user, string ipAddress)
        {
            if (user != null && !string.IsNullOrEmpty(user.UserName))
            {
                var dbuser = TaskyJService.GetAllUsers().FirstOrDefault(u => u.UserName == user.UserName);
                if (dbuser != null)
                {
                    //allow plain and encrypted passwords
                    if (dbuser.Password == user.Password || Globals.Data.Helpers.JPasswordHasher.CheckHashJ(user.Password, dbuser.Password, Globals.Data.Helpers.JPasswordHasher.Salt))
                    {
                        var session = new DBSessionJ
                        {
                            ID = 1 + (ValidSessions.Count == 0 ? 0 : ValidSessions.Max(c => c.ID)),
                            IDUser = dbuser.ID,
                            IpAddress = ipAddress,
                            JwtToken = GenerateJwtToken(user.UserName),
                            RefreshToken = GenerateRefreshToken(ipAddress),
                            CreateDate = DateTime.UtcNow,
                            UserName = user.UserName,
                        };

                        //sessions are stored in memory only, they should be stored in database instead
                        ValidSessions.Add(session);
                        return session;
                    }
                }
            }
            return null;
        }

        public static DBRefreshTokenJ RefreshToken(string token, string ipAddress)
        {
            var session = ValidSessions.SingleOrDefault(u => u.RefreshToken.ToString() == token && u.RefreshToken.IsActive);

            // return null if no user found with token
            if (session == null)
                return null;

            var oldToken = session.RefreshToken;

            // replace old refresh token with a new one and save
            var newToken = session.getRefreshToken(ipAddress);
            oldToken.Revoked = DateTime.UtcNow;
            oldToken.RevokedByIp = ipAddress;
            oldToken.ReplacedByToken = newToken.Token;
            session.RefreshToken = newToken;

            // generate new jwt
            session.JwtToken = GenerateJwtToken(session.UserName);

            return newToken;
        }

        public static bool RevokeToken(string token, string ipAddress)
        {
            var found = false;
            foreach (var session in ValidSessions.Where(u => u.RefreshToken.ToString() == token && u.RefreshToken.IsActive))
            {
                found = true;

                session.RefreshToken.Revoked = DateTime.UtcNow;
                session.RefreshToken.RevokedByIp = ipAddress;
            }
            return found;
        }

        private static DBRefreshTokenJ GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new DBRefreshTokenJ
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        private static string GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("appSettings.Secret");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static bool IsAlive()
        {
            try
            {
                return TaskyJService.GetAllUsers().Count() >= 0;
            }
            catch { }
            return false;
        }

        public static void ClearAllCaches()
        {
            TaskyJService.ClearAllCaches();
        }
    }
}
