using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IBaseRepository
    {
        /// <summary>
        /// created date: 22/06/2021
        /// created by: VXKHANH
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <typeparam name="entity">Đối tượng</typeparam>
        /// <returns>Danh sách dữ liệu cần lấy</returns>
        public IEnumerable<entity> GetAll<entity>(int? pageNumber, int? items);

        /// <summary>
        /// created date: 22/06/2021
        /// created by: VXKHANH
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <typeparam name="entity">Đối tượng</typeparam>
        /// <param name="id">id của đói tượng cần lấy</param>
        /// <returns></returns>
        public entity getById<entity>(Guid id);

        /// <summary>
        /// created date: 22/06/2021
        /// created by: VXKHANH
        /// Thêm bản ghi mới 
        /// </summary>
        /// <typeparam name="entity">Đối tượng</typeparam>
        /// <param name="param">Đối tượng chứa dữ liệu thêm mới bản ghi</param>
        /// <returns>Số dòng thay đổi</returns>
        public int insertEntity<entity>(entity param);

        /// <summary>
        /// created date: 22/06/2021
        /// created by: VXKHANH
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="param">Đối tượng chứa dữ liệu cần thay đổi</param>
        /// <returns></returns>
        public int updateEntity<entity>(entity param);

        /// <summary>
        /// created date: 22/06/2021
        /// created by: VXKHANH
        /// Hàm xóa 1 bản ghi
        /// </summary>
        /// <typeparam name="entity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public int deleteEntity<entity>(Guid id);

        /// <summary>
        /// created date 3/8/2021
        /// created by: vu xuan khanh
        /// Hàm lấy dữ liệu theo tên thuộc tính truyền vào
        /// </summary>
        /// <typeparam name="entity">Type</typeparam>
        /// <param name="param">Tham số giá trị để tìm kiếm </param>
        /// <param name="propertyName">Tên của thuộc tính</param>
        public entity getByProperty<entity>(string param, string propertyName);

    }
}
