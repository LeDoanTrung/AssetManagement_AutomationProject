using AssetManagement.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Updater
{
    public static class UserUpdater
    {
        public static User CreateExpectedUser(User createdUser, User editedUser)
        {
            return new User
            {
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                DateOfBirth = editedUser.DateOfBirth,
                Gender = editedUser.Gender,
                JoinedDate = editedUser.JoinedDate,
                Type = editedUser.Type,
                Location = createdUser.Location,
                StaffType = createdUser.StaffType
            };
        }
    }
}
