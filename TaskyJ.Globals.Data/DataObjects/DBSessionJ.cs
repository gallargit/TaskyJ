using System;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace TaskyJ.Globals.Data.DataObjects
{
    public class DBSessionJ : BaseEntity
    {
        [DataMember]
        public int IDUser { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }
        [DataMember]
        public string IpAddress { get; set; }
        [DataMember]
        public string JwtToken { get; set; }
        [DataMember]
        public DateTime Expires { get; set; }

        public DBRefreshTokenJ RefreshToken { get; set; }

        public override string ToString()
        {
            return UserName;
        }

        public override bool Equals(BaseEntity source)
        {
            if (source is DBSessionJ sourcej)
            {
                //TODO: complete equals method
                return UserName == sourcej.UserName && JwtToken == sourcej.JwtToken &&
                    CreateDate == sourcej.CreateDate && IpAddress == sourcej.IpAddress &&
                    base.Equals(source);
            }
            else
            {
                return base.Equals(source);
            }
        }

        public DBRefreshTokenJ getRefreshToken(string ipAddress)
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
    }
}
