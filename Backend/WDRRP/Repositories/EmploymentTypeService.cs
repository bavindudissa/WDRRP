using Microsoft.EntityFrameworkCore;
using WDRRP.Models;
using WDRRP.Services;

namespace WDRRP.Repositories;

public class EmploymentTypeService : IEmploymentTypeService
{
    private readonly WdrrpContext _dbContext;
    public EmploymentTypeService(WdrrpContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<EmploymentTypeDto>> GetEmploymentTypes()
    {
        
         var data = await (from employementtype in _dbContext.Skills
                where employementtype.IsActive == true
                select new EmploymentTypeDto
                {
                    Id = employementtype.Id,
                    Type = employementtype.Skill1,
                    IsActive = employementtype.IsActive
                }).ToListAsync();

        return data;    
    }
}
