/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class BorrowingException : Exception
    {
        public BorrowingException(string message) : base(message)
        {
        }

        public BorrowingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
