

using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Core.Wrappers
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedRasult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source,int pageNumber,int pageSize)
            where T : class
        {
            if(source ==  null) throw new Exception("source is empty");
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await source.CountAsync();
            if (count == 0) return PaginatedRasult<T>.Success(new List<T>(), count, pageNumber, pageSize);
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var items = await source.Skip((pageNumber-1) * pageSize).Take(pageSize).ToListAsync();
            return PaginatedRasult<T>.Success(items, count, pageNumber, pageSize);

        }
    }
}
