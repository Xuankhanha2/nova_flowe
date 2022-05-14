using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using MySqlConnector;
using Core.Interfaces;
using System.Security.Cryptography;

namespace Infastructure.Repositories
{
    public class BaseRepository: IBaseRepository
    {
        /// <summary>
        /// created by: VXKHANH
        /// created date: 22/06/2021
        /// Lớp base kết nối với cơ sở dữ liệu
        /// </summary>
        private string connectionString = "User Id=root;Host=localhost;Character Set=utf8;password=Khanh2000;Database=freshfood";
        protected IDbConnection dbConnection;

        public BaseRepository()
        {
            dbConnection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <typeparam name="entity">đối tượng</typeparam>
        /// <returns>Danh sách toàn bổ dữ liệu trong bảng</returns>
        public IEnumerable<entity> GetAll<entity>(int? pageNumber, int? items)
        {
            string className = typeof(entity).Name;
            string procName = $"procGet{className}s";
            List<entity> list = dbConnection.Query<entity>(procName, commandType: CommandType.StoredProcedure).ToList();
            if(pageNumber != null && items != null)
            {
                //Số trang - mặc định số trang đầu tiền
                int page = pageNumber ?? 1;
                //Số lượng phần tử trong 1 trang - mặc định 10 phần tử
                int number = items ?? 10;
                //Chỉ số bắt đầu khi sao chép list
                int startIndex = (page - 1) * number;
                //Kiểm tra số lượng phần tử list > số lượng phần tử của 1 trang thì mới thực hiện 
                if(list.Count > number)
                {

                    if (list.Count - startIndex >= number)
                    {
                        //nếu số phần tử còn lại tính từ vị trí startIndex đến phần tử cuối cùng của mảng lớn hơn số lượng phần tử của 1 trang
                        entity[] subList = new entity[number];
                        list.CopyTo(startIndex, subList, 0, number);
                        return subList;
                    }
                    else
                    {
                        entity[] subList = new entity[list.Count - startIndex];
                        list.CopyTo(startIndex, subList, 0, list.Count - startIndex);
                        return subList;
                    }
                }
                
            }
            return list;
        }

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <typeparam name="entity">Đối tượng</typeparam>
        /// <param name="id">id của đói tượng cần lấy</param>
        /// <returns></returns>
        public entity getById<entity>(Guid id)
        {
            string className = typeof(entity).Name;
            string procName = $"procGet{className}ById";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{className}Id", id.ToString());
            entity result = dbConnection.Query<entity>(procName, param:dynamicParameters, commandType:CommandType.StoredProcedure).FirstOrDefault();
            return result;
        }
        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// </summary>
        /// <typeparam name="entity">Đối tượng</typeparam>
        /// <param name="param">Đối tượng chứa dữ liệu thêm mới</param>
        /// <returns>Số dòng thay đổi</returns>
        public virtual int insertEntity<entity>(entity param)
        {
            string className = typeof(entity).Name;
            string procName = $"procInsert{className}";
            int result = dbConnection.Execute(procName, param: param, commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="param">Đối tượng chứa dữ liệu cần thay đổi</param>
        /// <returns>Sô dòng thay đổi</returns>
        public int updateEntity<entity>(entity param)
        {
            string className = typeof(entity).Name;
            string procName = $"procUpdate{className}";
            int result = dbConnection.Execute(procName, param: param, commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// created date: 22/06/2021
        /// created by: VXKHANH
        /// Hàm xóa 1 bản ghi
        /// </summary>
        /// <typeparam name="entity">Đối tượng</typeparam>
        /// <param name="id">Id của đối tượng cần xóa</param>
        /// <returns>Số dòng thay đổi</returns>
        public int deleteEntity<entity>(Guid id)
        {
            string className = typeof(entity).Name;
            string procName = $"procDelete{className}";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{className}Id", id.ToString());
            int result = dbConnection.Execute(procName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// created date 3/8/2021
        /// created by: vu xuan khanh
        /// Hàm lấy dữ liệu theo tên thuộc tính truyền vào
        /// </summary>
        /// <typeparam name="entity">Type</typeparam>
        /// <param name="param">Tham số giá trị để tìm kiếm </param>
        /// <param name="propertyName">Tên của thuộc tính</param>
        public entity getByProperty<entity>(string param, string propertyName)
        {
            string className = typeof(entity).Name;
            string procName = $"procGet{className}By{propertyName}";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{propertyName}", param.ToString());
            entity result = dbConnection.Query<entity>(propertyName, param: dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return result;
        }

        public string sha256Hash(string value)
        {
            SHA256Managed sha256 = new SHA256Managed();
            Byte[] byteValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
            string hashedValue = "";
            foreach (Byte conv in byteValue)
            {
                hashedValue += conv.ToString("x2");
            }
            return hashedValue.ToLower();
        }
    }
}
