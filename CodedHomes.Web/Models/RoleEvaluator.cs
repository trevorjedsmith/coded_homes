using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CodedHomes.Web.Models
{
    public class RoleEvaluator
    {
        public bool CanEdit
        {
            get
            {
                return Roles.IsUserInRole("admin") ||
                       Roles.IsUserInRole("manager") ||
                       Roles.IsUserInRole("user");
            }
        }

        public bool CanDelete
        {
            get
            {
                return Roles.IsUserInRole("admin") ||
                       Roles.IsUserInRole("manager");
            }
        }
    }
}