using API.Data;
using API.DTO;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class TaskListController : BaseApiController
  {
    private readonly DataContext _context;

    public TaskListController(DataContext context)
    {
      _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<TaskList>>> GetTasksList()
    {
      return await _context.TasksList.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskList>> GetTaskList(Guid id)
    {
      return await _context.TasksList.FindAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<TaskListDTO>> CreateList(CreateListDTO createListDto)
    {
      var taskList = new TaskList
      {
        Name = createListDto.Name,
        Tasks = createListDto.Tasks
      };

      _context.TasksList.Add(taskList);
      await _context.SaveChangesAsync();

      return new TaskListDTO
      {
        Id = taskList.Id,
        Name = taskList.Name,
        Tasks = taskList.Tasks
      };
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<TaskList>> UpdateTaskList(Guid id, UpdateListDTO updateListDTO)
    {
      var taskList = await _context.TasksList.FindAsync(id);

      if (!string.IsNullOrEmpty(updateListDTO.Name))
      {
        taskList.Name = updateListDTO.Name;
      }

      if (updateListDTO.Tasks != null)
      {
        taskList.Tasks = updateListDTO.Tasks;
      }

      _context.TasksList.Update(taskList);
      await _context.SaveChangesAsync();

      return taskList;
    }
  }

}