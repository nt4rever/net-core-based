using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_core_based.Entities;
using net_core_based.Helpers;
using net_core_based.Models;
using net_core_based.Repositories;
using net_core_based.Services;
using Newtonsoft.Json;

namespace net_core_based.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ITodoRepository _repo;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoService todoService, ITodoRepository repo, ILogger<TodoController> logger) {

            _todoService = todoService;
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Todo>> GetTodo(Guid id)
        {
            return await _todoService.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(TodoCreateDto dto)
        {
            return await _todoService.Create(dto);
        }

        [HttpGet]
        public ActionResult<TodosResponse> GetTodos([FromQuery] PaginationParameters prameters)
        {
            var todos = _repo.GetTodoes(prameters);
            _logger.LogInformation($"Returned {todos.Todos.Count()} todos from database.");
            var metadata = new
            {
                todos.CurrentPage,
                todos.PageSize,
                todos.TotalPages
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));   
            return Ok(todos);
        }
    }
}
