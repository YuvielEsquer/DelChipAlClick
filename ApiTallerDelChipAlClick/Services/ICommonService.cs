namespace ApiTallerDelChipAlClick.Services
{
    public interface ICommonService<GetDto, InsertDto, UpdateDto>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<GetDto>> Get();
        Task<GetDto> GetById(int id);
        Task<GetDto> Add(InsertDto InsertDto);
        Task<GetDto> Update(int id, UpdateDto beerUpdateDto);
        Task<GetDto> Delete(int id);
        bool Validate(InsertDto dto);
    }
}
