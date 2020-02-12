using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.DataServices.Interfaces;
using Source.Core.DataService.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Source.DataServices.EFCore
{
    using DataContext;
    using Domains.Entities;
    using Source.Pagination.Interfaces;

    public class CaregiversDataService : EntityDataService<Caregivers>, ICaregiversDataService
    {
        public CaregiversDataService(AppDbContext dbContext, IPageHelper pageHelper) : base(dbContext, pageHelper)        
        {

        }

        
        public virtual async Task<IList<Caregivers>> GetByFirstName(string firstName)
        {
            return await DbContext.Set<Caregivers>().Where(x => x.Name.Contains(firstName)).ToListAsync();
        }
        
        
    }
}