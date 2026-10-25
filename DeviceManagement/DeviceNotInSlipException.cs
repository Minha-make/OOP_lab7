/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class DeviceNotInSlipException : BorrowingException
    {
        public string DeviceId { get; }

        public DeviceNotInSlipException(string deviceId)
            : base($"Thiet bi {deviceId} khong co trong phieu muon.")
        {
            DeviceId = deviceId;
        }

        public DeviceNotInSlipException(string deviceId, string message)
            : base(message)
        {
            DeviceId = deviceId;
        }
    }
}
