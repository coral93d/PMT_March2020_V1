using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    [Table("TrackMaster")]
    public class TrackMaster
    {
        [Key]
        public int TrackID { get; set; }

        [Required(ErrorMessage = "Enter Track Name")]
     
        public string TrackName { get; set; }
    }
}
