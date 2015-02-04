using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer
{
    public class LoginController
    {

        // used for mobile
        public string getUserRole(string username)
        {            
            SA38ADTeam6Entities context = new SA38ADTeam6Entities();

            var q = (from x in context.Employees
                     where x.UserName == username
                     select x).First();
            
            return q.RoleCode;
        }

        // used for mobile
        public Employee getLoginCredentials(string username)
        {
            SA38ADTeam6Entities context = new SA38ADTeam6Entities();

            var q = (from x in context.Employees
                     where x.UserName == username
                     select x).First();
            
            return q;
            
        }




    }
}
