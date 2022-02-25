using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
  public class UpdateListDTO
  {
    public string Name { get; set; }

    public List<TaskDTO> Tasks { get; set; }
  }
}