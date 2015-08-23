using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StortyBack.Data
{
    public class UserData : MainData
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public int CheckLogin(UserData lm)
        {
            var userid = db.Users.AsNoTracking().Where(x => x.name == lm.UserName && x.password == lm.Password).Select(s => s.id).FirstOrDefault();
            return userid;
        }
    }
}
