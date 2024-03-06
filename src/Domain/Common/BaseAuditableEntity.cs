namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedTime { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public int? LastModifiedBy { get; set; }
}
