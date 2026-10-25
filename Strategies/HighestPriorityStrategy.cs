/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
using System.Collections.Generic;
using System.Linq;

public class HighestPriorityStrategy<T> : ISelectionStrategy<T>
{
    private readonly Func<T, double> _scoreSelector;

    public HighestPriorityStrategy(Func<T, double> scoreSelector)
    {
        _scoreSelector = scoreSelector;
    }

    public T Select(IReadOnlyList<T> candidates)
    {
        if (candidates == null || !candidates.Any())
        {
            throw new NoSuitableItemException();
        }
        return candidates.OrderByDescending(_scoreSelector).First();
    }
}
