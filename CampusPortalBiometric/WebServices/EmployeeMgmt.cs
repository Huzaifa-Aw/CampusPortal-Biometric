﻿using CampusPortalBiometric.Utils;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CampusPortalBiometric.Utils.Entities;

namespace CampusPortalBiometric.WebServices
{
    class EmployeeMgmt
    {
        public List<Employee> GetBiometricStudents(string userToken,string SchoolID)
        {
            var client = new RestClient(URLManager.GetBiometricEmployeesServiceURL(SchoolID));
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("token", userToken);
            IRestResponse response = client.Execute(request);
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(response.Content.ToString());
            return employees;
        }
        public List<Employee> GetNonBiometricStudents(string userToken, string SchoolID)
        {
            var client = new RestClient(URLManager.GetNonBiometricEmployeesServiceURL(SchoolID));
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("token", userToken);
            IRestResponse response = client.Execute(request);
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(response.Content.ToString());
            return employees;
        }

        public void RegisterorUpdateEmployeeFPrint(string ID, string XMLPrint, string userToker)
        {
            var client = new RestClient(URLManager.GetRegisterEmployeeServiceURL());
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("id", ID);
            request.AddParameter("fingerprint", XMLPrint);
            request.AddParameter("token", userToker);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Fingerprint not Saved.\n Please check internet connectivity and try again!");
        }
        public void MarkAttendance(string Id, string AId, string Token, string type)
        {
            var client = new RestClient(URLManager.GetMarkEmployeeAttendanceServiceURL());
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("token", Token);
            request.AddParameter("atid", AId);
            request.AddParameter("employee_id", Id);
            request.AddParameter("status", "Present");
            request.AddParameter("type", type);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Attendance not marked.\n Please check internet connectivity and try again!");
        }
    }
}
