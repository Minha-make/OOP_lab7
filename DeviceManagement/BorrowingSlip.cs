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

        // Them thiet bi vao phieu muon - xac thuc toan bo truoc khi doi trang thai
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

            if (fromPeriod <= 0 || toPeriod <= 0 || toPeriod < fromPeriod)
            {
                throw new ArgumentException("Tiet hoc khong hop le.");
            }

            // 1. Kiem tra trung lap thiet bi trong phieu
            foreach (var item in items)
            {
                if (item.Device.Id == device.Id)
                {
                    throw new DuplicateDeviceException(device.Id);
                }
            }

            // 2. Kiem tra han muc cua nguoi muon truoc khi thuc hien
            if (items.Count >= Borrower.GetMaximumDevices())
            {
                throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices());
            }

            // 3. Kiem tra so ngay muon toi da cua thiet bi
            if (borrowDays > device.MaxBorrowDays)
            {
                throw new InvalidBorrowPeriodException(device.Id, borrowDays, device.MaxBorrowDays);
            }

            // 4. Kiem tra san sang su dung bang da hinh
            if (!device.IsReadyForUse())
            {
                throw new DeviceUnavailableException(device.Id, device.Status);
            }

            // 5. Kiem tra tuong thich phong doi voi dieu khien dieu hoa
            if (device is AirConditionerRemote acRemote && !acRemote.IsCompatibleWith(TeachingRoom))
            {
                throw new BorrowingException($"Dieu khien dieu hoa {device.Id} khong tuong thich voi phong {TeachingRoom}.");
            }

            // 6. Khoi tao BorrowItem truoc
            DateTime dueDate = BorrowedDate.AddDays(borrowDays);
            BorrowItem borrowItem = new BorrowItem(device, BorrowedDate, dueDate, fromPeriod, toPeriod);

            // 7. Chi sau khi tat ca dieu kien thoa man moi doi trang thai thiet bi
            device.Borrow();
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
