using ApiTallerDelChipAlClick.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTallerDelChipAlClick.Repository
{
    public class LedsRepository : IRepository<LedsModel>
    {
        private TallerContext _context;
        public LedsRepository(TallerContext context) 
        {
            _context = context;
        }

        public async Task Add(LedsModel entity)
            => await _context.AddAsync(entity);
        public void Delete(LedsModel entity)
            => _context.Leds.Remove(entity);
        public async Task<IEnumerable<LedsModel>> Get()
            => await _context.Leds.ToListAsync();
        public async Task<LedsModel> GetById(int id)
            => await _context.Leds.FindAsync(id);
        public async Task Save()
            => await _context.SaveChangesAsync();
        public IEnumerable<LedsModel> Search(Func<LedsModel, bool> filter)
            => _context.Leds.Where(filter).ToList();
        public void Update(LedsModel entity)
        {
            _context.Leds.Attach(entity);
            _context.Leds.Entry(entity).State = EntityState.Modified;
        }
    }
}
