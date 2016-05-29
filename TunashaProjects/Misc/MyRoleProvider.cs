using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TunashaProjects.DAL;

namespace TunashaProjects.Misc
{
    public class MyRoleProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            //using (DataContext db = new DataContext())
            //{
            //    var roleId = db.Roles.SingleOrDefault(r => r.RoleName == roleNames[0]).ID;
            //    var user = db.Users.SingleOrDefault(x => x.Email == usernames[0]);
            //    user.RoleID = roleId;
            //    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            //    db.SaveChanges();
            //}
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (DataContext db = new DataContext())
            {
                var userRole = db.Users.Include("Role").SingleOrDefault(x => x.Email == username).Role.RoleName;
                string[] roles = { userRole };
                return roles;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (DataContext db = new DataContext())
            {
                var userRole = db.Users.SingleOrDefault(x => x.Email == username).Role.RoleName;
                return (userRole == roleName);
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}