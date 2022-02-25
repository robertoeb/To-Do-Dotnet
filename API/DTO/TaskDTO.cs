using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.DTO
{
  public class TaskDTO
  {
    [Required]
    public string Task { get; set; }
    [DefaultValue(false)]
    public bool Completed { get; set; }
  }
}