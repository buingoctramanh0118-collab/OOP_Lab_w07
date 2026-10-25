/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System.Collections.Generic;
using System.Linq;

public class FirstAvailableStrategy<T> : ISelectionStrategy<T>
{
    public T Select(IReadOnlyList<T> candidates)
    {
        if (candidates == null || !candidates.Any())
        {
            throw new NoSuitableItemException();
        }
        return candidates.First();
    }
}
