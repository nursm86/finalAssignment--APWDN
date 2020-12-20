using finalAssignment__APWDN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace finalAssignment__APWDN.Repositories
{
    public class UserRepository : Repository<User>
    {
        public string Validate(string encodedString)
        {
            string decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(encodedString));
            string[] splittedText = decodedString.Split(new char[] { ':' });
            string userName = splittedText[0];
            string password = splittedText[1];

            User user = this.GetAll().Where<User>(x => x.userName == userName && x.password == password).FirstOrDefault();
            if(user == null)
            {
                return null;
            }
            else
            {
                return user.userName;
            }
        }

        public bool Validate(User u)
        {
            User user = this.GetAll().Where<User>(x => x.userName == u.userName && x.password == u.password).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int getIdbyUserName(User u)
        {
            User user = this.GetAll().Where<User>(x => x.userName == u.userName).FirstOrDefault();
            return user.UserId;
        }

        public bool UpdateUser(User user)
        {
            User oldUser = Get(user.UserId);
            if (oldUser != null)
            {
                oldUser.password = user.password;
                oldUser.Name = user.Name;
                this.context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}