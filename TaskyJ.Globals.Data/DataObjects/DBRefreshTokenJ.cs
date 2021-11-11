using System;
using System.Runtime.Serialization;

namespace TaskyJ.Globals.Data.DataObjects
{
    public class DBRefreshTokenJ : BaseEntity
    {
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public DateTime Expires { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public string CreatedByIp { get; set; }
        [DataMember]
        public DateTime? Revoked { get; set; }
        [DataMember]
        public string RevokedByIp { get; set; }
        [DataMember]
        public string ReplacedByToken { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked == null && !IsExpired;

        public override string ToString()
        {
            return Token;
        }

        public override bool Equals(BaseEntity source)
        {
            if (source is DBRefreshTokenJ sourcej)
            {
                //TODO: complete equals method
                return base.Equals(sourcej);
            }
            else
            {
                return base.Equals(source);
            }
        }
    }
}
