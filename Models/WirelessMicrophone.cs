/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

public class WirelessMicrophone : TeachingDevice
{
    public int BatteryLevel { get; set; }
    public int SignalQuality { get; set; }

    public override bool IsReadyForUse()
    {
        return Status == DeviceStatus.Available && BatteryLevel >= 30 && SignalQuality >= 40;
    }

    public override double GetDefaultPriorityScore()
    {
        return (BatteryLevel + SignalQuality) / 2.0;
    }
}
