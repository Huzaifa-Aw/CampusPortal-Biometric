using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusPortalBiometric.Utils
{
    public class URLManager
    {
        //private static readonly string BaseURL = "192.168.10.9:8080";
        private static readonly string BaseURL = "campusportal.pk";
        private static string GetServiceURL(string serviceName,string UserID)
        {
            string url = string.Format("https://{0}/api/{1}{2}", BaseURL, serviceName,((UserID.Length>0)?"/"+UserID:""));
            return url;
        }
        internal static string GetLoginServiceURL()
        {
            return GetServiceURL("account_login","");
        }
        
        internal static string GetNonBiometricStudentsServiceURL(string UserId)
        {
            return GetServiceURL("get_biometric_non_reg_students",UserId);
        }
        internal static string GetBiometricStudentsServiceURL(string UserId)
        {
            return GetServiceURL("get_biometric_reg_students",UserId);
        }
        internal static string GetRegisterStudentServiceURL()
        {
            return GetServiceURL("register_biometric_student","");
        }
       
        internal static string GetNonBiometricEmployeesServiceURL(string UserId)
        {
            return GetServiceURL("get_biometric_non_reg_employees",UserId);
        }
        internal static string GetBiometricEmployeesServiceURL(string UserId)
        {
            return GetServiceURL("get_biometric_reg_employees",UserId);
        }
        internal static string GetRegisterEmployeeServiceURL()
        {
            return GetServiceURL("register_biometric_employee","");
        }
        internal static string GetMarkEmployeeAttendanceServiceURL()
        {
            return GetServiceURL("mark_employee_attendance","");
        }
        internal static string GetMarkStudentAttendanceServiceURL()
        {
            return GetServiceURL("mark_student_attendance","");
        }

        internal static string GetImageURL(string imageEndpoint)
        {
            string url = string.Format("https://campusportal.pk/public/images/{0}", imageEndpoint);
            return url;
        
        }
    }
}
