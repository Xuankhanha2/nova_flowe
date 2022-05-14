<template>
    <div class="revenue">
        <div class="grid">
            <div class="filter-line">
                <!-- -->
                <div class="filter-left">
                    
                </div>
                <!-- / -->

                <!--  -->
                <div class="filter-right">
                    <!-- Nút thêm  -->
                    <newButton 
                        :Text="'Thêm mới'"
                        :second="false"
                        @click.native="openForm(false, {})"
                    />
                    <!-- / -->

                    <!-- Nút sắp xếp -->
                    <newButton
                    :Text="'Sắp lại thứ tự'"
                        :second="true"
                    />
                    <div class="trash-btn"></div>
                    <!-- / -->
                </div>
                <!--  -->
            </div>

            <div class="grid-list">
                <div class="outline-table scrollbar">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <!-- Dòng tiêu đề của bảng -->
                        <tr>
                            <!-- Cột chứa ô checkbox chọn dòng sản phẩm -->
                            <th colspan="1"> </th>
                            <!-- / -->

                            <!-- Tên khách hàng -->
                            <th colspan="1" class="col-name-revenue">
                                <div class="th-text">Tên khách hàng</div>
                                <div class="btn-filter"><input type="text"></div>
                            </th>
                            <!-- / -->

                            <!-- Ngày sinh -->
                            <th colspan="1" class="td-to-check">Ngày sinh</th>
                            <!-- / -->

                            <!-- Giới tính -->
                            <th colspan="1" class="td-to-check">Giới tính</th>
                            <!-- / -->

                            <!-- Email -->
                            <th colspan="1" class="col-group-revenue">
                                <div class="th-text">Email</div>
                                <div class="btn-filter"><input type="text"></div>
                            </th>
                            <!-- / -->

                            <!-- Số điện thoại -->
                            <th colspan="1" class="col-group-revenue">
                                <div class="th-text">Số điện thoại</div>
                                <div class="btn-filter"><input type="text"></div>
                            </th>
                            <!-- / -->

                            <!-- Địa chỉ -->
                            <th colspan="1" class="col-group-revenue">
                                <div class="th-text">Địa chỉ</div>
                                <div class="btn-filter"><input type="text"></div>
                            </th>
                            <!-- / -->

                            <!-- Ghi chú -->
                            <th colspan="1" class="col-mount-revenue">
                                <div class="th-text">Ghi chú</div>
                                <div class="btn-filter"><input type="text"></div>
                            </th>
                            <!-- / -->

                            <!-- Cột chứa các nút chức năng -->
                            <th colspan="1"></th>
                            <!-- / -->
                        </tr>

                        <!-- Các dòng dữ liệu -->
                        <tr v-for="customer in customers" :key="customer.customerId"
                            :class="{'row-focus':customer.selectedItem}"
                        >
                            <!-- Ô checkbox đánh dấu dòng được chọn -->
                            <td colspan="1">
                                <div class="select-line"
                                    :class="{'selected-line':customer.selectedItem}"
                                    @click="customer.selectedItem = !customer.selectedItem"
                                ></div>
                            </td>
                            <!-- / -->

                            <!-- Tên khách hàng -->
                            <td colspan="1" class="colLinked"
                                @click="openForm(true, customer)"
                            >{{customer.customerName}}</td>
                            <!-- / -->

                            <!-- Ngày sinh -->
                            <td colspan="1" class="td-to-check align-right-text">
                                {{formatDate(String(customer.dateOfBirth))}}
                            </td>
                            <!-- / -->

                            <!-- Giới tính -->
                            <td colspan="1" class="">{{formatGender(customer.gender)}}</td>
                            <!-- / -->

                            <!-- Email -->
                            <td colspan="1" class="td-to-check">{{customer.email}}</td>
                            <!-- / -->

                            <!-- Số điện thoại -->
                            <td colspan="1" class="td-to-check">{{customer.phoneNumber}}</td>
                            <!-- / -->

                            <!-- Địa chỉ -->
                            <td colspan="1" class="td-to-check">{{customer.address}}</td>
                            <!-- / -->

                            <!-- Ghi chú -->
                            <td colspan="1" class="td-to-check">{{customer.note}}</td>
                            <!-- / -->

                            <!-- Ô chức các nút chức năng  -->
                            <td colspan="1">
                                <div class="last-cell">
                                    <!-- Sửa -->
                                    <div class="btn-edit" title="Chỉnh sửa dòng dữ liệu này"
                                        @click="openForm(true, customer)"
                                    ></div>
                                    <!-- / -->

                                    <!-- Xóa -->
                                    <div class="btn-delete" title="Xóa dòng dữ liệu này"
                                        @click="showDeletePopup(customer.customerId)"
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
                    Tổng số: <span class="count">{{customers.length}}</span> kết quả.
                </div>
            </div>
        </div>

        <!-- Form thêm sửa -->
        <customerDetail
            v-if="showPopup"
            :isUpdate="isUpdate"
            :pageTitle="customerDetailTitle"
            :refCustomer="this.customer"
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
            @negativeFunction="deleteCustomer"
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
import customerDetail from './customerDetail.vue'
import newButton from '../../layout/button.vue'
import popup from '../../popup/notifyPopup.vue'
import apiPath from '../../../path'
export default {
    data() {
        return {
            
            /**Biến  mảng chứa danh sách khách hàng khi gọi api*/
            customers:[],
            /**Biến tiêu đề của popup thêm - sửa khách hàng */
            customerDetailTitle: '',
            /** Biến kiểm tra popup thêm sửa hiển thị lên là thêm hay sửa*/
            isUpdate:false,
            /**Biến mở - đóng popup thêm - sửa */
            showPopup: false,
            /**Biến bật tắt hiển thị danh sách sản phẩm ngừng kinh doanh*/
            stopBusiness: false,
            /**Biến lưu trữ thông tin 1 khách hàng - phục vụ cho việc cập nhật dữ liệu*/
            customer:{},
            //Biến hiển thị thông báo hỏi khi xóa sản phẩm
            deletePopup: false,
            //Biến hiển thị thông báo
            notifyPopup:false,
            //Biến chứa thông điệp khi thông báo
            notifyText: 'Đã có lỗi xảy ra.',
            //Biến dùng để truyền prop cho popup
            popupParam:'',
            //Biến sử dung để hiển thị loading khi load dữ liệu 
            loading: false,
            config: {}
        }
    },
    components:{
        customerDetail,
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
        /**Bảng mở form thêm - sửa khách hàng */
        openForm(check, customer){
            //nếu true  thì form mở lên là form sửa
            this.isUpdate = check;
            if(this.isUpdate){
                this.customer=customer;
                this.customerDetailTitle="Cập nhật thông tin sản phẩm";
            }
            else{
                this.customer=null;
                this.customerDetailTitle="Thêm sản phẩm";
            }
            this.showPopup = true;
        },
        /**Hàm đóng form thêm sửa khách hàng */
        closeForm(){
            this.showPopup = false;
        },
        /**Hàm hiển thị nhứng sản phẩn đã ngừng theo dõi */
        showStopBusiness(){ 
            this.stopBusiness = !this.stopBusiness;
        },
        /**Hàm hiển thị popup hỏi người dùng có xóa khách hàng ko */
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
        /**Hàm xóa khách hàng theo id */
        async deleteCustomer(id){
            await axios.delete(apiPath.customers+id, this.config).then((result)=>{
                this.notifyText = "Đã xóa sản phẩm vừa chọn.";
                this.notifyPopup = true;
                this.loadData();
                console.log(result.data);
                return result;
            }).catch(()=>{
                this.notifyText = "Không thể xóa sản phẩm này";
                this.notifyPopup = true;
            })
        },
        /**Hàm load lại trang sau khi thêm sủa xóa  */
        async loadData(){
            this.loading = true;
            await axios.get(apiPath.customers).then((response)=>{
                this.customers = response.data;
            }).catch(()=>{
                console.log("Có lỗi xảy ra khi gọi api product");
            });
            this.loading = false;
        },
        /**Hàm định dạng ngày */
        formatDate(datetime){
            if(datetime != null){
                var date = datetime.substr(0, 10);
                date = date.split("-");
                var formatedDate = [date[2],date[1],date[0]].join("/");
                return formatedDate;
            }
            return "";
        },
        /**Hàm format giới tính */
        formatGender(gender){
            if(gender == 0){
                return "Nữ";
            }else if(gender == 1){
                return "Nam";
            }else{
                return "Khác";
            }
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
        }
    },

    async created() {
        this.config = {
            headers: { Authorization: `Bearer ${this.getToken()}` }
        }
        this.loadData();
    },
}
</script>

<style lang="css">
    @import '../../../css/dictionary/product/productPage.css';
</style>