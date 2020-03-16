using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    [Table("QA_Audit_FormsFieldMaster")]
    public class FormsFieldMaster
    {
        [Key]
        public int FieldID { get; set; }

        public string Questions { get; set; }

        [Required(ErrorMessage = "Enter Parameter contenet")]
        public Boolean Is_Selected { get; set; }

        [Required(ErrorMessage = "Enter Metric Name")]
        public string Metric { get; set; }

        [Required(ErrorMessage = "Enter Max_score")]
        public int Max_Score { get; set; }
    }
}
