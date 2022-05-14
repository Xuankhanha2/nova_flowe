using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class OrderService: BaseService, IOrderService
    {
        IOrderRepository orderRepository;
        public OrderService(IOrderRepository _orderRepository, IBaseRepository _baseRepository) : base(_orderRepository)
        {
            orderRepository = _orderRepository;
        }

        /// <summary>
        /// created date: 6/5/2022
        /// created by: KHANHVX
        /// Lấy danh sách hóa đơn theo mã khách hàng
        /// </summary>
        /// <param name="customerId">customerId</param>
        /// <returns>ServiceResult</returns>
        public ServiceResult getByCustomerId(Guid customerId)
        {
            if (!validateId(customerId))
            {
                serviceResult.isValid = false;
                serviceResult.data = "customerId is required";
                serviceResult.code = statusCode.notValid;
            }
            else
            {
                try
                {
                    var data = orderRepository.getByCustomerId(customerId);
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
            }

            return serviceResult;
        }

        /// <summary>
        /// created date: 6/5/2022
        /// created by: KhanhVX
        /// Hàm lấy thông tin hóa đơn theo mã hóa đơn và mã khách hàng
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <param name="customerId">customerId</param>
        /// <returns></returns>
        public ServiceResult getOrdersOfCustomer(Guid orderId, Guid customerId)
        {
            if(!validateId(orderId))
            {
                serviceResult.isValid = false;
                serviceResult.data = "orderId is required";
                serviceResult.code = statusCode.notValid;
                return serviceResult;
            }
            else if (!validateId(customerId))
            {
                serviceResult.isValid = false;
                serviceResult.data = "customerId is required";
                serviceResult.code = statusCode.notValid;
                return serviceResult;
            }
            else
            {
                try
                {
                    var data = orderRepository.getOrdersOfCustomer(orderId, customerId);
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
            }
            
            return serviceResult;
        }
        /// <summary>
        /// created by: khanhvx
        /// created date 6/5/2022
        /// Hàm validate dữ liệu 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool validateId(Guid id)
        {
            if (id == null)
            {
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// created date: 12/5/2022
        /// created by: KhanhVX
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns></returns>
        public ServiceResult insertOrder(Order order)
        {
            try
            {
                //validate với trạng thái là insert
                this.validateOrder(order, false);
                if (serviceResult.isValid)
                {
                    int check = orderRepository.insertOrder(order);
                    if (check > 0)
                    {
                        serviceResult.code = statusCode.success;
                        serviceResult.message = Properties.resource.createdSuccess;
                        serviceResult.data = check;
                    }
                    else
                    {
                        serviceResult.code = statusCode.fail;
                        serviceResult.message = Properties.resource.createFail;
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
        /// created by: KhanhVX
        /// created date: 10/5/2022
        /// Hàm cập nhật thông tin hóa đơn
        /// </summary>
        /// <param name="orderDetail">orderDetail</param>
        /// <returns></returns>
        public ServiceResult updateOrder(OrderDetail orderDetail)
        {
            try
            {
                this.validateOrderDetail(orderDetail, false);
                if (serviceResult.isValid)
                {
                    int check = orderRepository.updateOrder(orderDetail);
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
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Hàm xóa hóa đơn 
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <returns></returns>
        public ServiceResult deleteOrder(Guid orderId)
        {
            try
            {
                if (orderId != null)
                {
                    int check = orderRepository.deleteOrder(orderId);
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
                else
                {
                    serviceResult.code = statusCode.notValid;
                    serviceResult.message = Properties.resource.deleteFail;
                    serviceResult.data = "orderId is required!";
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
        /// created date: 10/5/2022
        /// created by: KhanhVX
        /// Hàm xóa hóa đơn 
        /// </summary>
        /// <param name="orderDeailId">orderDeailId</param>
        /// <returns></returns>
        public ServiceResult deleteOrderDetail(Guid orderDeailId)
        {
            try
            {
                if (orderDeailId != null)
                {
                    int check = orderRepository.deleteOrderDetail(orderDeailId);
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
                else
                {
                    serviceResult.code = statusCode.notValid;
                    serviceResult.message = Properties.resource.deleteFail;
                    serviceResult.data = "orderDeailId is required!";
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
        /// Created date: 6/5/2022
        /// created by: KhanhVX
        /// Hàm validate dữ liệu cho hóa đơn
        /// </summary>
        /// <param name="order">order</param>
        /// <param name="isUpdate"></param>
        public void validateOrder(Order order, bool isUpdate)
        {
            if (isUpdate)
            {
                return;
            }
            else
            {
                if(order.customerId == null || order.customerId.ToString().Trim().Equals(""))
                {
                    serviceResult.isValid = false;
                    serviceResult.code = statusCode.notValid;
                    serviceResult.message = "customerId is required!";
                }
            }
        }

        public void validateOrderDetail(OrderDetail orderDetail, bool isUpdate)
        {
            serviceResult.isValid = true;
            if (isUpdate)
            {
                return;
            }
            else
            {
                if (orderDetail.orderId == null || orderDetail.orderId.ToString().Trim().Equals(""))
                {
                    serviceResult.isValid = false;
                    serviceResult.code = statusCode.notValid;
                    serviceResult.message = "orderId is required!";
                }
                if (orderDetail.productId == null || orderDetail.productId.ToString().Trim().Equals(""))
                {
                    serviceResult.isValid = false;
                    serviceResult.code = statusCode.notValid;
                    serviceResult.message = "productId is required!";
                }
                if (orderDetail.quantity <= 1)
                {
                    serviceResult.isValid = false;
                    serviceResult.code = statusCode.notValid;
                    serviceResult.message = "quantity must euqal or above 1!";
                }
                if (orderDetail.quantity.Equals(null) || orderDetail.quantity.GetType() != typeof(int))
                {
                    serviceResult.isValid = false;
                    serviceResult.code = statusCode.notValid;
                    serviceResult.message = "Quantity is required!";
                }
            }
        }
    }
}
