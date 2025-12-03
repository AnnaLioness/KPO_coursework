using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class DocumentRepository : BaseRepository<DocumentModel>
    {
        public DocumentRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<DocumentModel>> GetAllAsync()
        {
            return await _dbSet.Include(a => a.Admin).ToListAsync();
        }

        public override async Task<DocumentModel?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(a => a.Admin).FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
