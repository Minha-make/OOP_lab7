/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class Lecturer : IBorrower
    {
        public string BorrowerId { get; }
        public string FullName { get; }

        public Lecturer(string borrowerId, string fullName)
        {
            if (string.IsNullOrWhiteSpace(borrowerId))
            {
                throw new ArgumentException("Ma nguoi muon khong duoc de trong.");
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Ho ten nguoi muon khong duoc de trong.");
            }

            BorrowerId = borrowerId;
            FullName = fullName;
        }

        // Giang vien duoc muon toi da 3 thiet bi
        public int GetMaximumDevices()
        {
            return 3;
        }

        public override string ToString()
        {
            return $"Giang vien: [{BorrowerId}] {FullName} - Han muc: {GetMaximumDevices()} thiet bi";
        }
    }
}
