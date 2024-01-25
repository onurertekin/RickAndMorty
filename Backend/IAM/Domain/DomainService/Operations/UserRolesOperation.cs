using DatabaseModel;
using DatabaseModel.Entities;
using DomainService.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Operations
{
    public class UserRolesOperation
    {
        private readonly MainDbContext mainDbContext;
        public UserRolesOperation(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }
        public List<Role> GetUserRoles(int userId)
        {
            var user = mainDbContext.Users.Include(x => x.Roles).Where(x => x.Id == userId).SingleOrDefault();
            if (user == null)
                throw new BusinessException(404, "Not Found User");

            return user.Roles.ToList();
        }

        public bool CheckUserClaim(int userId, string claimCode)
        {
            var hasUserClaim = false;

            var user = mainDbContext.Users.Include(u => u.Roles).ThenInclude(r => r.Claims).Where(u => u.Id == userId).SingleOrDefault();
            if (user == null)
                return false;

            foreach (var role in user.Roles)
            {
                var exist = role.Claims.Any(x => x.Code == claimCode);
                if (exist)
                {
                    hasUserClaim = true;
                    break;
                }
            }

            return hasUserClaim;
        }

        public void Update(int userId, List<int> roles)
        {
            //  Users tablosuna rolleri dahil et. Where'de ise gönderilen Id var mı diye kontrol ediyoruz.
            var user = mainDbContext.Users.Include(x => x.Roles).Where(x => x.Id == userId).SingleOrDefault(); //Include Dahil etmek 
            if (user == null)
                throw new BusinessException(404, "Not Found User");

            #region Roles
            user.Roles.Clear();
            foreach (var roleId in roles)
            {
                var _role = mainDbContext.Roles.Where(x => x.Id == roleId).SingleOrDefault();
                if (_role == null)
                    throw new BusinessException(400, "Invalid Role");

                user.Roles.Add(_role);
            }
            #endregion
            mainDbContext.SaveChanges();
        }
    }
}
