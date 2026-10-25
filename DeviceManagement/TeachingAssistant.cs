/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class TeachingAssistant : IBorrower
    {
        public string BorrowerId { get; }
        public string FullName { get; }

        public TeachingAssistant(string borrowerId, string fullName)
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

        // Tro giang duoc muon toi da 2 thiet bi
        public int GetMaximumDevices()
        {
            return 2;
        }

        public override string ToString()
        {
            return $"Tro giang: [{BorrowerId}] {FullName} - Han muc: {GetMaximumDevices()} thiet bi";
        }
    }
}
