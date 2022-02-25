using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
  public class CreateListDTO
  {
    [Required]
    public string Name { get; set; }

    public List<TaskDTO> Tasks { get; set; }
  }
}