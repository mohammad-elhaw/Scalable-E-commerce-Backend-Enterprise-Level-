namespace SharedKernel;

public abstract class AuditableAggregateRoot<TId>
    : AggregateRoot<TId>
{
    public DateTime CreatedAtUtc { get; protected set; }
    public Guid? CreatedBy { get; protected set; }
    public DateTime? ModifiedAtUtc { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public DateTime? DeletedAtUtc { get; protected set; }

    protected void MarkModified(Guid? userId)
    {
        ModifiedAtUtc = DateTime.UtcNow;
        ModifiedBy = userId;
    }

    public void SoftDelete(Guid? userId)
    {
        IsDeleted = true;
        DeletedAtUtc = DateTime.UtcNow;
        ModifiedBy = userId;
    }
}