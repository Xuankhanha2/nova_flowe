using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    /// <summary>
    /// created date: 22/06/2021
    /// created by: VXKHANH
    /// Lớp kết quả trả về của các API khi có lơi gọi API
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// xác định dữ liệu có hợp lệ hay không
        /// </summary>
        public bool isValid { get; set; }
        /// <summary>
        /// Thông báo muốn đưa ra 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Dữ liệu trả về 
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// Mã http status
        /// </summary>
        public statusCode code { get; set; }
    }
}
