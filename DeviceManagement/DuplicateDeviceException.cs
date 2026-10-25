/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class DuplicateDeviceException : BorrowingException
    {
        public string DeviceId { get; }

        public DuplicateDeviceException(string deviceId)
            : base($"Thiet bi {deviceId} da ton tai trong phieu muon.")
        {
            DeviceId = deviceId;
        }

        public DuplicateDeviceException(string deviceId, string message)
            : base(message)
        {
            DeviceId = deviceId;
        }
    }
}
