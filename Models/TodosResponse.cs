using net_core_based.Entities;

namespace net_core_based.Models
{
    public class TodosResponse
    {
        public IEnumerable<Todo> Todos { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
