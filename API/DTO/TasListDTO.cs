using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
  public class TaskListDTO
  {
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<TaskDTO> Tasks { get; set; }
  }
}