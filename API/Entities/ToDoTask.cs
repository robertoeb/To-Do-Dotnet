using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
  public class ToDoTask
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Title { get; set; }

    [DefaultValue(false)]
    public bool Completed { get; set; }

    public TaskList TaskList { get; set; }
  }
}