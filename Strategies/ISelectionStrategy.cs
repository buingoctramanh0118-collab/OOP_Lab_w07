/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System.Collections.Generic;

public interface ISelectionStrategy<T>
{
    T Select(IReadOnlyList<T> candidates);
}
