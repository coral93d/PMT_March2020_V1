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
    public class TrackConcrete : ITrack
    {
       
        public bool CheckTrackNameExists(string TrackName)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from user in _context.TrackMaster
                                  where user.TrackName == TrackName
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

        public List<TrackMaster> GetListofTrack()
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var listofTrack = (from track in _context.TrackMaster
                                          select track).ToList();
                    return listofTrack;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int SaveTrack(TrackMaster TrackMaster)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.TrackMaster.Add(TrackMaster);
                    return _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

      public  IQueryable<TrackMaster> ShowTrack(string sortColumn, string sortColumnDir, string Search)
        {
            var _context = new DatabaseContext();

            var IQueryableTrack = (from trackmaster in _context.TrackMaster
                                     select trackmaster);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryableTrack = IQueryableTrack.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableTrack = IQueryableTrack.Where(m => m.TrackName == Search );
            }

            return IQueryableTrack;

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
      public  int TrackDelete(int TrackID)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var Track = (from tracks in _context.TrackMaster
                                   where tracks.TrackID == TrackID
                                   select tracks).SingleOrDefault(); ;

                    if (Track != null)
                    {
                        _context.TrackMaster.Remove(Track);
                        int resultTrack = _context.SaveChanges();
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

        public int GetTotalTrackCounts()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var Count = con.Query<int>("Usp_GetTrackCount", null, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
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
