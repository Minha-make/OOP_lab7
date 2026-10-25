/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;

namespace DeviceManagement
{
    public class BorrowItem
    {
        public TeachingDevice Device { get; }
        public DateTime BorrowedDate { get; }
        public DateTime DueDate { get; }
        public int FromPeriod { get; }
        public int ToPeriod { get; }
        public DateTime? ReturnedDate { get; private set; }

        public bool IsReturned => ReturnedDate.HasValue;

        public BorrowItem(TeachingDevice device, DateTime borrowedDate, DateTime dueDate, int fromPeriod, int toPeriod)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device), "Thiet bi khong duoc null.");
            }

            if (dueDate < borrowedDate)
            {
                throw new ArgumentException("Ngay het han phai lon hon hoac bang ngay muon.");
            }

            if (fromPeriod <= 0 || toPeriod <= 0)
            {
                throw new ArgumentException("Tiet hoc phai lon hon 0.");
            }

            if (toPeriod < fromPeriod)
            {
                throw new ArgumentException("Tiet ket thuc phai lon hon hoac bang tiet bat dau.");
            }

            Device = device;
            BorrowedDate = borrowedDate;
            DueDate = dueDate;
            FromPeriod = fromPeriod;
            ToPeriod = toPeriod;
            ReturnedDate = null;
        }

        // Xu ly tra thiet bi
        public void Return(DateTime returnDate)
        {
            if (IsReturned)
            {
                throw new DeviceAlreadyReturnedException(Device.Id);
            }

            if (returnDate.Date < BorrowedDate.Date)
            {
                throw new BorrowingException("Ngay tra khong duoc truoc ngay muon.");
            }

            // Chi chuyen ve Available neu thiet bi dang Borrowed, khong ghi de UnderMaintenance
            if (Device.Status == DeviceStatus.Borrowed)
            {
                Device.Return();
            }
            else
            {
                throw new InvalidOperationException($"Thiet bi {Device.Id} khong o trang thai Borrowed (hien tai: {Device.Status}).");
            }

            ReturnedDate = returnDate;
        }

        // Tinh phi tre han neu co
        public decimal CalculateLateFee()
        {
            if (!ReturnedDate.HasValue)
            {
                return 0;
            }

            if (ReturnedDate.Value.Date > DueDate.Date)
            {
                int lateDays = (ReturnedDate.Value.Date - DueDate.Date).Days;
                return lateDays * Device.GetLateFeePerDay();
            }

            return 0;
        }

        public override string ToString()
        {
            string statusText = IsReturned ? $"Da tra ngay {ReturnedDate:dd/MM/yyyy}" : "Chua tra";
            return $"[{Device.Id}] {Device.Name} | Muon: {BorrowedDate:dd/MM/yyyy} - Han: {DueDate:dd/MM/yyyy} | Tiet: {FromPeriod}-{ToPeriod} | {statusText}";
        }
    }
}
