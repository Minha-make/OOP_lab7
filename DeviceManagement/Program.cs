/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

using System;
using System.Collections.Generic;

namespace DeviceManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== DEMO LUONG MUON TRA THANH CONG ===");

                // 1. Tao giang vien
                Lecturer lecturer = new Lecturer("GV001", "Nguyen Van An");
                Console.WriteLine($"Nguoi muon: {lecturer.FullName} (Han muc: {lecturer.GetMaximumDevices()} thiet bi)");

                // 2. Tao 3 thiet bi day hoc
                ProjectorRemote projectorRemote = new ProjectorRemote("PR001", "Dieu khien may chieu Sony", 2023, 500000m, 85);
                AirConditionerRemote acRemote = new AirConditionerRemote("AC001", "Dieu khien dieu hoa Daikin", 2022, 450000m, new List<string> { "P201", "P202" });
                Microphone microphone = new Microphone("MC001", "Micro khong day Shure", 2024, 1200000m, 90, 80);

                Console.WriteLine("\nDanh sach thiet bi khoi tao:");
                Console.WriteLine($"- {projectorRemote}");
                Console.WriteLine($"- {acRemote}");
                Console.WriteLine($"- {microphone}");

                // 3. Tao phieu muon cho phong P201
                DateTime borrowDate = new DateTime(2026, 10, 20);
                BorrowingSlip slip = new BorrowingSlip("PM001", lecturer, "P201", borrowDate);
                Console.WriteLine($"\nTao phieu muon {slip.SlipId} tai phong {slip.TeachingRoom} vao ngay {borrowDate:dd/MM/yyyy}");

                // 4. Them dieu khien may chieu va micro vao phieu muon
                slip.AddDevice(projectorRemote, 2, 1, 3);
                slip.AddDevice(microphone, 1, 1, 3);
                Console.WriteLine("Da them dieu khien may chieu (muon 2 ngay) va micro (muon 1 ngay) vao phieu.");

                // 5. Tra may chieu dung han sau 2 ngay
                DateTime projectorReturnDate = borrowDate.AddDays(2);
                decimal projectorLateFee = slip.ReturnDevice("PR001", projectorReturnDate);
                Console.WriteLine($"\nTra may chieu ngay {projectorReturnDate:dd/MM/yyyy}. Phi tre han: {projectorLateFee:N0} VND");

                // 6. Tra micro tre 2 ngay (han 1 ngay, tra sau 3 ngay)
                DateTime micReturnDate = borrowDate.AddDays(3);
                decimal micLateFee = slip.ReturnDevice("MC001", micReturnDate);
                Console.WriteLine($"Tra micro ngay {micReturnDate:dd/MM/yyyy} (tre 2 ngay). Phi tre han: {micLateFee:N0} VND");

                // 7. In danh sach thiet bi tra muon
                Console.WriteLine("\n=== DANH SACH THIET BI TRA MUON ===");
                bool hasLate = false;
                foreach (var item in slip.Items)
                {
                    if (item.IsReturned && item.ReturnedDate.HasValue && item.ReturnedDate.Value.Date > item.DueDate.Date)
                    {
                        int lateDays = (item.ReturnedDate.Value.Date - item.DueDate.Date).Days;
                        Console.WriteLine($"- [{item.Device.Id}] {item.Device.Name}: tre {lateDays} ngay | Phi phat: {item.CalculateLateFee():N0} VND");
                        hasLate = true;
                    }
                }
                if (!hasLate)
                {
                    Console.WriteLine("Khong co thiet bi nao tra muon.");
                }

                // 8. In danh sach thiet bi chua tra
                Console.WriteLine("\n=== DANH SACH THIET BI CHUA TRA ===");
                bool hasUnreturned = false;
                foreach (var item in slip.Items)
                {
                    if (!item.IsReturned)
                    {
                        Console.WriteLine($"- [{item.Device.Id}] {item.Device.Name} (Han tra: {item.DueDate:dd/MM/yyyy})");
                        hasUnreturned = true;
                    }
                }
                if (!hasUnreturned)
                {
                    Console.WriteLine("Tat ca thiet bi trong phieu deu da duoc tra.");
                }

                // 9. Tong phi tre han cua ca phieu
                Console.WriteLine($"\nTong phi tre han cua phieu {slip.SlipId}: {slip.CalculateTotalLateFee():N0} VND");
            }
            catch (BorrowingException ex)
            {
                Console.WriteLine($"Loi nghiep vu muon tra: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi he thong: {ex.Message}");
            }
        }
    }
}
