namespace SharedKernel;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}
