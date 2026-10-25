/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class DeviceAlreadyReturnedException : BorrowingException
    {
        public string DeviceId { get; }

        public DeviceAlreadyReturnedException(string deviceId)
            : base($"Thiet bi {deviceId} da duoc tra truoc do.")
        {
            DeviceId = deviceId;
        }

        public DeviceAlreadyReturnedException(string deviceId, string message)
            : base(message)
        {
            DeviceId = deviceId;
        }
    }
}
