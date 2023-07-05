using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using net_core_based.Entities;
using net_core_based.Models;

namespace net_core_based.Services
{
    public interface ITodoService
    {
        public Task<ActionResult> Get(Guid guid);
        public Task<IActionResult> Create(TodoCreateDto todo);
    }
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoService> _logger;

        public TodoService(ApplicationDbContext dbContext,IMapper mapper, ILogger<TodoService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Create(TodoCreateDto todo)
        {
            var _todo = _mapper.Map<Todo>(todo);
            _dbContext.Todos.Add(_todo);
            await _dbContext.SaveChangesAsync();
            return new CreatedAtActionResult("GetTodo", "todo", new { _todo.Id }, _todo);
        }

        public async Task<ActionResult> Get(Guid guid)
        {
            var todo = await _dbContext.Todos.FindAsync(guid);
            if (todo == null) return new NotFoundResult();
            return new OkObjectResult(todo);
        }
    }
}
