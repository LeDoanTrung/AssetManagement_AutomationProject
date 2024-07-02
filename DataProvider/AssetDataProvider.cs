using AssetManagement.DataObjects;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.DataProvider
{
    public static class AssetDataProvider
    {
        private static readonly Random Random = new Random();
        public static Asset CreateRandomValidAsset(string category = "Laptop")
        {
            var asset = new Asset
            {
                Name = GenerateName(),
                Category = category,
                Specification = GenerateSpecification(),
                InstalledDate = GenerateInstalledDate(),
                State = GenerateState()
            };

            return asset;
        }

        public static Asset CreateRandomValidAssetForEditting(string category = "Laptop")
        {
            var asset = new Asset
            {
                Name = GenerateName(),
                Category = category,
                Specification = GenerateSpecification(),
                InstalledDate = GenerateInstalledDate(),
                State = GenerateStateForEditting()
            };

            return asset;
        }

        private static string GenerateName()
        {
            var faker = new Faker();
            return faker.Commerce.ProductName();
        }

        private static string GenerateSpecification()
        {
            var faker = new Faker();
            string specification = faker.Lorem.Sentence(60, 10);
            return specification.Length > 300 ? specification.Substring(0, 300) : specification;
        }

        private static string GenerateInstalledDate()
        {
            var faker = new Faker();
            DateTime today = DateTime.Today;
            DateTime installedDate = faker.Date.Past(10, today);
            return installedDate.ToString("dd/MM/yyyy");
        }

        private static string GenerateState()
        {
            string[] states = { "Available", "Not available" };
            return states[Random.Next(states.Length)];
        }

        private static string GenerateStateForEditting()
        {
            string[] states = { "Available", "Not available", "Waiting for recycling", "Recycled" };
            return states[Random.Next(states.Length)];
        }

    }
}
