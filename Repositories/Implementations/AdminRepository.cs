using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class AdminRepository : BaseRepository<AdminModel>
    {
        public AdminRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<AdminModel>> GetAllAsync()
        {
            return await _dbSet
                .ToListAsync();
        }

        public override async Task<AdminModel?> GetByIdAsync(int id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
