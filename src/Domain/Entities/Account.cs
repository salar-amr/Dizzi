using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Name { get; set; }

    public UserType UserType { get; set; }

    [Timestamp]
    public uint RowVersion { get; set; }

}
