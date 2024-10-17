using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation; 

namespace ApiTallerDelChipAlClick.DtoModels
{
    public class LedsDto  
{
        public int LedID { get; set; }
        public string LedName { get; set; }
        public string Rooms { get; set; }
        public bool IsActive { get; set; }
    }
}
