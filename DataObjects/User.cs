﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.DataObjects
{
    public class User
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("joinedDate")]
        public string JoinedDate { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("staffType")]
        public string StaffType { get; set; }


        public static User CreateExpectedUser(User createdUser, User edittedUser)
        {
            return new User
            {
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                DateOfBirth = edittedUser.DateOfBirth,
                Gender = edittedUser.Gender,
                JoinedDate = edittedUser.JoinedDate,
                Type = edittedUser.Type,
                Location = createdUser.Location,
                StaffType = createdUser.StaffType
            };
        }

    }
}