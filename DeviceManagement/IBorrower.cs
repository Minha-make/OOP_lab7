/****************
Ma sinh vien: 202418890
Ho ten: Nguyễn Minh Hà
****************/

namespace DeviceManagement
{
    public interface IBorrower
    {
        string BorrowerId { get; }
        string FullName { get; }
        int GetMaximumDevices();
    }
}
