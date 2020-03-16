using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    [NotMapped]
    public class AuditFormsModel
    {
        [Required(ErrorMessage = "Enter Form ID")]

        public int FormsID { get; set; }

        [Required(ErrorMessage = "Enter Form Name")]
        public string FormName { get; set; }

       public int FormsCode { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }
        public List<FormsFieldModel> ListofFormsField { get; set; }


    }

    [NotMapped]
    public class FormsFieldModel
    {

        public int FieldID { get; set; }

        public string Questions { get; set; }


        public bool selectedFormsField { get; set; }
          public string FormsID { get; set; }

    }


}