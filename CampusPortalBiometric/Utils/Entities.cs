using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusPortalBiometric.Utils
{
    public class Entities
    {
        public class UserInfo
        {
            
            [JsonProperty("shortname")]
            public string SchoolName { get; set; }
            public string token { get; set; }
            public string attendance_id { get; set; }
            [JsonProperty("logo")]
            public string SchoolLogo { get; set; }
            [JsonProperty("user")]
            public string SchoolID { get; set; }
        }
        public class LoginResponse
        {
            public string status { get; set; }
            public string msg { get; set; }
            public bool attendance_status { get; set; }
            public string attendance_id { get; set; }
            public object user { get; set; }
        }
        public class Student
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("sname")]
            public string Name { get; set; }
            [JsonProperty("fathername")]
            public string Father_Name { get; set; }
            [JsonProperty("secname")]
            public string Class { get; set; }
            [JsonProperty("bio_finger_print")]
            public string Fingerprint { get; set; }
        }
        public class Employee
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("fathername")]
            public string Father_Name { get; set; }
            [JsonProperty("designation")]
            public string Designation { get; set; }
            [JsonProperty("bio_finger_print")]
            public string Fingerprint { get; set; }
        }
    }
    
}
