using System;
using System.Collections.Generic;
using System.Linq;
using ComputerECommerce.Models;

namespace Services
{
    public class UserPermissionService
    {
        private IUser? user { get; set; }
        private readonly UserService userService;
        
        public UserPermissionService(UserService userService)
        {
            this.userService = userService;
        }
        
        public bool RequireOne(string perm) => RequireOne(new[] { Enum.Parse<UserPermissions>(perm) });
        public bool RequireOne(string[] perms) => RequireOne(perms.Select(perm => Enum.Parse<UserPermissions>(perm)).ToArray());
        public bool RequireOne(UserPermissions perm) => RequireOne(new[] { perm });
        public bool RequireOne(UserPermissions[] perms)
        {
            if (user == null)
            {
                throw new InvalidOperationException("User not set");
            }

            if (user.HasPermission(UserPermissions.GODMODE)) return true;

            return perms.Any(perm => user.HasPermission(perm));
        }

        public bool RequireAll(string perm) => RequireAll(new[] { Enum.Parse<UserPermissions>(perm) });
        public bool RequireAll(string[] perms) => RequireAll(perms.Select(perm => Enum.Parse<UserPermissions>(perm)).ToArray());
        public bool RequireAll(UserPermissions perm) => RequireAll(new[] { perm });
        public bool RequireAll(UserPermissions[] perms)
        {
            if (user == null)
            {
                throw new InvalidOperationException("User not set");
            }

            if (user.HasPermission(UserPermissions.GODMODE)) return true;

            return perms.All(perm => user.HasPermission(perm));
        }

        public void SetUser(IUser user) {
            this.user = user;
        }
    }
}