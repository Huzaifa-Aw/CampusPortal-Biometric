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
    public class StudentMgmt
    {
        public List<Student> GetBiometricStudents(string userToken)
        {
            var client = new RestClient(URLManager.GetBiometricStudentsServiceURL());
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("token", userToken);
            IRestResponse response = client.Execute(request);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(response.Content.ToString());
            return students;
        }
        public List<Student> GetNonBiometricStudents(string userToken)
        {
            var client = new RestClient(URLManager.GetNonBiometricStudentsServiceURL());
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("token", userToken);
            IRestResponse response = client.Execute(request);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(response.Content.ToString());
            return students;
        }
        public void RegisterorUpdateStudentFPrint(string ID,string XMLPrint,string userToker) 
        {
            var client = new RestClient(URLManager.GetRegisterStudentServiceURL());
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("id", ID);
            request.AddParameter("fingerprint", XMLPrint);
            request.AddParameter("token", userToker);
            IRestResponse response = client.Execute(request);
        }

        public void MarkAttendance(string Id, string AId, string Token, string type)
        {
            var client = new RestClient(URLManager.GetMarkStudentAttendanceServiceURL());
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("token", Token);
            request.AddParameter("atid", AId);
            request.AddParameter("student_id", Id);
            request.AddParameter("status", "Present");
            request.AddParameter("type", type);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}