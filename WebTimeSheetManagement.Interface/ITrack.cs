using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface ITrack
    {
        List<TrackMaster> GetListofTrack();
       
        bool CheckTrackNameExists(string TrackName);
        int SaveTrack(TrackMaster TrackMaster);
        IQueryable<TrackMaster> ShowTrack(string sortColumn, string sortColumnDir, string Search);
    //    bool CheckProjectIDExistsInTimesheet(int ProjectID);
    //   bool CheckProjectIDExistsInExpense(int ProjectID);
        int TrackDelete(int TrackID);
        int GetTotalTrackCounts();
    }
}
