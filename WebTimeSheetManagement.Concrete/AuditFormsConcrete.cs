using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;
using System.Linq.Dynamic;

namespace WebTimeSheetManagement.Concrete
{
    public class AuditFormsConcrete : IAuditForms
    {
        /**  List<AdminModel> ListofAdmins()
            {

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    con.Open();
                    try
                    {
                        var result = con.Query<AdminModel>("Usp_GetListofAdmins", null, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                        result.Insert(0, new AdminModel { Name = "----Select----", RegistrationID = "" });
                        return result;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            List<UserModel> ListofUser()
            {

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    con.Open();
                    try
                    {
                        var result = con.Query<UserModel>("Usp_GetListofUsers", null, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                        return result;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            } **/

       // List<AuditFormsModel> GetListofForms()
        public List<AuditedForms> GetListofForms()
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var listofForms = _context.AuditedForms.GroupBy(l => l.FormName)
                      .Select(g => g.FirstOrDefault())
                      .ToList();


                   
                    return listofForms;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

     

        public IQueryable<AuditedForms> ShowForms(string sortColumn, string sortColumnDir, string Search)
        {


            var _context = new DatabaseContext();

            var IQueryableForms = (from auditedForms in _context.AuditedForms
                                   
                                   select auditedForms);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryableForms = IQueryableForms.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableForms = IQueryableForms.Where(m => m.FormName == Search);
            }

            return IQueryableForms;

        }

        public  List<FormsFieldModel> ListofFormsField()
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                try
                {
                    var result = con.Query<FormsFieldModel>("Usp_GetListofFormsField", null, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }




        /**  public int UpdateAssigntoAdmin(string AssignToAdminID, string UserID)
          {
              using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
              {
                  con.Open();
                  try
                  {
                      var param = new DynamicParameters();
                      param.Add("@AssignTo", AssignToAdminID);
                      param.Add("@RegistrationID", UserID);
                      var result = con.Execute("Usp_UpdateAssignToUser", param, null, 0, System.Data.CommandType.StoredProcedure);
                      return result;
                  }
                  catch (Exception)
                  {
                      throw;
                  }
              }
          }

          public IQueryable<UserModel> ShowallRoles(string sortColumn, string sortColumnDir, string Search)
          {
              var _context = new DatabaseContext();

              var IQueryabletimesheet = (from AssignedRoles in _context.AssignedRoles
                                         join registration in _context.Registration on AssignedRoles.RegistrationID equals registration.RegistrationID
                                         join AssignedRolesAdmin in _context.Registration on AssignedRoles.AssignToAdmin equals AssignedRolesAdmin.RegistrationID
                                         select new UserModel
                                         {
                                             Name = registration.Name,
                                             AssignToAdmin = string.IsNullOrEmpty(AssignedRolesAdmin.Name) ? "*Not Assigned*" : AssignedRolesAdmin.Name.ToUpper(),
                                             RegistrationID = registration.RegistrationID

                                         });

              if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
              {
                  IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
              }
              if (!string.IsNullOrEmpty(Search))
              {
                  IQueryabletimesheet = IQueryabletimesheet.Where(m => m.Name == Search);
              }

              return IQueryabletimesheet;

          }

         
          public IQueryable<QualityModel> ShowallRole(string sortColumn, string sortColumnDir, string Search)
          {
              var _context = new DatabaseContext();

              var IQueryabletimesheet = (from AssignedRoles in _context.AssignedRoles
                                         join registration in _context.Registration on AssignedRoles.RegistrationID equals registration.RegistrationID
                                         join AssignedRolesAdmin in _context.Registration on AssignedRoles.AssignToAdmin equals AssignedRolesAdmin.RegistrationID
                                         select new QualityModel
                                         {
                                             Name = registration.Name,
                                             AssignToAdmin = string.IsNullOrEmpty(AssignedRolesAdmin.Name) ? "*Not Assigned*" : AssignedRolesAdmin.Name.ToUpper(),
                                             RegistrationID = registration.RegistrationID

                                         });

              if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
              {
                  IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
              }
              if (!string.IsNullOrEmpty(Search))
              {
                  IQueryabletimesheet = IQueryabletimesheet.Where(m => m.Name == Search);
              }

              return IQueryabletimesheet;

          }

          public bool RemovefromUserRole(string RegistrationID)
          {
              using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
              {
                  con.Open();
                  try
                  {
                      var param = new DynamicParameters();
                      param.Add("@RegistrationID", RegistrationID);
                      var result = con.Execute("Usp_UpdateUserRole", param, null, 0, System.Data.CommandType.StoredProcedure);
                      if (result > 0)
                      {
                          return true;
                      }
                      else
                      {
                          return false;
                      }
                  }
                  catch (Exception)
                  {
                      throw;
                  }
              }
          }
     
          public List<UserModel> GetListofFormsField()
          {

              using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
              {
                  con.Open();
                  try
                  {
                      var result = con.Query<UserModel>("Usp_GetListofUnAssignedUsers", null, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                      return result;
                  }
                  catch (Exception)
                  {
                      throw;
                  }
              }
          }

          public List<QualityModel> GetListofUnAssignedQuality()
          {

              using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
              {
                  con.Open();
                  try
                  {
                      var result = con.Query<QualityModel>("Usp_GetListofUnAssignedQuality", null, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                      return result;
                  }
                  catch (Exception)
                  {
                      throw;
                  }
              }
          } 
       **/

        public bool CheckFormNameExists(string FormName)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from user in _context.AuditedForms
                                  where user.FormName == FormName
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

        public bool CheckFormsCodeExists(int FormsCode)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from code in _context.AuditedForms
                                  where code.FormsCode == FormsCode
                                  select code).Count();

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

        public bool SaveAuditForms(AuditFormsModel AuditFormsModel)
        {
            bool result = false;
            using (var _context = new DatabaseContext())
            {
                try
                {
                    for (int i = 0; i < AuditFormsModel.ListofFormsField.Count(); i++)
                    {
                        if (AuditFormsModel.ListofFormsField[i].selectedFormsField)
                        {
                            AuditedForms AuditedForms = new AuditedForms
                            {

                                CreatedBy = AuditFormsModel.CreatedBy,
                                 CreatedOn = DateTime.Now,
                                FieldID = AuditFormsModel.ListofFormsField[i].FieldID,
                                FormName = AuditFormsModel.FormName,
                                FormsCode = AuditFormsModel.FormsCode,
                                FormsID = 0                            
                            };

                            _context.AuditedForms.Add(AuditedForms);
                            _context.SaveChanges();
                        }
                    }

                    

                    result = true;
                }
                catch (Exception)
                {
                    throw;
                }

                return result;
            }
        }

     /**   public bool CheckIsUserAssignedRole(int RegistrationID)
        {
            var IsassignCount = 0;
            using (var _context = new DatabaseContext())
            {
                IsassignCount = (from assignUser in _context.AssignedRoles
                 where assignUser.RegistrationID == RegistrationID
                 select assignUser).Count();
            }

            if (IsassignCount>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } **/
    }
}
