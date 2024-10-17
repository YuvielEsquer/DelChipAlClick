using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTallerDelChipAlClick.Models

{
    public class LedsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LedID { get; set; }
        public string LedName { get; set; }
        public string Rooms {  get; set; }
        public bool IsActive { get; set; }
    }
}
