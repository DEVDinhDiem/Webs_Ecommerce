# ASP.NET Core 6 API

ASP.NET Core 6 API là một dự án mẫu để xây dựng các ứng dụng web API sử dụng ASP.NET Core 6, một framework web mã nguồn mở và hiện đại của Microsoft. ASP.NET Core 6 API cho phép bạn tạo các API RESTful mạnh mẽ, bảo mật và hiệu năng cao với các tính năng như:
- Routing
- Model binding và validation
- Authentication và authorization
- Logging và exception handling
- Testing và documentation
- Middleware và filters
- Configuration và options
- Entity Framework Core 6, một ORM mã nguồn mở để truy cập và thao tác với các cơ sở dữ liệu quan hệ

## Cài đặt

Để chạy dự án này, bạn cần có:

- .NET 6 SDK, bạn có thể tải về từ [đây](https://dotnet.microsoft.com/download/dotnet/6.0)
- Visual Studio 2022, bạn có thể tải về từ [đây](https://visualstudio.microsoft.com/downloads/)
- SQL Server Express LocalDB, bạn có thể tải về từ [đây](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15)

Sau khi cài đặt xong, bạn có thể mở dự án bằng Visual Studio 2022 và chạy nó bằng lệnh:

```bash
dotnet run
```

Hoặc bạn có thể chạy nó bằng cách nhấn nút F5 trong Visual Studio 2022.

## Sử dụng

Dự án này cung cấp các API để quản lý các sản phẩm và danh mục sản phẩm. Bạn có thể sử dụng Postman hoặc Swagger UI để gọi các API. Để truy cập Swagger UI, bạn có thể vào đường dẫn:

```url
https://localhost:5001/swagger/index.html
```

Bạn sẽ thấy các API được liệt kê như sau:

![Swagger UI](swagger.png)

Bạn có thể nhấn vào từng API để xem chi tiết về các tham số, kiểu dữ liệu, mã trả về và ví dụ. Bạn cũng có thể nhấn vào nút Try it out để gọi API trực tiếp từ Swagger UI.

## Đóng góp

Nếu bạn muốn đóng góp cho dự án này, bạn có thể tạo một fork và gửi một pull request. Bạn cũng có thể tạo một issue nếu bạn phát hiện ra lỗi hoặc có ý kiến góp ý.
