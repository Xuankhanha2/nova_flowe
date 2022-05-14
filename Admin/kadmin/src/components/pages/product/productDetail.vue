<template>
    <div class="popup">
        <div class="form">
            <!-- <button @click="ValideteForm()">Load</button> -->
            <div class="form-title">
                {{pageTitle}}
            </div>
            <div class="form-content">
                <div class="form-left">
                <!-- Tên của sản phẩm -->
                    <newLabel :text="'Tên sản phẩm'" 
                        :required="true"/>
                    <div class="input">
                        <input type="text" 
                            class="textbox margin-r-16"
                            v-model="cloneProduct.productName"
                            @keyup="required.productName = false"
                        >
                        <div class="tooltipCustom" 
                            v-show="required.productName">
                                Dữ liệu không được để trống!
                        </div>
                    </div>
                <!-- end Tên của sản phẩm -->

                <!-- Danh mục sản phẩm -->
                    <newLabel :text="'Thuộc danh mục'"
                        :required="true"
                    />
                    <div class="form-left-row">
                        <select v-model="cloneProduct.categoryId"
                            @change="required.category = false"
                            class="margin-r-16"
                        >
                            <option class="select-item"
                                v-for="category in refCategories"
                                :key="category.categoryId"
                                :value="category.categoryId"
                            >{{category.categoryName}}</option>
                        </select>
                        <!-- <div class="btn-plus"></div> -->
                        <div class="tooltipCustom" 
                            v-show="required.category">
                                Bạn chưa chọn đanh mục nào!
                        </div>
                    </div>
                <!-- end Danh mục của sản phẩm -->

                <!-- Giá của sản phẩm  -->
                    <newLabel :text="'Giá của sản phẩm'" 
                        :required="true"/>
                    <div class="input">
                        <!-- Ô nhập dữ liệu -->
                        <input type="text" 
                            style="text-align: right"
                            ref="feeName" 
                            class="textbox margin-r-16"
                            :class="{'required-outline':false}" 
                            v-model="cloneProduct.price"
                            @keyup="formatMoney(); required.price = false"
                        >
                        <!-- Tooltip thông báo khi dữ liệu của trường này bị để trống khi gửi -->
                        <div class="tooltipCustom" 
                            v-show="required.price">
                                Dữ liệu không được để trống!
                        </div>
                    </div>
                <!-- end Giá của sản phẩm -->

                <!--Giảm giá  -->
                    <newLabel :text="'Giảm giá'" 
                        :required="false"/>
                    <div class="input">
                        <!-- Ô nhập dữ liệu -->
                        <input type="text" 
                            ref="feeName" 
                            class="textbox margin-r-16 percentEnd"
                            :class="{'required-outline':false}"
                            v-model="cloneProduct.discount"
                        >
                        <!-- Tooltip thông báo khi dữ liệu của trường này bị để trống khi gửi -->
                        <div class="tooltipCustom" 
                            v-show="false">
                                Dữ liệu không được để trống!
                        </div>
                    </div>
                <!-- end Giảm giá -->

                <!-- Nhà cung cấp sản phẩm -->
                    <newLabel :text="'Nhà cung cấp sản phẩm'" :required="true"/>
                    <div class="form-left-row ">
                        <!-- Ô Chọn dữ liệu  -->
                        <select class="mar-r-16px radius"
                            :class="{'required-outline':false}"
                            v-model="cloneProduct.providerId"
                        >
                            <option 
                                v-for="provider in refProviders"
                                :key="provider.providerId"
                                :value="provider.providerId"
                            >{{provider.providerName}}</option>
                        </select>
                        <!-- Tooltip thông báo khi dữ liệu của trường này bị để trống khi gửi -->
                        <div class="tooltipCustom" v-show="false">Dữ liệu không được để trống!</div>
                    </div>
                <!-- end Nhà cung cấp sản phẩm -->

                </div>
                <div class="form-right">
                    <!-- Ảnh sản phẩm -->
                    <div class="form-right-top-content">
                        <img :src="cloneProduct.image">
                        <div id="inputLinkImage">
                            <!-- <input type="file" id="selectImage"> -->
                            <label>Link ảnh sản phẩm</label>
                            <input type="text" class="textbox"
                                v-model="cloneProduct.image"
                            >
                        </div>
                    </div>
                    <!-- / -->

                    <!-- Mô tả về sản phẩm -->
                    <div class="form-right-middle-content">
                        <newLabel :text="'Mô tả về sản phẩm'" 
                            :required="false"/>
                        <div class="input">
                            <textarea
                                class="textbox"
                                v-model="cloneProduct.description"
                            >
                                
                            </textarea>
                            <div class="tooltipCustom" 
                                v-show="false">
                                    Dữ liệu không được để trống!
                            </div>
                        </div>
                    </div>
                    <!-- / -->

                    <!-- Sản phẩm hot -->
                    <div class="form-right-bottom-content">
                        <div class="form-r-col">
                            <div class="row-check">
                                <input type="checkbox" 
                                    v-model="cloneProduct.hot"
                                >
                                <label></label>
                                <div class="col-row-text">
                                    Sản phẩm hot
                                </div>
                            </div>
                        </div>
                        <div class="form-r-col">
                            <!--  -->
                        </div>
                    </div>
                    <!-- / -->
                </div>
            </div>
            <div class="form-footer">
                <div class="f-footer-left">
                    <div class="row-check non-margin">
                        <!-- <input type="checkbox">
                        <label></label>
                        <div class="col-row-text">
                            Ngừng kinh doanh
                        </div> -->
                    </div>
                </div>
                <div class="f-footer-right">
                    <newButton
                        :Text="'Đóng'"
                        :second="true"
                        @click.native="closeForm()"
                    />
                    <!-- <newButton 
                    :Text="'Lưu và thêm'"
                    :second="false"
                    v-if="isUpdate==false"
                    /> -->
                    <newButton 
                        :Text="'Lưu'"
                        :second="false"
                        @click.native="processData"
                    />
                </div>
            </div>
            <div class="btn-close-form"
                @click="closeForm()"
            ></div>
        </div>
        <div class="blur">

        </div>
        <newPopup  
            :thirdBtn="true"
            :text="notifyText"
            v-if="popup"
            @closePopup="closePopup"
        />
    </div>
</template>

<script>
/**
    created by: vu xuan khanh
    created date: 15/7/2021
*/
import newLabel from '../../layout/label.vue'
import newButton from '../../layout/button.vue'
import axios from 'axios'
import newPopup from '../../popup/notifyPopup.vue'
import apiPath from '../../../path'
export default {
    props:{
        pageTitle:String,
        focusOn: Boolean,
        isUpdate: Boolean,
        refProduct: Object,
        refCategories: [],
        refProviders: [],
        config: Object,
    },
    data() {
        return {
            //Biến product lưu giá trị của sản phẩm dược cập nhật
            cloneProduct:{
                productId: "00000000-0000-0000-0000-000000000000",
                categoryId: "00000000-0000-0000-0000-000000000000",
                createdBy: "",
                createdDate: null,
                description: "",
                discount: 0,
                hot: true,
                image: "",
                modifiedDate: null,
                price: "",
                productName: "",
                providerId: "00000000-0000-0000-0000-000000000000",
                selectedItem: false,
                status: false
            },
            /**Biến kiểm tra các ô không được để dữ liêu trống */
            required:{
                productName:false,
                category:false,
                price:false,

            },
            notifyText: '',
            /*Biến hiển thị thông báo*/
            popup: false,
            
        }
    },
    beforeUpdate(){
        
    },
    components:{
        // Textbox,
        newLabel,
        newButton,
        newPopup
    },
    methods:{
        // Save:async function(){}
        //Hàm dùng để đóng form khi nhấn vào nút đóng
        closeForm(){
            this.$emit('closeForm');
        },
        /**Hàm xử lý cho nút gửi dữ liệu  bào gồm cả thêm và sửa*/
        async processData(){
            // var rowAffect = 0;
            var processResult;
            //Validate dữ liệu
            var isValid = this.validateData();
            //Kiem tra dữ liệu hợp lệ. Nếu hợp lệ thì thực hiện thêm - sửa
            if(isValid){
                if(this.isUpdate){
                    //Thục hiện sửa
                    await axios.put(apiPath.products, this.cloneProduct, this.config).then((result)=>{
                        processResult = result.data;
                        //Hiển thị kết quả sau khi xử lý
                        this.showNotifyPopup(processResult.message);
                        this.loadData();
                    }).catch(()=>{
                        this.showNotifyPopup("Đã có lỗi xảy ra.");
                    })
                    
                }else{
                    //Thực hiện thêm
                    await axios.post(apiPath.products, this.cloneProduct, this.config).then((result)=>{
                        processResult = result.data;
                        //Hiển thị kết quả sau khi xử lý
                        this.notifyText = processResult.message;
                        this.popup = true;
                        this.loadData();
                    }).catch(()=>{
                        this.showNotifyPopup("Đã có lỗi xảy ra.");
                    })
                    
                }
            }else{
                //Nếu dữ liệu chưa hợp lệ thì kết thúc hàm bằng lệnh return 
                return;
            }
            
        },
        /**Hàm xử lý nghiệp vụ cho các ô nhập liệu*/
        validateData(){
            var name = this.cloneProduct.productName;
            var category = this.cloneProduct.categoryId;
            var price = this.cloneProduct.price;
            var discount = this.cloneProduct.discount;
            //Xóa khoảng trắng đầu cuối  của dữ liệu 

            //Thực hiện kiểm tra dữ liêu hợp lệ
            var isValid = true;
            if(name == "" | name == null | name == undefined){
                isValid = false;
                this.required.productName = true;
            }else{
                this.required.productName = false;
            }
            //Validate danh mục 
            if(category == "" | category == null | category == undefined){
                isValid = false;
                this.required.category = true;
            }else{
                this.required.category = false;
            }
            //Validate giá tiền
            if(price == "" | price == null | price == undefined | isNaN(price)){
                isValid = false;
                this.required.price = true;
            }else{
                this.required.price = false;
                price = price.toString().split(".").join("");
                this.cloneProduct.price = parseInt(price);
            }
            //Validate phần trăn giảm giá
            if(discount == "" | discount == null | discount == undefined){
                this.cloneProduct.discount = 0;
            }else{
                this.cloneProduct.discount = parseFloat(discount)
            }
            return isValid;
            
        },
        /**Hàm format số tiền */
        formatMoney(){
            var newPrice = String(this.cloneProduct.price);
            newPrice = newPrice.toString().split(".").join("");
            this.cloneProduct.price = newPrice.replace(/(\d)(?=(?:\d{3})+$)/g, '$1.');
        },
        /*Hàm xử lý sự kiện đóng popup thông báo*/
        closePopup(){
            this.notifyText = "";
            this.popup = false;
            this.closeForm();
        },
        /**Hàm load dữ liệu khi thêm hoặc sủa trên form */
        loadData(){
            this.$emit('loadData');
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
        /**
         * created by: khanhvx
         * created date: 28/04/2022
         * Hàm thông báo lỗi
         */
        showNotifyPopup(msg){
            this.popup = true;
            this.notifyText = msg;
        },
            
    },
    created() {
        if(this.refProduct){
            this.cloneProduct={...this.refProduct};
            this.formatMoney();
        }
        //Format tiền ngay khi form được bật lên
        this.formatMoney();
    },
}
</script>

<style lang="css" scoped>
    @import'../../../css/dictionary/product/productDetail.css'
</style>