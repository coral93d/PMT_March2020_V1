using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface IAssignRoles
    {
        List<AdminModel> ListofAdmins();
        List<UserModel> ListofUser();
        List<QualityModel> ListofQuality();
        int UpdateAssigntoAdmin(string AssignToAdminID, string UserID);
        IQueryable<UserModel> ShowallRoles(string sortColumn, string sortColumnDir, string Search);
        IQueryable<QualityModel> ShowallRole(string sortColumn, string sortColumnDir, string Search);
        bool RemovefromUserRole(string RegistrationID);
        List<UserModel> GetListofUnAssignedUsers(string RM_Username);
        List<UserModel> GetListofUnAssignedUsers();
        List<QualityModel> GetListofUnAssignedQuality();
        bool SaveAssignedRoles(AssignRolesModel AssignRolesModel);
        bool CheckIsUserAssignedRole(int RegistrationID);
        List<Registration> GetUserBasedOn_Admin(string Name);
    }
}
