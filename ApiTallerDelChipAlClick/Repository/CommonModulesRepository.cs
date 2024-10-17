using ApiTallerDelChipAlClick.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTallerDelChipAlClick.Repository
{
    public class CommonModulesRepository : IRepository<CommonModulesModel>
    {
        private TallerContext _context;
        public CommonModulesRepository(TallerContext context)
        {
            _context = context;
        }
        public async Task Add(CommonModulesModel entity)
            => await _context.AddAsync(entity);
        

        public void Delete(CommonModulesModel entity)
            => _context.CommonModules.Remove(entity);

        public async Task<IEnumerable<CommonModulesModel>> Get()
            => await _context.CommonModules.ToListAsync();

        public async Task<CommonModulesModel> GetById(int id)
            => await _context.CommonModules.FindAsync(id);

        public async Task Save()
            => await _context.SaveChangesAsync();
        

        public IEnumerable<CommonModulesModel> Search(Func<CommonModulesModel, bool> filter)
            => _context.CommonModules.Where(filter).ToList();
        

        public void Update(CommonModulesModel entity)
        {
            _context.CommonModules.Attach(entity);
            _context.CommonModules.Entry(entity).State = EntityState.Modified;
        }
    }
}
