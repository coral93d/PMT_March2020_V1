using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface IFormsField
    {
      //  List<ProjectMaster> GetListofProjects();
       
       // bool CheckProjectCodeExists(string ProjectCode);
        bool CheckFormsFieldNameExists(string Questions);
        int SaveFormsField(FormsFieldMaster FormsFieldMaster);
        IQueryable<FormsFieldMaster> ShowFormsField(string sortColumn, string sortColumnDir, string Search);
      //  bool CheckProjectIDExistsInTimesheet(int ProjectID);
      //  bool CheckProjectIDExistsInExpense(int ProjectID);
        int FormsFieldDelete(int FieldID);
        int GetTotalFormsFieldCounts();
        List<FormsFieldMaster> GetListofFormsFields();
       List<FormsFieldMaster> ListofFormsField(int FormsCode);
        List<FormsFieldMaster> GetListoffields(int FormsCode);
    }
}
