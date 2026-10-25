/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;
using System.Collections.Generic;

namespace DeviceManagement
{
    public class BorrowingSlip
    {
        private readonly List<BorrowItem> items;

        public string SlipId { get; }
        public IBorrower Borrower { get; }
        public string TeachingRoom { get; }
        public DateTime BorrowedDate { get; }
        public IReadOnlyList<BorrowItem> Items => items;

        public BorrowingSlip(string slipId, IBorrower borrower, string teachingRoom, DateTime borrowedDate)
        {
            if (string.IsNullOrWhiteSpace(slipId))
            {
                throw new ArgumentException("Ma phieu muon khong duoc de trong.");
            }

            if (borrower == null)
            {
                throw new ArgumentNullException(nameof(borrower), "Nguoi muon khong duoc null.");
            }

            if (string.IsNullOrWhiteSpace(teachingRoom))
            {
                throw new ArgumentException("Phong hoc khong duoc de trong.");
            }

            SlipId = slipId;
            Borrower = borrower;
            TeachingRoom = teachingRoom;
            BorrowedDate = borrowedDate;
            items = new List<BorrowItem>();
        }

        // Them thiet bi vao phieu muon
        public void AddDevice(TeachingDevice device, int borrowDays, int fromPeriod = 1, int toPeriod = 5)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device), "Thiet bi khong duoc null.");
            }

            if (borrowDays <= 0)
            {
                throw new ArgumentException("So ngay muon phai lon hon 0.");
            }

            if (device.Status != DeviceStatus.Available)
            {
                throw new DeviceUnavailableException(device.Id, device.Status);
            }

            if (borrowDays > device.MaxBorrowDays)
            {
                throw new InvalidBorrowPeriodException(device.Id, borrowDays, device.MaxBorrowDays);
            }

            foreach (var item in items)
            {
                if (item.Device.Id == device.Id)
                {
                    throw new DuplicateDeviceException(device.Id);
                }
            }

            // Doi trang thai thiet bi truoc khi kiem tra han muc
            device.MarkAsBorrowed();

            // Kiem tra han muc nguoi muon
            if (items.Count >= Borrower.GetMaximumDevices())
            {
                throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices());
            }

            DateTime dueDate = BorrowedDate.AddDays(borrowDays);
            BorrowItem borrowItem = new BorrowItem(device, BorrowedDate, dueDate, fromPeriod, toPeriod);
            items.Add(borrowItem);
        }

        // Tra thiet bi va tra ve phi tre
        public decimal ReturnDevice(string deviceId, DateTime returnedDate)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
            {
                throw new ArgumentException("Ma thiet bi khong duoc de trong.");
            }

            BorrowItem? targetItem = null;
            foreach (var item in items)
            {
                if (item.Device.Id == deviceId)
                {
                    targetItem = item;
                    break;
                }
            }

            if (targetItem == null)
            {
                throw new DeviceNotInSlipException(deviceId);
            }

            targetItem.Return(returnedDate);
            return targetItem.CalculateLateFee();
        }

        // Tinh tong phi tre han cua ca phieu
        public decimal CalculateTotalLateFee()
        {
            decimal totalFee = 0;
            foreach (var item in items)
            {
                totalFee += item.CalculateLateFee();
            }

            return totalFee;
        }

        public override string ToString()
        {
            return $"Phieu muon: {SlipId} - Nguoi muon: {Borrower.FullName} - Phong: {TeachingRoom} - So thiet bi: {items.Count}";
        }
    }
}
