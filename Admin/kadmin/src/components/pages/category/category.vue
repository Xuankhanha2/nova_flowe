<template>
    <div class="revenue">
        <div class="grid">
            <div class="filter-line">
                <div class="filter-left">
                    
                </div>
                <div class="filter-right">
                    <newButton 
                        :Text="'Thêm mới'"
                        :second="false"
                        @click.native="openForm(false, {})"
                        />
                    <newButton
                    :Text="'Sắp lại thứ tự'"
                        :second="true"
                        />
                    <div class="trash-btn"></div>
                </div>
            </div>

            <div class="grid-list">
                <div class="outline-table">
                <table cellspacing="0" cellpadding="0" border="0">
                    <!-- Dòng tiêu đề của bảng -->
                    <tr>
                        <th colspan="1"> </th>
                        <th colspan="1" class="col-name-revenue">
                            <div class="th-text">Tên danh mục</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <th colspan="1" class="col-group-revenue">
                            <div class="th-text">Thuộc danh mục</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <!-- <th colspan="1" class="col-mount-revenue">
                            <div class="th-text">Giá sản phẩm</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <th colspan="1" class="col-mount-revenue">
                            <div class="th-text">Nhà cung cấp</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <th colspan="1" class="td-to-check">Giảm giá</th>
                        <th colspan="1" class="td-to-check">Sản phẩm hot</th>-->
                        <th colspan="1" class="td-to-check">Icon</th> 
                        <th colspan="1" class="maxWidth100"></th>
                    </tr>
                    <!-- Các dòng dữ liệu -->
                    <tr v-for="category in categories" :key="category.categoryId"
                        :class="{'row-focus':category.selectedItem}"
                    >
                        <td colspan="1">
                            <div class="select-line"
                                :class="{'selected-line':category.selectedItem}"
                                @click="category.selectedItem = !category.selectedItem"
                            ></div>
                        </td>
                        
                        <td colspan="1" class="colLinked"
                            @click="openForm(true, category)"
                        >{{category.categoryName}}</td>
                        <td colspan="1" class="">{{bindCategoryName(category.parentId)}}</td>
                        <td colspan="1" class="td-to-check">{{category.categoryIcon}}</td>
                        <td colspan="1" class="maxWidth100">
                            <div class="last-cell">
                                <div class="btn-edit" title="Chỉnh sửa dòng dữ liệu này"
                                    @click="openForm(true, category)"
                                ></div>
                                <div class="btn-delete" title="Xóa dòng dữ liệu này"
                                    @click="showDeletePopup(category.categoryId)"
                                ></div>
                            </div>
                        </td>

                    </tr>
                </table>
                </div>
            </div>
            <div class="footer">
                <div class="result">
                    Tổng số: <span class="count">{{categories.length}}</span> kết quả.
                </div>
            </div>
        </div>
        <categoryDetail
            @loadData="loadData"
            v-if="showPopup"
            :isUpdate="isUpdate"
            :pageTitle="categoryDetailTitle"
            :refCategories="this.categories"
            :refCategory="this.category"
            :config="config"
            @closeForm="closeForm"
        />
        <!-- Popup xóa danh mục -->
        <popup 
            :text="'Bạn có muốn xóa danh mục này?'"
            v-if="deletePopup"
            :firstBtn="true"
            :secondBtn="true"
            @closePopup="closePopup"
            @negativeFunction="deleteCategory"
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
    created date: 27/7/2021
*/
import axios from 'axios'
import categoryDetail from './categoryDetail.vue'
import newButton from '../../layout/button.vue'
import popup from '../../popup/notifyPopup.vue'
import apiPath from '../../../path'
export default {
    data() {
        return {
           /**Biến tiêu đề của popup thêm - sửa sản phẩm */
           categoryDetailTitle: '',
           /** Biến kiểm tra popup thêm sửa hiển thị lên là thêm hay sửa*/
           isUpdate:false,
           /**Biến mở - đóng popup thêm - sửa */
           showPopup: false,
           //Biến dùng để lưu danh sách danh mục sản phẩm, sử dụng để tải dữ liệu vào ô chọn danh mục sản phẩm
            categories: [{}],
            //Biến hiển thị thông báo hỏi khi xóa sản phẩm
            deletePopup: false,
            //Biến hiển thị thông báo
            notifyPopup:false,
            //Biến chứa thông điệp khi thông báo
            notifyText: 'Đã có lỗi xảy ra.',
            //Biến dùng để truyền prop cho popup
            popupParam:'',
            //Biến category dùng để truyền prop cho form cập nhật danh mục sản phẩm
            category: {},
            //Biến sử dung để hiển thị loading khi load dữ liệu 
            loading: false,
            config: {},
        }
    },
    components:{
        categoryDetail,
        newButton,
        popup
    },
    watch:{
        
    },

    methods:{
        /**Bảng mở form thêm - sửa sản phẩm */
        openForm(check, category){
            //nếu true  thì form mở lên là form sửa
            this.isUpdate = check;
            if(this.isUpdate){
                this.categoryDetailTitle="Cập nhật danh mục"
                this.category = category;
            }   
            else{
                this.categoryDetailTitle="Thêm danh mục mới"
                this.category = null;
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
        //Hàm hiển thị tên danh mục cha của sản phẩm
        bindCategoryName(categoryId){
            var name
            this.categories.forEach(element => {
                if(element.categoryId == categoryId){
                    name = element.categoryName;
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
        async deleteCategory(id){
            await axios.delete(apiPath.categories+id, this.config).then((result)=>{
                this.notifyText = "Đã xóa sản phẩm vừa chọn.";
                this.notifyPopup = true;
                console.log(result.data);
                this.loadData();
                return result;
            }).catch(()=>{
                this.notifyText = "Không thể xóa sản phẩm này";
                this.notifyPopup = true;
            })
        },
        /**Hàm load lại dữ liêu sau khi thêm sửa xóa */
        async loadData(){
            this.loading = true;
            await axios.get(apiPath.categories).then((result)=>{
                this.categories = result.data;
            }).catch(()=>{
                console.log('Đã có lỗi xảy ra khi lấy categories');
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