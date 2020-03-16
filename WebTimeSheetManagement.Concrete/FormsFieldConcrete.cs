using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace WebTimeSheetManagement.Concrete
{
    public class FormsFieldConcrete : IFormsField
    {
        /***  public bool CheckProjectCodeExists(string ProjectCode)
          {
              try
              {
                  using (var _context = new DatabaseContext())
                  {
                      var result = (from user in _context.ProjectMaster
                                    where user.ProjectCode == ProjectCode
                                    select user).Count();

                      if (result > 0)
                      {
                          return true;
                      }
                      else
                      {
                          return false;
                      }
                  }
              }
              catch (Exception)
              {
                  throw;
              }
          }
          ***/

        public List<FormsFieldMaster> ListofFormsField(int FormsCode)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    // IEnumerable<Formfields> Formfields = new List<Formfields>();
                    var Formfields = (from a in _context.AuditedForms.AsEnumerable()
                                      join b in _context.FormsFieldMaster on a.FieldID equals b.FieldID
                                      where a.FormsCode == FormsCode
                                      select b).ToList();



                    return Formfields;

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool CheckFormsFieldNameExists(string Questions)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from user in _context.FormsFieldMaster
                                  where user.Questions == Questions
                                  select user).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<FormsFieldMaster> GetListoffields(int FormsCode)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var listoffields = (from a in _context.AuditedForms.AsEnumerable()
                                        join b in _context.FormsFieldMaster on a.FieldID equals b.FieldID
                                        where a.FormsCode == FormsCode
                                        select b).ToList();




                    return listoffields;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<FormsFieldMaster> GetListofFormsFields()
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var listofFormsField = (from project in _context.FormsFieldMaster
                                          select project).ToList();
                    return listofFormsField;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int SaveFormsField(FormsFieldMaster FormsFieldMaster)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.FormsFieldMaster.Add(FormsFieldMaster);
                    return _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<FormsFieldMaster> ShowFormsField(string sortColumn, string sortColumnDir, string Search)
        {
            var _context = new DatabaseContext(); 

             var IQueryableFormsFieldMaster = (from FormsFieldMaster in _context.FormsFieldMaster
                                               select FormsFieldMaster);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryableFormsFieldMaster = IQueryableFormsFieldMaster.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableFormsFieldMaster = IQueryableFormsFieldMaster.Where(m => m.Questions == Search );
            }

            return IQueryableFormsFieldMaster;

        }

        /**  public bool CheckProjectIDExistsInTimesheet(int ProjectID)
          {
              try
              {
                  using (var _context = new DatabaseContext())
                  {
                      var result = (from timesheet in _context.TimeSheetDetails
                                    where timesheet.ProjectID == ProjectID
                                    select timesheet).Count();

                      if (result > 0)
                      {
                          return true;
                      }
                      else
                      {
                          return false;
                      }
                  }
              }
              catch (Exception)
              {
                  throw;
              }
          }
     
          public bool CheckProjectIDExistsInExpense(int ProjectID)
          {
              try
              {
                  using (var _context = new DatabaseContext())
                  {
                      var result = (from expense in _context.ExpenseModel
                                    where expense.ProjectID == ProjectID
                                    select expense).Count();

                      if (result > 0)
                      {
                          return true;
                      }
                      else
                      {
                          return false;
                      }
                  }
              }
              catch (Exception)
              {
                  throw;
              }
          }
       **/
        public int FormsFieldDelete(int FieldID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var FormsField = (from expense in _context.FormsFieldMaster
                                   where expense.FieldID == FieldID
                                   select expense).SingleOrDefault(); ;

                    if (FormsField != null)
                    {
                        _context.FormsFieldMaster.Remove(FormsField);
                        int resultFormsField = _context.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetTotalFormsFieldCounts()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var Count = con.Query<int>("Usp_GetFormsFieldCount", null, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
                if (Count > 0)
                {
                    return Count;
                }
                else
                {
                    return 0;
                }
            }
        }

     
    }
}
