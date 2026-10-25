/*
MSSV: 202418832
Họ tên: Bùi Ngọc Trâm Anh
*/

using System;
public class DuplicateIdException : Exception { public string DuplicateId { get; set; } }
public class NoSuitableItemException : Exception { }
public class DeviceNotReadyException : Exception {
    public string DeviceId { get; set; }
    public DeviceStatus CurrentStatus { get; set; }
}
