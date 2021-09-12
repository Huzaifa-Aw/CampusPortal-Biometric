using CampusPortalBiometric.Utils;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static CampusPortalBiometric.Utils.Entities;

namespace CampusPortalBiometric.WebServices
{
     public class LoginMgmt
    {
        public bool Login(string UserID, string Password, ref UserInfo userInfo, ref string errorMsg)
        {
            try
            {
                var client = new RestClient(URLManager.GetLoginServiceURL());
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("username", UserID);
                request.AddParameter("password", Password);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content.ToString());
                    if (loginResponse.status.Equals("200"))
                    {
                        if (!loginResponse.attendance_status)
                        {
                            errorMsg = "Please start attendance first, then try to Login again.";
                            return false;
                        }
                        userInfo = JsonConvert.DeserializeObject<UserInfo>(loginResponse.user.ToString());
                        userInfo.attendance_id = loginResponse.attendance_id;
                        return true;
                    }

                    else
                    {

                        errorMsg = loginResponse.msg;
                        return false;
                    }
                }
                else
                {
                    errorMsg = "Please check your internet connectivity and try again!" ;
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }


        }

    }
}
