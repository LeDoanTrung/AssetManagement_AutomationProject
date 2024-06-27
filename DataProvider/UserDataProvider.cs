using AssetManagement.DataObjects;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AssetManagement.DataProvider
{
    public static class UserDataProvider
    {
        private static readonly Random Random = new Random();

        public static User CreateRandomValidUser(bool isAdmin = true, bool isSDStaffType = true, string adminLocation = "HCM: Ho Chi Minh")
        {
            var faker = new Faker();

            DateTime today = DateTime.Today;
            DateTime startDate = today.AddYears(-18).AddDays(-1);
            DateTime endDate = today.AddYears(-100); 
            DateTime dateOfBirth = faker.Date.Between(startDate, endDate);

            DateTime joinedDate = faker.Date.Between(dateOfBirth, today);

            // Ensure joinedDate is not Saturday or Sunday
            while (joinedDate.DayOfWeek == DayOfWeek.Saturday || joinedDate.DayOfWeek == DayOfWeek.Sunday)
            {
                joinedDate = faker.Date.Between(dateOfBirth, today);
            }

            string[] genders = { "Male", "Female" };
            string gender = genders[Random.Next(genders.Length)];

            string type = isAdmin ? "Admin" : "Staff";

            string location = isAdmin ? adminLocation : "HCM: Ho Chi Minh";

            string staffType = isSDStaffType ? "SD" : "BPS";

            string firstName = GetValidName(faker.Name.FirstName());
            string lastName = GetValidName(faker.Name.LastName());


            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth.ToString("dd/MM/yyyy"),
                Gender = gender,
                JoinedDate = joinedDate.ToString("dd/MM/yyyy"),
                Type = type,
                Location = location,
                StaffType = staffType
            };

            return user;
        }

        private static string GetValidName(string name)
        {
            return Regex.Replace(name, "[^a-zA-Z]", "");
        }
    }
}
