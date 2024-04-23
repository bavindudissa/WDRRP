namespace WDRRP.Services;

public interface IEmploymentTypeService
{
    Task<IEnumerable<EmploymentTypeDto>> GetEmploymentTypes();
}
