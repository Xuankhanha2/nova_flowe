<template>
    <div class="revenue">
        <div class="grid">
            <div class="filter-line">
                <div class="filter-left">
                    
                </div>
                <!-- / -->

                <!--  -->
                <div class="filter-right">
                    <!-- Nút thêm  -->
                    <!-- <newButton 
                        :Text="'Thêm mới'"
                        :second="false"
                        @click.native="openForm(false, {})"
                        /> -->
                    <!-- / -->

                    <!-- Nút sắp xếp -->
                    <newButton
                        :Text="'Tải lại'"
                        :second="true"
                        @click.native="loadData()"
                        />
                    <div class="trash-btn"></div>
                    <!-- / -->
                </div>
                <!--  -->
            </div>

            <div class="grid-list">
                <div class="outline-table">
                <table cellspacing="0" cellpadding="0" border="0">
                    <!-- Dòng tiêu đề của bảng -->
                    <tr>
                        <!-- Cột chứa ô checkbox chọn dòng sản phẩm -->
                        <th colspan="1"> </th>
                        <!-- / -->

                        <!-- Mã hóa đơn -->
                        <th colspan="1" class="col-name-revenue">
                            <div class="th-text">Mã hóa đơn</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <!-- / -->

                        <!-- Tên khách hàng -->
                        <th colspan="1" class="col-name-revenue">
                            <div class="th-text">Khách hàng</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <!-- / -->

                        <!-- Số điện thoại khách hàng -->
                        <th colspan="1" class="col-group-revenue">
                            <div class="th-text">Số điện thoại</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <!-- / -->

                        <!-- Ngày lập hóa đơn -->
                        <th colspan="1" class="col-mount-revenue">
                            <div class="th-text">Ngày lập hóa đơn</div>
                        </th>
                        <!-- / -->
                        
                        <!-- Tổng tiền -->
                        <th colspan="1" class="col-mount-revenue">
                            <div class="th-text">Tổng tiền</div>
                        </th>
                        <!-- / -->

                        <!-- Trạng thái đơn hàng -->
                        <th colspan="1" class="td-to-check">Trạng thái</th>
                        <!-- / -->

                        <!-- Cột chứa các nút chức năng -->
                        <th colspan="1"></th>
                        <!-- / -->
                    </tr>

                    <!-- Các dòng dữ liệu -->
                    <tr v-for="order in orders" :key="order.orderId"
                        :class="{'row-focus':order.selectedItem}"
                    >
                        <!-- Ô checkbox đánh dấu dòng được chọn -->
                        <td colspan="1">
                            <div class="select-line"
                                :class="{'selected-line':order.selectedItem}"
                                @click="order.selectedItem = !order.selectedItem"
                            ></div>
                        </td>
                        <!-- / -->

                         <!-- Mã hóa đơn -->
                        <td colspan="1"  class="colLinked"
                            @click="openForm(true, order)"
                        >{{order.orderCode}}</td>
                        <!-- / -->

                        <!-- Tên khách hàng -->
                        <td colspan="1"
                        >{{order.customerName}}</td>
                        <!-- / -->

                        <!-- Số điện thoại khách hàng -->
                        <td colspan="1" class="">{{order.phoneNumber}}</td>
                        <!-- / -->

                         <!-- Ngày lập hóa đơn -->
                        <td colspan="1" class="">{{formatDate(order.createdDate)}}</td>
                        <!-- / -->

                        <!-- tổng tiền -->
                        <td colspan="1" class="td-to-check align-right-text" >
                            {{ formatMoney(getOrderTotal(order)) }} vnđ</td>
                        <!-- / -->

                        <!-- Trạng thái còn hàng -->
                        <td colspan="1" class="td-to-check">{{showStatus(order.status)}}</td>
                        <!-- / -->

                        <!-- Ô chức các nút chức năng  -->
                        <td colspan="1">
                            <div class="last-cell">
                                <!-- Sửa -->
                                <div class="btn-edit" title="Chỉnh sửa dòng dữ liệu này"
                                    @click="openForm(true, order)"
                                ></div>
                                <!-- / -->

                                <!-- Xóa -->
                                <div class="btn-delete" title="Xóa dòng dữ liệu này"
                                    @click="showDeletePopup(order.orderId)"
                                ></div>
                                <!-- / -->
                            </div>
                        </td>
                        <!-- / -->
                    </tr>
                </table>
                </div>
            </div>
            <div class="footer">
                <div class="result">
                    Tổng số: <span class="count">{{orders.length}}</span> kết quả.
                </div>
            </div>
        </div>

        <!-- Form thêm sửa -->
        <orderDetail
            v-if="showPopup"
            :isUpdate="isUpdate"
            :pageTitle="orderDetailTitle"
            :refOrder="this.order"
            :config="config"
            @closeForm="closeForm"
            @loadData="loadData"
        />
        <!-- / -->

        <!-- Popup xóa sản phẩm -->
        <popup 
            :text="'Bạn có muốn xóa sản phẩm này?'"
            v-if="deletePopup"
            :firstBtn="true"
            :secondBtn="true"
            @closePopup="closePopup"
            @negativeFunction="deleteOrder"
            :param="popupParam"
        />
        <!-- end Popup xóa sản phẩm -->

        <!-- Popup thông báo -->
        <popup
            :text="notifyText"
            :thirdBtn="true"
            @closePopup="closePopup"
            v-if="notifyPopup"
        />
        <!-- end Popup thông báo -->
        
        <!-- Ảnh loading của trang  -->
        <div class="preload" v-show="loading"></div>
    </div>
</template>

<script>
/*
    created by: vu xuan khanh
    created date: 12/7/2021
*/
import axios from 'axios'
import orderDetail from './orderDetail.vue'
import newButton from '../../layout/button.vue'
import popup from '../../popup/notifyPopup.vue'
import apiPath from '../../../path'
export default {
    data() {
        return {
            
            /**Biến  mảng chứa danh sách sản phẩm khi gọi api*/
            orders:[],
            /**Biến tiêu đề của popup thêm - sửa sản phẩm */
            orderDetailTitle: 'Hóa đơn bán hàng',
            /** Biến kiểm tra popup thêm sửa hiển thị lên là thêm hay sửa*/
            isUpdate:false,
            /**Biến mở - đóng popup thêm - sửa */
            showPopup: false,
            /**Biến bật tắt hiển thị danh sách sản phẩm ngừng kinh doanh*/
            stopBusiness: false,
            /**Biến lưu trữ 1 thông tin sản phẩm - phục vụ cho việc cập nhật dữ liệu*/
            order:{},
            // Biến dùng để hiển thị popup khi xóa
            deletePopup: false,
            //Biến hiển thị thông báo
            notifyPopup:false,
            //Biến chứa thông điệp khi thông báo
            notifyText: 'Đã có lỗi xảy ra.',
            //Biến dùng để truyền prop cho popup
            popupParam:'',
            //Biến sử dung để hiển thị loading khi load dữ liệu 
            loading: false,
            config: {},
            accepted: "Đã duyệt",
            denied: "Đã hủy",
            pendding: "Đang chờ duyệt"
        }
    },
    components:{
        orderDetail,
        newButton,
        popup
    },
    watch:{
        
    },

    methods:{
        /**Hàm sử dung để hiển thị các dòng được chọn trong bảng */
        selectRow(selectedItem){
            selectedItem = !selectedItem;
            return selectedItem;
        },
        /**Bảng mở form thêm - sửa sản phẩm */
        openForm(check, order){
            //nếu true  thì form mở lên là form sửa
            this.isUpdate = check;
            if(this.isUpdate){
                this.order = order;
            }
            else{
                this.order=null;
            }
            this.showPopup = true;
        },
        /**Hàm dùng để chỉnh lại định dạng ngày khi hiển thị */
        formatDate(datetime){
            let date = "";
            if (datetime)
                date = String(datetime).substring(0, 10).split("-").reverse().join("/");
            return date;
        },
        /**Hàm đóng form thêm sửa sản phẩm */
        closeForm(){
            this.showPopup = false;
        },
        /**Hàm hiển thị popup hỏi người dùng có xóa sản phẩm này hay ko */
        showDeletePopup(id){
            this.popupParam = id;
            this.deletePopup = true;
        },
        /**Hàm đóng popup */
        closePopup(){
            this.notifyText= "";
            this.deletePopup = false;
            this.notifyPopup = false;
        },
        /**Hàm xóa sản phẩm theo id */
        async deleteOrder(id){
            await axios.delete(apiPath.order+id, this.config).then((result)=>{
                this.notifyText = "Đã xóa hoá đơn vừa chọn.";
                this.notifyPopup = true;
                this.loadData();
                return result;
            }).catch(()=>{
                this.notifyText = "Không thể xóa sản phẩm này";
                this.notifyPopup = true;
            })
        },
        /**Hàm load lại trang sau khiu thêm sủa xóa  */
        async loadData(){
            this.loading = true;
            await axios.get(apiPath.order).then((response)=>{
                this.orders = response.data;
            }).catch(()=>{

            });
            this.loading = false;
        },
        /**
         * created date: 27/04/2022
         * created by: khanhvx
         * hàm lấy tokken từ localStorage
         */
        getToken(){
            if(localStorage.getItem('accessToken'))
                return localStorage.getItem('accessToken');
            else
                return "";
        },
        /**Hàm xử lý hiện thi trạng thái hóa đơn */
        showStatus(status){
            if(status < 0)
                return this.denied;
            if(status === 0)
                return this.pendding;
            if(status > 0)
                return this.accepted;
        },
        
        /**Hàm trả về tổng tiền của hóa đơn */
        getOrderTotal(order){
            let total = 0;
            if(order.orderDetails)
            {
                order.orderDetails.forEach((element) => {
                    total += element.quantity * element.price;
                })
            }
            return total;
        },

        /**Hàm format tiền */
        formatMoney(native){
            var newPrice = String(native);
            newPrice = newPrice.toString().split(".").join("");
            return newPrice.replace(/(\d)(?=(?:\d{3})+$)/g, '$1.');
        }
    },

    async created() {
        this.loading = true;
        this.config = {
            headers: { Authorization: `Bearer ${this.getToken()}` }
        }
        this.loadData();
        this.loading = false;
    },
}
</script>

<style lang="css">
    @import '../../../css/dictionary/product/productPage.css';
</style>