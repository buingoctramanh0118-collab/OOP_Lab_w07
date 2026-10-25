/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System.Collections.Generic;

public class AirConditionerRemote : TeachingDevice
{
    public IReadOnlySet<string> CompatibleRooms { get; set; }

    public bool IsCompatibleWith(string roomId)
    {
        return CompatibleRooms != null && CompatibleRooms.Contains(roomId);
    }

    public override bool IsReadyForUse()
    {
        return Status == DeviceStatus.Available;
    }

    public override double GetDefaultPriorityScore()
    {
        return 100;
    }
}
