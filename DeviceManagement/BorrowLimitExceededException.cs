/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class BorrowLimitExceededException : BorrowingException
    {
        public string BorrowerId { get; }
        public int MaximumDevices { get; }

        public BorrowLimitExceededException(string borrowerId, int maximumDevices)
            : base($"Nguoi muon {borrowerId} da vuot qua han muc cho phep toi da {maximumDevices} thiet bi.")
        {
            BorrowerId = borrowerId;
            MaximumDevices = maximumDevices;
        }

        public BorrowLimitExceededException(string borrowerId, int maximumDevices, string message)
            : base(message)
        {
            BorrowerId = borrowerId;
            MaximumDevices = maximumDevices;
        }
    }
}
