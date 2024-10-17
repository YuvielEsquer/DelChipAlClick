using ApiTallerDelChipAlClick.DtoModels;
using ApiTallerDelChipAlClick.Models;
using ApiTallerDelChipAlClick.Repository;
using AutoMapper;


namespace ApiTallerDelChipAlClick.Services
{
    public class LedsService : ICommonService<LedsDto, LedsInsertDto, LedsUpdateDto>
    {
        private IRepository<LedsModel> _ledsRepository;
        private IMapper _mapper;
        public List<string> Errors { get; }
        public LedsService(IRepository<LedsModel> ledsRepository, 
            IMapper mapper)
        {
            _ledsRepository = ledsRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<LedsDto> Add(LedsInsertDto InsertDto)
        {
            var leds = _mapper.Map<LedsModel>(InsertDto);

            await _ledsRepository.Add(leds);
            await _ledsRepository.Save();

            var ledsDto = _mapper.Map<LedsDto>(leds);

            return ledsDto;
        }
        public async Task<LedsDto> Delete(int id)
        {
            var led = await _ledsRepository.GetById(id);
            
            if(led != null)
            {
                var ledDto = _mapper.Map<LedsDto>(led);

                _ledsRepository.Delete(led);
                await _ledsRepository.Save();

                return ledDto;
            }
            return null;
        }
        public async Task<IEnumerable<LedsDto>> Get()
        {
            var leds = await _ledsRepository.Get();

            return leds.Select(b => _mapper.Map<LedsDto>(b));
        }
        public async Task<LedsDto> GetById(int id)
        {
            var leds = await _ledsRepository.GetById(id);
            if(leds != null)
            {
                var ledsDto = _mapper.Map<LedsDto>(leds);
                return ledsDto;
            }
            return null;
        }
        public async Task<LedsDto> Update(int id, LedsUpdateDto ledsUpdateDto)
        {
            var leds = await _ledsRepository.GetById(id);

            if (leds != null)
            {
                leds = _mapper.Map<LedsUpdateDto, LedsModel>(ledsUpdateDto, leds);

                _ledsRepository.Update(leds);
                await _ledsRepository.Save();

                var ledsDto = _mapper.Map<LedsDto>(leds);

                return ledsDto;
            }
            return null;
        }
        public bool Validate(LedsInsertDto ledsInsertDto)
        {
            if (_ledsRepository.Search(l => l.LedName == ledsInsertDto.LedName).Count()>0) 
            {
                Errors.Add("No puede existir un led con un nombre ya existente");
                return false;
            }
            return true;
        }
        
    }
}
