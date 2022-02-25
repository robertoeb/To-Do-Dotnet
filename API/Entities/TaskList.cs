using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.DTO;
using Microsoft.EntityFrameworkCore;
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

    [NotMapped]
    [BackingField(nameof(_tasks))]
    public List<TaskDTO> Tasks
    {
      get
      {
        return JsonConvert.DeserializeObject<List<TaskDTO>>(string.IsNullOrEmpty(_tasks) ? "[]" : _tasks);
      }
      set
      {
        _tasks = value == null ? "[]" : JsonConvert.SerializeObject(value);
      }
    }
  }
}