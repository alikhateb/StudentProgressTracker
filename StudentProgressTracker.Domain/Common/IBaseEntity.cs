namespace StudentProgressTracker.Domain.Common;

public interface IBaseEntity<TId>
    where TId : struct
{
    public TId Id { get; set; }
}
