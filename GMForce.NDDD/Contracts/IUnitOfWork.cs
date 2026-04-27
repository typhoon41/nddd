namespace GMForce.NDDD.Contracts;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    void CancelSaving();
}
