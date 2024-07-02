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

            DateTime dateOfBirth = GenerateDateOfBirth();
            DateTime joinedDate = GenerateJoinedDate(dateOfBirth);
            string gender = GenerateGender();
            string type = GenerateType(isAdmin);
            string location = GenerateLocation(isAdmin, adminLocation);
            string staffType = GenerateStaffType(isSDStaffType);
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

        private static DateTime GenerateDateOfBirth()
        {
            var faker = new Faker();
            DateTime today = DateTime.Today;
            DateTime startDate = today.AddYears(-18).AddDays(-1);
            DateTime endDate = today.AddYears(-100);
            return faker.Date.Between(startDate, endDate);
        }

        private static DateTime GenerateJoinedDate(DateTime dateOfBirth)
        {
            var faker = new Faker();
            DateTime today = DateTime.Today;
            DateTime joinedDate = faker.Date.Between(dateOfBirth, today);

            // Ensure joinedDate is not Saturday or Sunday
            while (joinedDate.DayOfWeek == DayOfWeek.Saturday || joinedDate.DayOfWeek == DayOfWeek.Sunday)
            {
                joinedDate = faker.Date.Between(dateOfBirth, today);
            }

            return joinedDate;
        }
        private static string GenerateGender()
        {
            string[] genders = { "Male", "Female" };
            return genders[Random.Next(genders.Length)];
        }
        private static string GenerateType(bool isAdmin)
        {
            return isAdmin ? "Admin" : "Staff";
        }

        private static string GenerateLocation(bool isAdmin, string adminLocation)
        {
            return isAdmin ? adminLocation : "HCM: Ho Chi Minh";
        }
        private static string GenerateStaffType(bool isSDStaffType)
        {
            return isSDStaffType ? "SD" : "BPS";
        }

        private static string GetValidName(string name)
        {
            return Regex.Replace(name, "[^a-zA-Z]", "");
        }
    }
}
