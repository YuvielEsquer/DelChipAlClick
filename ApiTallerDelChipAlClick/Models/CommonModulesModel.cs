using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiTallerDelChipAlClick.Models
{
    public class CommonModulesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string? Temperature { get; set; }
        public bool IsActive { get; set; }
    }
}
