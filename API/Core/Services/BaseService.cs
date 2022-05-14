using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Models.BaseClass;
namespace Core.Services
{
    public class BaseService : IBaseService
    {
        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// </summary>
        protected ServiceResult serviceResult = new ServiceResult();
        IBaseRepository baseRepository;
        public BaseService(IBaseRepository _baseRepository)
        {
            baseRepository = _baseRepository;
        }
       
        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// Lấy toàn bộ dữ liệu 
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        public ServiceResult getAll<entity>(int? pageNumber, int? items)
        {
            try
            {
                var list = baseRepository.GetAll<entity>(pageNumber, items);
                if (list != null)
                {
                    serviceResult.isValid = true;
                    serviceResult.data = list;
                    serviceResult.code = statusCode.success;
                }
                else
                {
                    serviceResult.isValid = true;
                    serviceResult.data = list;
                    serviceResult.code = statusCode.noContent;
                }
            }
            catch (Exception e)
            {
                serviceResult.isValid = false;
                serviceResult.data = e.Message;
                serviceResult.message = e.Message;
                serviceResult.code = statusCode.exception;
            }
            return serviceResult;
        }

        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// Lấy bản ghi theo id
        /// </summary>
        /// <typeparam name="entity"></typeparam>
        /// <param name="id"></param>
        /// <returns>Trả về bản ghi có id tương ứng</returns>
        public ServiceResult getById<entity>(Guid id)
        {
            try
            {
                var data = baseRepository.getById<entity>(id);
                if (data != null)
                {
                    serviceResult.isValid = true;
                    serviceResult.data = data;
                    serviceResult.code = statusCode.success;
                }
                else
                {
                    serviceResult.isValid = true;
                    serviceResult.data = data;
                    serviceResult.code = statusCode.noContent;
                }
            }
            catch (Exception e)
            {
                serviceResult.message = e.Message;
                serviceResult.isValid = false;
                serviceResult.data = e.Message;
                serviceResult.code = statusCode.exception;
            }
            return serviceResult;
        }

        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <typeparam name="entity">Kiểu</typeparam>
        /// <param name="param">Đối tượng</param>
        /// <returns>Số dòng ảnh hưởng. Nếu > 0 thì đã thêm thành công</returns>
        public virtual ServiceResult insert<entity>(entity param)
        {
            try
            {
                //validate với trạng thái là insert
                baseValidate<entity>(param, false);
                if (serviceResult.isValid)
                {
                    int check = baseRepository.insertEntity<entity>(param);
                    if (check > 0)
                    {
                        serviceResult.code = statusCode.success;
                        serviceResult.message = Properties.resource.createdSuccess;
                        serviceResult.data = Properties.resource.createdSuccess;
                    }
                    else
                    {
                        serviceResult.code = statusCode.fail;
                        serviceResult.message = Properties.resource.createFail;
                        serviceResult.data = Properties.resource.createdSuccess;
                    }
                }
                else
                {
                    serviceResult.code = statusCode.notValid;
                    //Không cần gán message vì trong hàm validate đã gán rồi
                    serviceResult.data = null;
                }
            }
            catch(Exception e)
            {
                serviceResult.message = e.Message;
                serviceResult.isValid = false;
                serviceResult.data = e.Message;
                serviceResult.code = statusCode.exception;
            }
            return serviceResult;
        }

        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// Cập nhật dữ liệu 
        /// </summary>
        /// <typeparam name="entity">Kiểu</typeparam>
        /// <param name="param">Đói tượng</param>
        /// <returns>Số dòng ảnh hưởng. Nếu > 0 thì đã cập nhật thành công</returns>
        public ServiceResult update<entity>(entity param)
        {
            try
            {
                baseValidate<entity>(param, true);
                if (serviceResult.isValid)
                {
                    int check = baseRepository.updateEntity<entity>(param);
                    if (check > 0)
                    {
                        serviceResult.code = statusCode.success;
                        serviceResult.message = Properties.resource.updatetSuccess;
                        serviceResult.data = check;
                    }
                    else
                    {
                        serviceResult.code = statusCode.fail;
                        serviceResult.message = Properties.resource.updateFail;
                        serviceResult.data = check;
                    }
                }
                else
                {
                    serviceResult.code = statusCode.notValid;
                    //Không cần gán message vì trong hàm validate đã gán rồi
                    serviceResult.data = null;
                }
            }
            catch (Exception e)
            {
                serviceResult.message = e.Message;
                serviceResult.isValid = false;
                serviceResult.data = e.Message;
                serviceResult.code = statusCode.exception;
            }
            return serviceResult;
        }

        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// Xoá dữ liệu theo id 
        /// </summary>
        /// <typeparam name="entity"></typeparam>
        /// <param name="id"></param>
        /// <returns>Số dòng ảnh hưởng. Nếu > 0 thì đã xóa thành công</returns>
        public ServiceResult delete<entity>(Guid id)
        {
            try
            {
                if (id != null)
                {
                    int check = baseRepository.deleteEntity<entity>(id);
                    if (check > 0)
                    {
                        serviceResult.code = statusCode.success;
                        serviceResult.message = Properties.resource.deleteSuccess;
                        serviceResult.data = check;
                    }
                    else
                    {
                        serviceResult.code = statusCode.fail;
                        serviceResult.message = Properties.resource.deleteFail;
                        serviceResult.data = check;
                    }
                }
            }
            catch (Exception e)
            {
                serviceResult.message = e.Message;
                serviceResult.isValid = false;
                serviceResult.data = e.Message;
                serviceResult.code = statusCode.exception;
            }
            return serviceResult;
        }

        /// <summary>
        /// created date: 23/06/2021
        /// created by: VXKHANH
        /// Hàm validate dữ liệu chung cho các hàm thêm sửa
        /// </summary>
        /// <typeparam name="entity"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public void baseValidate<entity>(entity data, bool isUpdate)
        {
            serviceResult.isValid = true;
            serviceResult.code = statusCode.success;
            var listError = new List<string>();

            var properties = data.GetType().GetProperties();    
            foreach(var property in properties)
            {
                string propertyName = property.Name;
                var propertyValue = property.GetValue(data);
                if (property.IsDefined(typeof(required), false))
                {
                    
                    if(propertyValue == null || propertyValue.ToString().Equals(""))
                    {
                        serviceResult.isValid = false;
                        listError.Add(propertyName + "is required!");
                        serviceResult.code = statusCode.notValid;
                        serviceResult.message = property.Name + Properties.resource.requiredData;
                    }
                }
                if(property.IsDefined(typeof(duplicate), false))
                {
                    if (isUpdate)
                    {
                        string primaryKeyName = typeof(entity).Name.ToLower()+"Id";

                        Guid papram = new Guid(data.GetType().GetProperty(primaryKeyName).GetValue(data).ToString());
                        //Lấy dữ liệu từ db thông qua id của đối tượng được truyền từ đầu hàm
                        entity newEntity = baseRepository.getById<entity>(papram);
                        //Giá trị của thuộc tính hiện tại
                        var curentValue = property.GetValue(data).ToString();
                        //Giá trị khóa chính được lấy trong db để so sánh 
                        var compareValue = newEntity.GetType().GetProperty(property.Name).GetValue(newEntity).ToString();
                        //So sánh xem dữ liệu có thay đổi hay là không 
                        if (curentValue.Equals(compareValue))
                        {
                            //Nếu bằng nhau .
                        }
                        else
                        {
                            entity checkingData = baseRepository.getByProperty<entity>(curentValue, property.Name);
                            if(checkingData != null)
                            {
                                serviceResult.isValid = false;
                                listError.Add(propertyName + " này đã tồn tại.");
                                serviceResult.code = statusCode.notValid;
                                
                            }
                        }
                    }
                }
            }
        }
    }
}
