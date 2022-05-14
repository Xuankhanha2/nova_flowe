<template>
    <div class="popup">
        <div class="form categoryDetail">
            <!-- <button @click="ValideteForm()">Load</button> -->
            <div class="form-title">
                {{pageTitle}}
            </div>
            <div class="form-content">
                <div class="form-left deleteBorderRight changePaddingBottom">
                <!-- Tên của sản phẩm -->
                    <newLabel :text="'Tên danh mục:'" 
                        :required="true"/>
                    <div class="input">
                        <input type="text" 
                            class="textbox margin-r-16"
                            v-model="cloneCategory.categoryName"
                            @keyup="required.categoryName = false"
                        >
                        <div class="tooltipCustom tooltipChangePosition" 
                            v-show="required.categoryName">
                                Dữ liệu không được để trống!
                        </div>
                    </div>
                <!-- end Tên của sản phẩm -->


                <!-- Nhà cung cấp sản phẩm -->
                    <newLabel :text="'Dannh mục cha'" :required="false"/>
                    <div class="form-left-row ">
                        <!-- Ô Chọn dữ liệu  -->
                        <select class="mar-r-16px radius"
                            :class="{'required-outline':false}"
                            v-model="cloneCategory.parentId"
                        >
                            <option 
                                v-for="category in refCategories"
                                :key="category.categoryId"
                                :value="category.categoryId"
                            >{{category.categoryName}}</option>
                        </select>
                        <!-- Tooltip thông báo khi dữ liệu của trường này bị để trống khi gửi -->
                        <div class="tooltipCustom" v-show="false">Dữ liệu không được để trống!</div>
                    </div>
                <!-- end Nhà cung cấp sản phẩm -->
                    <newLabel :text="'Thẻ icon của sản phẩm'" 
                        :required="false"
                        title="Thẻ icon có thể lấy trên trang Fontawsome"
                    />
                    <div class="input">
                        <!-- Ô nhập dữ liệu -->
                        <input type="text" 
                            ref="feeName" 
                            class="textbox margin-r-16"
                            :class="{'required-outline':false}"
                            v-model="cloneCategory.categoryIcon"
                            title="Thẻ icon có thể lấy trên trang Fontawsome"
                        >
                        <!-- Tooltip thông báo khi dữ liệu của trường này bị để trống khi gửi -->
                        <div class="tooltipCustom " 
                            v-show="false">
                                Dữ liệu không được để trống!
                        </div>
                    </div>
                </div>
                
            </div>
            <div class="form-footer">
                <div class="f-footer-left">
                    <div class="row-check non-margin">
                        
                    </div>
                </div>
                <div class="f-footer-right">
                    <newButton
                        :Text="'Đóng'"
                        :second="true"
                        @click.native="closeForm()"
                    />
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
    created date: 27/7/2021
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
        refCategories: [],
        refCategory: Object,
        config: Object,
    },
    data() {
        return {
            /**Biến chứa giá trị danh mục khi cập nhật */
            cloneCategory: {
                categoryId: "197fda97-5a35-781d-0e77-d9ebc53aac3d",
                categoryName: "",
                categoryIcon: ""
            },
            /**Biến kiểm tra các ô không được để dữ liêu trống */
            required:{
                categoryName:false,

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
                    await axios.put(apiPath.categories, this.cloneCategory, this.config).then((result)=>{
                        processResult = result.data;
                        //Hiển thị kết quả sau khi xử lý
                        this.notifyText = processResult.message;
                        this.popup = true;
                        this.loadData();
                    }).catch(()=>{
                        this.notifyText = "Đã có lỗi xảy ra.";
                        this.popup = true;
                        
                    })
                    
                }else{
                    //Thực hiện thêm
                    await axios.post(apiPath.categories, this.cloneCategory, this.config).then((result)=>{
                        processResult = result.data;
                        //Hiển thị kết quả sau khi xử lý
                        this.notifyText = processResult.message;
                        this.popup = true;
                        this.loadData();
                    }).catch(()=>{
                        this.popup = true;
                        this.notifyText = "Đã có lỗi xảy ra.";
                    })
                    
                }
                console.log(processResult);
            }else{
                //Nếu dữ liệu chưa hợp lệ thì kết thúc hàm bằng lệnh return 
                console.log("Return");
                return;
            }
            
        },
        /**Hàm xử lý nghiệp vụ cho các ô nhập liệu*/
        validateData(){
            var name = this.cloneCategory.categoryName;
            //Thực hiện kiểm tra dữ liêu hợp lệ
            var isValid = true;
            if(name == "" | name == null | name == undefined){
                isValid = false;
                this.required.categoryName = true;
            }else{
                name = String(name);
                name = name.trim();
                if(name == ""){
                    isValid = false;
                    this.required.categoryName = true;
                }
                else{
                    this.required.categoryName = false;
                    this.cloneCategory.categoryName = name;
                }   
            }
            return isValid;
            
        },
        /*Hàm xử lý sự kiện đóng popup thông báo*/
        closePopup(){
            this.notifyText = "";
            this.popup = false;
            this.closeForm();
            
        },
        /**load dữ liệu khi thực hiện thêm, sửa thành công */
        loadData(){
            this.$emit('loadData');
        }  
    },
    created() {
        if(this.refCategory != null){
            this.cloneCategory={...this.refCategory};
        }
    },
}
</script>

<style lang="css" scoped>
    @import'../../../css/dictionary/product/productDetail.css';
    @import'../../../css/dictionary/category/categoryDetail.css'
</style>