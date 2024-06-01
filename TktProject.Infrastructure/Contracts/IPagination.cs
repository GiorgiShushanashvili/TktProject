using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TktProject.Infrastructure.Contracts;

public interface IPagination
{
    Task<List<T>> PaginateAsync<T>(List<T> values,int pageIndex,int pageSize);  
}
