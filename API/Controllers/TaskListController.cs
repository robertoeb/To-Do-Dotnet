using System.Linq;
using System.Threading.Tasks;
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
    public async Task<ActionResult<TaskListDTO>> GetTaskList(Guid id)
    {
      var tasks = new List<TaskDTO>();

      var taskList = await _context.TasksList.FindAsync(id);
      var toDoTasks = await _context.ToDoTasks.Where(r => r.TaskList.Id == taskList.Id).ToListAsync();

      if (toDoTasks != null)
      {
        foreach (var task in toDoTasks)
        {
          tasks.Add(new TaskDTO
          {
            Id = task.Id,
            Task = task.Title,
            Completed = task.Completed
          });
        }
      }

      return new TaskListDTO
      {
        Id = taskList.Id,
        Name = taskList.Name,
        Tasks = tasks
      };
    }

    [HttpPost]
    public async Task<ActionResult<TaskListDTO>> CreateList(CreateListDTO createListDto)
    {
      var tasks = new List<TaskDTO>();
      var taskList = new TaskList
      {
        Name = createListDto.Name
      };

      _context.TasksList.Add(taskList);

      if (createListDto.Tasks != null)
      {
        foreach (var task in createListDto.Tasks)
        {
          var toDoTask = new ToDoTask
          {
            Title = task.Task,
            Completed = task.Completed,
            TaskList = taskList
          };

          tasks.Add(new TaskDTO
          {
            Id = task.Id,
            Task = task.Task,
            Completed = task.Completed
          });

          _context.ToDoTasks.Add(toDoTask);
        }
      }

      await _context.SaveChangesAsync();

      return new TaskListDTO
      {
        Id = taskList.Id,
        Name = taskList.Name,
        Tasks = tasks
      };
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<TaskListDTO>> UpdateTaskList(Guid id, UpdateListDTO updateListDTO)
    {
      var tasks = new List<TaskDTO>();

      var taskList = await _context.TasksList.FindAsync(id);

      if (!string.IsNullOrEmpty(updateListDTO.Name)) taskList.Name = updateListDTO.Name;

      if (updateListDTO.Tasks != null)
      {
        foreach (var item in updateListDTO.Tasks)
        {
          var task = await _context.ToDoTasks.FindAsync(item.Id);
          if (string.IsNullOrEmpty(item.Task)) task.Title = item.Task;
          task.Completed = item.Completed;
          tasks.Add(new TaskDTO
          {
            Id = task.Id,
            Task = task.Title,
            Completed = task.Completed
          });
          _context.ToDoTasks.Update(task);
        }
      }

      _context.TasksList.Update(taskList);
      await _context.SaveChangesAsync();

      return new TaskListDTO
      {
        Id = taskList.Id,
        Name = taskList.Name,
        Tasks = tasks
      };
    }
  }

}