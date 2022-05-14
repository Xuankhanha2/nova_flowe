using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    /// <summary>
    /// created date: 23/06/2021
    /// created by: VXKHANH
    /// 
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// Lấy toàn bộ đữ liệu
        /// </summary>
        /// <returns></returns>
        public ServiceResult getAll<entity>(int? pageNumber, int? items);

        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult getById<entity>(Guid id);

        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// thêm 1 bản ghi mới
        /// </summary>
        /// <typeparam name="entity"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public ServiceResult insert<entity>(entity param);

        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// cập nhật 1 bản ghi
        /// </summary>
        /// <typeparam name="entity"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public ServiceResult update<entity>(entity param);

        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// xóa bản ghi theo id
        /// </summary>
        /// <typeparam name="entity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult delete<entity>(Guid id);
    }
}
