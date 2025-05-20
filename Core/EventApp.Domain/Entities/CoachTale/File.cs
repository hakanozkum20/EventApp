using System.ComponentModel.DataAnnotations.Schema;
using EventApp.Domain.Entities.Common;

namespace EventApp.Domain.Entities.CoachTale;

public class File : BaseEntity
{
    public string FileName { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; }
    
    [NotMapped]
    public override DateTime UpdatedDate { get; set;  }
}