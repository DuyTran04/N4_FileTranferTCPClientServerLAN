# Ứng Dụng Truyền File Qua Mạng LAN

## 📝 Mô tả
Ứng dụng cho phép truyền file giữa các máy tính trong mạng LAN sử dụng giao thức TCP/IP với mô hình Client-Server. Được xây dựng bằng C# Windows Forms.

## ✨ Tính năng chính
- **Server:**
  - Hiển thị địa chỉ IP của máy
  - Cho phép chọn port để lắng nghe
  - Tự động tạo thư mục lưu file
  - Hiển thị danh sách clients kết nối
  - Theo dõi tiến trình nhận file
  - Xử lý file trùng tên tự động

- **Client:**
  - Kết nối tới server thông qua IP và port
  - Cho phép chọn nhiều file để gửi
  - Hiển thị tiến trình gửi file
  - Giao diện thân thiện, dễ sử dụng

## 🛠️ Công nghệ sử dụng
- C# Windows Forms (.NET Framework)
- TCP/IP Protocol
- File I/O Operations
- Multi-threading
- Asynchronous Programming

## 📋 Yêu cầu hệ thống
- Windows 7/8/10/11
- .NET Framework 4.7.2 trở lên
- Kết nối mạng LAN

## 🚀 Hướng dẫn cài đặt
1. Clone repository:
```bash
git clone https://github.com/DuyTran04/N4_FileTranferTCPClientServerLAN.git
```
2. Mở solution trong Visual Studio
3. Build và chạy ứng dụng

## 📖 Hướng dẫn sử dụng

### Khởi động Server:
1. Chạy ứng dụng TCPServer
2. Nhập port muốn sử dụng (ví dụ: 8083)
3. Chọn thư mục lưu file (hoặc sử dụng thư mục mặc định)
4. Click "Start Server"
5. Ghi nhớ địa chỉ IP hiển thị

### Kết nối Client:
1. Chạy ứng dụng TCPClient
2. Nhập địa chỉ IP của Server
3. Nhập port đã cấu hình trên Server
4. Click "Connect"
5. Chọn file và gửi

## 🔧 Xử lý sự cố thường gặp
1. Không kết nối được:
   - Kiểm tra IP và port
   - Đảm bảo Server đang chạy
   - Kiểm tra firewall
   - Thử ping giữa các máy

2. Không gửi được file:
   - Kiểm tra quyền truy cập thư mục
   - Đảm bảo còn đủ dung lượng
   - Kiểm tra kết nối mạng

## 🔒 Bảo mật
- Chỉ sử dụng trong mạng LAN tin cậy
- Cấu hình firewall phù hợp
- Kiểm soát quyền truy cập thư mục

## 🤝 Đóng góp
Mọi đóng góp đều được chào đón:
1. Fork project
2. Tạo branch mới (`git checkout -b feature/AmazingFeature`)
3. Commit thay đổi (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Tạo Pull Request

## ⚖️ Giấy phép
Project được phân phối dưới giấy phép MIT. Xem thêm tại `LICENSE`.

## 👥 Tác giả
- DuyTran04

## 📧 Liên hệ
- Email: [anhduytd@outlook.com.vn]
- GitHub: [@DuyTran04](https://github.com/DuyTran04)
