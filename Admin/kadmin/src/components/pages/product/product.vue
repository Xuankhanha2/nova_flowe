<template>
    <div class="revenue">
        <div class="grid">
            <div class="filter-line">
                <!-- checkbox hiển thị sản phẩm ngừng kinh doanh -->
                <div class="filter-left">
                    <!-- <div class="select-line"
                        :class="{'selected-line':stopBusiness}"
                        @click="showStopBusiness"
                    ></div>
                    <label for="">Hiển thị sản phẩm ngừng kinh doanh</label> -->
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
                <div class="outline-table">
                <table cellspacing="0" cellpadding="0" border="0">
                    <!-- Dòng tiêu đề của bảng -->
                    <tr>
                        <!-- Cột chứa ô checkbox chọn dòng sản phẩm -->
                        <th colspan="1"> </th>
                        <!-- / -->

                        <!-- Ảnh sản phẩm -->
                        <th colspan="1" class="td-to-check">Ảnh sản phẩm</th>
                        <!-- / -->

                        <!-- Tên sản phẩm -->
                        <th colspan="1" class="col-name-revenue">
                            <div class="th-text">Tên sản phẩm</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <!-- / -->

                        <!-- Danh mục sản phẩm -->
                        <th colspan="1" class="col-group-revenue">
                            <div class="th-text">Danh mục sản phẩm</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <!-- / -->

                        <!-- Giá sản phẩm -->
                        <th colspan="1" class="col-mount-revenue">
                            <div class="th-text">Giá sản phẩm</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <!-- / -->

                        <!-- Nhà cung cấp -->
                        <th colspan="1" class="col-mount-revenue">
                            <div class="th-text">Nhà cung cấp</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <!-- / -->
                        
                        <!-- Giảm giá -->
                        <th colspan="1" class="td-to-check">Giảm giá</th>
                        <!-- / -->

                        <!-- Sản phẩm hot -->
                        <th colspan="1" class="td-to-check">Sản phẩm hot</th>
                        <!-- / -->

                        <!-- Trạng thái còn hàng -->
                        <th colspan="1" class="td-to-check">Trạng thái</th>
                        <!-- / -->

                        <!-- Cột chứa các nút chức năng -->
                        <th colspan="1"></th>
                        <!-- / -->
                    </tr>

                    <!-- Các dòng dữ liệu -->
                    <tr v-for="product in products" :key="product.productId"
                        :class="{'row-focus':product.selectedItem}"
                    >
                        <!-- Ô checkbox đánh dấu dòng được chọn -->
                        <td colspan="1">
                            <div class="select-line"
                                :class="{'selected-line':product.selectedItem}"
                                @click="product.selectedItem = !product.selectedItem"
                            ></div>
                        </td>
                        <!-- / -->

                        <!-- Ảnh sản phẩm -->
                        <td colspan="1" class="imageCell">
                            <img :src="product.image" />
                        </td>
                        <!-- / -->

                        <!-- Tên sản phẩm -->
                        <td colspan="1" class="colLinked"
                            @click="openForm(true, product)"
                        >{{product.productName}}</td>
                        <!-- / -->

                        <!-- Danh mục sản phẩm -->
                        <td colspan="1" class="">{{bindCategoryName(product.categoryId)}}</td>
                        <!-- / -->

                        <!-- Giá sản phẩm -->
                        <td colspan="1" class="td-to-check align-right-text" >{{product.price}} vnđ</td>
                        <!-- / -->

                        <!-- Nhà cung cấp -->
                        <td colspan="1" class="td-to-check">{{bindProviderName(product.providerId)}}</td>
                        <!-- / -->

                        <!-- Giảm giá -->
                        <td colspan="1" class="td-to-check">{{product.discount}}%</td>
                        <!-- / -->

                        <!-- Sản phẩm hot -->
                        <td colspan="1" class="td-to-check"><div :class="{'cell-checking':product.hot}"></div></td>
                        <!-- / -->

                        <!-- Trạng thái còn hàng -->
                        <td colspan="1" class="td-to-check">{{product.status}}</td>
                        <!-- / -->

                        <!-- Ô chức các nút chức năng  -->
                        <td colspan="1">
                            <div class="last-cell">
                                <!-- Sửa -->
                                <div class="btn-edit" title="Chỉnh sửa dòng dữ liệu này"
                                    @click="openForm(true, product)"
                                ></div>
                                <!-- / -->

                                <!-- Xóa -->
                                <div class="btn-delete" title="Xóa dòng dữ liệu này"
                                    @click="showDeletePopup(product.productId)"
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
                    Tổng số: <span class="count">{{products.length}}</span> kết quả.
                </div>
            </div>
        </div>

        <!-- Form thêm sửa -->
        <productDetail
            v-if="showPopup"
            :isUpdate="isUpdate"
            :pageTitle="productDetailTitle"
            :refProduct="this.product"
            :refCategories="this.categories"
            :refProviders="this.providers"
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
            @negativeFunction="deleteProduct"
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
import productDetail from './productDetail.vue'
import newButton from '../../layout/button.vue'
import popup from '../../popup/notifyPopup.vue'
import apiPath from '../../../path'
export default {
    data() {
        return {
            
           /**Biến  mảng chứa danh sách sản phẩm khi gọi api*/
           products:[],
           /**Biến tiêu đề của popup thêm - sửa sản phẩm */
           productDetailTitle: '',
           /** Biến kiểm tra popup thêm sửa hiển thị lên là thêm hay sửa*/
           isUpdate:false,
           /**Biến mở - đóng popup thêm - sửa */
           showPopup: false,
           /**Biến bật tắt hiển thị danh sách sản phẩm ngừng kinh doanh*/
           stopBusiness: false,
           /**Biến lưu trữ 1 thông tin sản phẩm - phục vụ cho việc cập nhật dữ liệu*/
           product:{},
           //Biến dùng để lưu danh sách danh mục sản phẩm, sử dụng để tải dữ liệu vào ô chọn danh mục sản phẩm
            categories: [{}],
            //Biến providers chứa danh sách cửa hàng, dùng để đưa dữ liêu vào ô chọn cửa hàng cung cấp - nhà phân phối sp
            providers: [{}],
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
        productDetail,
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
        openForm(check, product){
            //nếu true  thì form mở lên là form sửa
            this.isUpdate = check;
            if(this.isUpdate){
                this.product=product;
                this.productDetailTitle="Cập nhật thông tin sản phẩm";
            }
            else{
                this.product=null;
                this.productDetailTitle="Thêm sản phẩm";
            }
            this.showPopup = true;
        },
        /**Hàm đóng form thêm sửa sản phẩm */
        closeForm(){
            this.showPopup = false;
        },
        /**Hàm hiển thị nhứng sản phẩn đã ngừng theo dõi */
        showStopBusiness(){
            this.stopBusiness = !this.stopBusiness;
        },
        /**Hàm hiển thị tên danh mục lên lưới thông qua id */
        bindCategoryName(categoryId){
            var name
            this.categories.forEach(element => {
                if(element.categoryId == categoryId){
                    name = element.categoryName;
                }
            });
            return name;
        },
        /**Hàm hiển thị tên cửa hàng - nhà cung cấp sản phẩm */
        bindProviderName(providerId){
            var name;
            this.providers.forEach(element => {
                if(element.providerId == providerId){
                    name = element.providerName;
                }
            });
            return name;
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
        async deleteProduct(id){
            await axios.delete(apiPath.products+id, this.config).then((result)=>{
                this.notifyText = "Đã xóa sản phẩm vừa chọn.";
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
            await axios.get(apiPath.products).then((response)=>{
                this.products = response.data;
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
        }

    },

    async created() {
        this.loading = true;
        this.config = {
            headers: { Authorization: `Bearer ${this.getToken()}` }
        }
        this.products = await axios.get(apiPath.products).then((response)=>{
            return response.data;
        }).catch(()=>{
            
        });
        this.categories = await axios.get(apiPath.categories).then((result)=>{
            return result.data;
        }).catch(()=>{
            
        });

        this.providers = await axios.get(apiPath.providers).then((result)=>{
            return result.data;
        }).catch(()=>{
            
        });
        this.loading = false;
    },
}
</script>

<style lang="css">
    @import '../../../css/dictionary/product/productPage.css';
</style>