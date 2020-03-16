using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    public class FormViewSummaryModel
    {
        public int Forms_ID { get; set; }
        public string Questions { get; set; }
        public string Form_Name { get; set; }
        public string Metric { get; set; }
        public bool Is_Selected { get; set; }
        public int Max_Score { get; set; }
    }
}
