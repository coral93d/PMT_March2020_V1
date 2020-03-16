using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface IAuditForms
    {
        //  List<AdminModel> ListofAdmins();
        //    List<UserModel> ListofUser();
        //  List<QualityModel> ListofQuality();
        List<AuditedForms> GetListofForms();

     
        IQueryable<AuditedForms> ShowForms(string sortColumn, string sortColumnDir, string Search);
        // LoadFormData
        bool CheckFormNameExists(string FormName);
        bool CheckFormsCodeExists(int FormsCode);
        List<FormsFieldModel> ListofFormsField();
       // int UpdateAssigntoAdmin(string AssignToAdminID, string UserID);
      //  IQueryable<UserModel> ShowallRoles(string sortColumn, string sortColumnDir, string Search);
      //  IQueryable<QualityModel> ShowallRole(string sortColumn, string sortColumnDir, string Search);
      //  bool RemovefromUserRole(string RegistrationID);
      //  List<FormsFieldModel> GetListofFormsField();
    //    List<QualityModel> GetListofUnAssignedQuality();
        bool SaveAuditForms(AuditFormsModel AuditFormsModel);
      //  bool CheckIsUserAssignedRole(int RegistrationID);
    }
}
