using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface ILogin
    {
        Registration ValidateUser(string userName, string passWord);
        bool UpdatePassword(string NewPassword, int UserID);
        string GetPasswordbyUserID(int UserID);
        string GetPasswordbyQualityID(int UserID);
        string GetPasswordbyQuality_LeadID(int UserID);
        string GetPasswordbyAdminID(int UserID);
    }
}
