using TktProject.Infrastructure.Contracts;

namespace TktProject.Infrastructure.Services;
public class Pagination : IPagination
{
    public Task<List<T>> PaginateAsync<T>(List<T> values, int pageIndex, int pageSize)
    {
        if(values.Count()==0||values==null) 
            throw new ArgumentException(nameof(values));
        if(pageIndex<1) 
            throw new ArgumentException("PageIndex Must be greater than 0");
        if(pageSize<1) 
            throw new ArgumentException("PageSize must be greater than 0");
        var totalPages = Math.Ceiling((decimal)values.Count()/pageSize);
        if(pageIndex>totalPages) 
            throw new ArgumentException("Too High Required Pageindex");
        var data = values.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
        return Task.FromResult(data);
    }
}