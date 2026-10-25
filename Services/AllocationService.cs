/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
using System.Linq;

public class AllocationService<T> where T : TeachingDevice
{
    private readonly IRepository<T> _repository;
    private ISelectionStrategy<T> _strategy;

    public AllocationService(IRepository<T> repository, ISelectionStrategy<T> strategy)
    {
        _repository = repository;
        _strategy = strategy;
    }

    public void ChangeStrategy(ISelectionStrategy<T> newStrategy)
    {
        _strategy = newStrategy;
    }

    public T Allocate(Predicate<T> additionalCondition = null)
    {
        var readyDevices = _repository.GetAll().Where(d => d.IsReadyForUse());
        
        if (additionalCondition != null)
        {
            readyDevices = readyDevices.Where(d => additionalCondition(d));
        }

        var candidates = readyDevices.ToList();
        var selectedDevice = _strategy.Select(candidates.AsReadOnly());
        
        selectedDevice.Borrow();
        
        return selectedDevice;
    }
}
