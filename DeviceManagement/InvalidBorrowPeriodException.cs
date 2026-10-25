/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class InvalidBorrowPeriodException : BorrowingException
    {
        public string DeviceId { get; }
        public int RequestedDays { get; }
        public int MaximumDays { get; }

        public InvalidBorrowPeriodException(string deviceId, int requestedDays, int maximumDays)
            : base($"Thoi gian muon {requestedDays} ngay cua thiet bi {deviceId} vuot qua so ngay toi da ({maximumDays} ngay).")
        {
            DeviceId = deviceId;
            RequestedDays = requestedDays;
            MaximumDays = maximumDays;
        }

        public InvalidBorrowPeriodException(string deviceId, int requestedDays, int maximumDays, string message)
            : base(message)
        {
            DeviceId = deviceId;
            RequestedDays = requestedDays;
            MaximumDays = maximumDays;
        }
    }
}
