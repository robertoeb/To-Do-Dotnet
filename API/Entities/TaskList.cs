using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace API.Entities
{
  public class TaskList
  {
    private string _tasks;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [MaxLength(50)]
    [Required]
    public string Name { get; set; }

    public ICollection<ToDoTask> Tasks { get; set; }
  }
}