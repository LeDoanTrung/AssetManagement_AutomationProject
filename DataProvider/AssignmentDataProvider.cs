using AssetManagement.DataObjects;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.DataProvider
{
    public class AssignmentDataProvider
    {
        private static readonly Random Random = new Random();
        public static Assignment CreateRandomValidAssignment()
        {
            var assignment = new Assignment
            {
                AssignedDate = GenerateAssignedDate(),
                Note = GenerateNote(),
            };

            return assignment;
        }

        public static Assignment CreateRandomValidAssignmentForReturning()
        {
            var assignment = new Assignment
            {
                AssignedDate = GenerateToday(),
                Note = GenerateNote(),
            };

            return assignment;
        }

        private static string GenerateToday()
        {
            DateTime assignedDate = DateTime.Today;
            return assignedDate.ToString("dd/MM/yyyy");
        }

        private static string GenerateAssignedDate()
        {
            var faker = new Faker();
            DateTime today = DateTime.Today;
            DateTime assignedDate = faker.Date.Future(10, today);
            return assignedDate.ToString("dd/MM/yyyy");
        }

        private static string GenerateNote()
        {
            var faker = new Faker();
            string specification = faker.Lorem.Sentence(30, 10);
            return specification.Length > 50 ? specification.Substring(0, 50).Trim() : specification;
        }
    }
}
