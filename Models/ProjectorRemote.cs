/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

public class ProjectorRemote : TeachingDevice
{
    public int BatteryLevel { get; set; }

    public override bool IsReadyForUse()
    {
        return Status == DeviceStatus.Available && BatteryLevel >= 20;
    }

    public override double GetDefaultPriorityScore()
    {
        return BatteryLevel;
    }
}
