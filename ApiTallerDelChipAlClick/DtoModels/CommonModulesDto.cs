using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace ApiTallerDelChipAlClick.DtoModels
{
    public class CommonModulesDto
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string? Temperature { get; set; }
        public bool IsActive { get; set; }
    }
}
