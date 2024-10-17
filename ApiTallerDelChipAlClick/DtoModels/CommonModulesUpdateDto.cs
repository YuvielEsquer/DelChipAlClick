namespace ApiTallerDelChipAlClick.DtoModels
{
    public class CommonModulesUpdateDto
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string? Temperature { get; set; }
        public bool IsActive { get; set; }
    }
}
