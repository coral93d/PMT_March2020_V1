using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    [NotMapped]
    public class DisplayViewModel
    {
        public int ApprovalUser { get; set; }
        public int SubmittedCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int AppealCount { get; set; }
        public int ShowAll { get; set; }
        public int less48 { get; set; }
        public int Greater48 { get; set; }
        public int Within24 { get; set; }
    }
}
