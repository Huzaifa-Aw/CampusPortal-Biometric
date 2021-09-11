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
        private static string GetServiceURL(string serviceName)
        {
            string url = string.Format("http://{0}/api/{1}", BaseURL, serviceName);
            return url;
        }
        internal static string GetLoginServiceURL()
        {
            return GetServiceURL("account_login");
        }
        
        internal static string GetNonBiometricStudentsServiceURL()
        {
            return GetServiceURL("get_biometric_non_reg_students/1");
        }
        internal static string GetBiometricStudentsServiceURL()
        {
            return GetServiceURL("get_biometric_reg_students/1");
        }
        internal static string GetRegisterStudentServiceURL()
        {
            return GetServiceURL("register_biometric_student");
        }
       
        internal static string GetNonBiometricEmployeesServiceURL()
        {
            return GetServiceURL("get_biometric_non_reg_employees/1");
        }
        internal static string GetBiometricEmployeesServiceURL()
        {
            return GetServiceURL("get_biometric_reg_employees/1");
        }
        internal static string GetRegisterEmployeeServiceURL()
        {
            return GetServiceURL("register_biometric_employee");
        }
        internal static string GetMarkEmployeeAttendanceServiceURL()
        {
            return GetServiceURL("mark_employee_attendance");
        }
        internal static string GetMarkStudentAttendanceServiceURL()
        {
            return GetServiceURL("mark_student_attendance");
        }
    }
}
