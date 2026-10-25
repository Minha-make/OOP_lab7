/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class DeviceUnavailableException : BorrowingException
    {
        public string DeviceId { get; }
        public DeviceStatus CurrentStatus { get; }

        public DeviceUnavailableException(string deviceId, DeviceStatus currentStatus)
            : base($"Thiet bi {deviceId} khong san sang de muon (Trang thai hien tai: {currentStatus}).")
        {
            DeviceId = deviceId;
            CurrentStatus = currentStatus;
        }

        public DeviceUnavailableException(string deviceId, DeviceStatus currentStatus, string message)
            : base(message)
        {
            DeviceId = deviceId;
            CurrentStatus = currentStatus;
        }
    }
}
