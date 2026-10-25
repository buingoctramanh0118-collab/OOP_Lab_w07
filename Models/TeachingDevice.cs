/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;

public abstract class TeachingDevice : IIdentifiable
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string StorageLocation { get; set; }
    public DeviceStatus Status { get; protected set; } = DeviceStatus.Available;

    public abstract bool IsReadyForUse();
    public abstract double GetDefaultPriorityScore();

    public void Borrow()
    {
        if (IsReadyForUse())
        {
            Status = DeviceStatus.Borrowed;
        }
    }

    public void Return()
    {
        if (Status == DeviceStatus.Borrowed)
        {
            Status = DeviceStatus.Available;
        }
    }
}
