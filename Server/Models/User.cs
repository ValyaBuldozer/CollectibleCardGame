using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class User
    {
        public int Id { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public UserInfo UserInfo { set; get; }

        protected bool Equals(User other)
        {
            return string.Equals(Username, other.Username) && string.Equals(Password, other.Password) &&
                   Equals(UserInfo, other.UserInfo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((User) obj);
        }
    }
}
