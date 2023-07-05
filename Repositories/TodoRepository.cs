using AutoMapper;
using Microsoft.EntityFrameworkCore;
using net_core_based.Entities;
using net_core_based.Helpers;
using net_core_based.Models;
using net_core_based.Services;

namespace net_core_based.Repositories
{
    public interface ITodoRepository
    {
        TodosResponse GetTodoes(PaginationParameters parameters);
    }

    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoService> _logger;

        public TodoRepository(ApplicationDbContext dbContext, IMapper mapper, ILogger<TodoService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public TodosResponse GetTodoes(PaginationParameters parameters)
        {
            var todos = _dbContext.Todos.OrderByDescending(x => x.CreatedAt).Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize).ToList();
            var count = _dbContext.Todos.Count();
            return new TodosResponse { Todos = todos, CurrentPage = parameters.PageNumber, PageSize = parameters.PageSize, TotalPages = (int)Math.Ceiling(count / (double)parameters.PageSize) };
        }
    }
}
