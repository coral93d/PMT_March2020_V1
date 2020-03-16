using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    [Table("QA_Forms")]
    public class AuditedForms
    {
        [Key]
        public int FormsID { get; set; }
        public int FormsCode { get; set; }

        public string FormName { get; set; }

        public int? FieldID { get; set; }
        public DateTime CreatedOn { get; set; }

        public int? CreatedBy { get; set; }



    }


}
