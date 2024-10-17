using ApiTallerDelChipAlClick.DtoModels;
using ApiTallerDelChipAlClick.Models;
using ApiTallerDelChipAlClick.Repository;
using AutoMapper;

namespace ApiTallerDelChipAlClick.Services
{
    public class CommonModulesService : ICommonService<CommonModulesDto, CommonModulesInsertDto, CommonModulesUpdateDto>
    {
        private IRepository<CommonModulesModel> _commonModulesrepository;
        private IMapper _mapper;
        public List<string> Errors { get; }
        public CommonModulesService(IRepository<CommonModulesModel> repository, 
            IMapper mapper)
        {
            _commonModulesrepository = repository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<CommonModulesDto> Add(CommonModulesInsertDto InsertDto)
        {
            var modules = _mapper.Map<CommonModulesModel>(InsertDto);

            await _commonModulesrepository.Add(modules);
            await _commonModulesrepository.Save();

            var modulesDto = _mapper.Map<CommonModulesDto>(modules);

            return modulesDto;
        }

        public async Task<CommonModulesDto> Delete(int id)
        {
            var modules = await _commonModulesrepository.GetById(id);

            if(modules != null)
            {
                var modulesDto = _mapper.Map<CommonModulesDto>(modules);

                _commonModulesrepository.Delete(modules);
                await _commonModulesrepository.Save();

                return modulesDto;
            }
            return null;
        }

        public async Task<IEnumerable<CommonModulesDto>> Get()
        {
            var modules = await _commonModulesrepository.Get();

            return modules.Select(b => _mapper.Map<CommonModulesDto>(b));
        }

        public async Task<CommonModulesDto> GetById(int id)
        {
            var modules = await _commonModulesrepository.GetById(id);
            if (modules != null)
            {
                var modulesDto = _mapper.Map<CommonModulesDto>(modules);
                return modulesDto;
            }
            return null;
        }

        public async Task<CommonModulesDto> Update(int id, CommonModulesUpdateDto commonModulesUpdateDto)
        {
            var modules = await _commonModulesrepository.GetById(id);

            if (modules != null)
            {
                modules = _mapper.Map<CommonModulesUpdateDto, CommonModulesModel>(commonModulesUpdateDto, modules);

                _commonModulesrepository.Update(modules);
                await _commonModulesrepository.Save();

                var modulesDto = _mapper.Map<CommonModulesDto>(modules);

                return modulesDto;
            }
            return null;
        }

        public bool Validate(CommonModulesInsertDto dto)
        {
            if (_commonModulesrepository.Search(l => l.ModuleName == dto.ModuleName).Count() > 0)
            {
                Errors.Add("No puede existir un modulo con un nombre ya existente");
                return false;
            }
            return true;
        }

        public bool Validate(CommonModulesUpdateDto dto)
        {
            if (_commonModulesrepository.Search(l => l.ModuleName == dto.ModuleName
            && dto.ModuleID != l.ModuleID).Count() > 0)
            {
                Errors.Add("No puede existir un modulo con un nombre ya existente");
                return false;
            }
            return true;
        }
    }
}