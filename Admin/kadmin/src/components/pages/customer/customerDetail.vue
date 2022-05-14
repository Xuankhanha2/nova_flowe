<template>
    <div class="popup">
        <div class="form">
            <!-- <button @click="ValideteForm()">Load</button> -->
            <div class="form-title">
                {{pageTitle}}
            </div>
            <div class="form-content">
                <div class="form-left">
                <!-- Avatar -->
                    <div class="form-right-top-content">
                        <img :src="customer.image">
                        <div id="inputLinkImage" class="margin-r-16">
                            <!-- <input type="file" id="selectImage"> -->
                            <label>Chọn ảnh</label>
                            <input type="file" name="customerImage">
                        </div>
                    </div>
                 <!-- / -->
                
                <!-- Tên của khách hàng -->
                    <newLabel :text="'Họ và tên:'" 
                        :required="true"/>
                    <div class="input">
                        <input type="text" 
                            class="textbox margin-r-16"
                            v-model="customer.customerName"
                            @keyup="required.customerName = false"
                        >
                        <div class="tooltipCustom" 
                            v-show="required.customerName">
                                Dữ liệu không được để trống!
                        </div>
                    </div>
                <!-- /Tên của khách hàng -->

                <!-- Ngày sinh -->
                    <newLabel :text="'Ngày sinh'"
                        :required="false"
                    />
                    <div class="input">
                        <input type="date" 
                            class="textbox margin-r-16"
                            v-model="customer.dateOfBirth"
                        >
                        <div class="tooltipCustom" 
                            v-show="false">
                                Dữ liệu không được để trống!
                        </div>
                    </div>
                <!-- /Ngày sinh -->

                <!-- Giới tính -->
                <newLabel :text="'Giới tính'"
                    :required="true"
                />
                <div class="radio-row">
                        <div class="radio-button" >
                            <input type="radio" name="term" 
                                v-model="customer.gender" 
                                value="0"
                                @change="required.turnFee = false"
                                >
                            <label for=""></label>
                            <div class="radio-text">Nữ</div>
                        </div>
                        <div class="radio-button" >
                            <input type="radio" name="term" 
                                v-model="customer.gender" 
                                value="1"
                                @change="required.gender = false"
                                >
                            <label for=""></label>
                            <div class="radio-text">Nam</div>
                        </div>
                        <div class="radio-button">
                            <input type="radio" name="term" 
                                v-model="customer.gender" 
                                value="2"
                                @change="required.turnFee = false"
                                >
                            <label for=""></label>
                            <div class="radio-text">Khác</div>
                        </div>
                    </div>
                <!-- /Giới tính -->

                <!-- Email khách hàng -->
                    <newLabel :text="'Email'" 
                        :required="true"/>
                    <div class="input">
                        <input type="text" 
                            class="textbox margin-r-16"
                            v-model="customer.email"
                            @keyup="required.email = false"
                        >
                        <div class="tooltipCustom" 
                            v-show="required.email">
                                Dữ liệu không được để trống!
                        </div>
                    </div>
                <!-- /Email khách hàng -->
                </div>


                <div class="form-right">
                    <!-- Số điện thoại  -->
                    <newLabel :text="'Số điện thoại'" 
                        :required="true"/>
                    <div class="input">
                        <!-- Ô nhập dữ liệu -->
                        <input type="text"
                            class="textbox"
                            :class="{'required-outline':false}" 
                            v-model="customer.phoneNumber"
                            @keyup="required.phoneNumber = false"
                        >
                        <!-- Tooltip thông báo khi dữ liệu của trường này bị để trống khi gửi -->
                        <div class="tooltipCustom" 
                            v-show="required.phoneNumber">
                                Dữ liệu không được để trống!
                        </div>
                    </div>
                    <!-- /Số điện thoại -->

                    <!-- Địa chỉ  -->
                    <newLabel :text="'Địa chỉ'" 
                        :required="true"/>
                    <div class="input">
                        <textarea
                            class="textbox"
                            v-model="customer.address"
                        >    
                        </textarea>
                        <div class="tooltipCustom" 
                            v-show="required.address">
                                Dữ liệu không được để trống!
                        </div>
                    </div>
                    <!-- Địa chỉ -->

                    <!-- Ghi chú -->
                    <div class="form-right-middle-content">
                        <newLabel :text="'Ghi chú'" 
                            :required="false"/>
                        <div class="input">
                            <textarea
                                class="textbox"
                                v-model="customer.note"
                            >  
                            </textarea>
                            <div class="tooltipCustom" 
                                v-show="false">
                                    Dữ liệu không được để trống!
                            </div>
                        </div>
                    </div>
                    <!-- /Ghi chú -->

                </div>
            </div>
            <div class="form-footer">
                <div class="f-footer-left">
                    
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
        refCustomer: Object,
        config: Object,
    },
    data() {
        return {
            //Biến customer lưu giá trị của sản phẩm dược cập nhật
            customer:{},
            /**Biến kiểm tra các ô không được để dữ liêu trống */
            required:{
                customerName:false,
                dateOfBirth: false,
                phoneNumber:false,
                email:false,
                gender:false,
                address:false,
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

        /**Hàm xử lý cho nút gửi dữ liệu bao gồm cả thêm và sửa*/
        async processData(){
            //Gán mật khẩu và password giả
            this.customer.password = "";
            this.customer.username = "";
            // var rowAffect = 0;
            var processResult;
            //Validate dữ liệu
            var isValid = this.validateData();
            //Kiem tra dữ liệu hợp lệ. Nếu hợp lệ thì thực hiện thêm - sửa
            if(isValid){
                if(this.isUpdate){
                    //Thục hiện sửa
                    await axios.put(apiPath.customers, this.customer, this.config).then((result)=>{
                        processResult = result.data;
                        //Hiển thị kết quả sau khi xử lý
                        this.showNotifyPopup(processResult.message)
                        //Load lại dữ liệu
                        this.loadData();
                    }).catch(()=>{
                        this.showNotifyPopup("Đã có lỗi xảy ra.")
                    })
                }else{
                    
                    //Thực hiện thêm
                    await axios.post(apiPath.customers, this.customer, this.config).then((result)=>{
                        processResult = result.data;
                        //Hiển thị kết quả sau khi xử lý
                        this.showNotifyPopup(processResult.message)
                        this.loadData();
                    }).catch(()=>{
                        this.showNotifyPopup("Đã có lỗi xảy ra.")
                    })
                    
                }
            }else{
                //Nếu dữ liệu chưa hợp lệ thì kết thúc hàm bằng lệnh return 
                console.log("Return");
                return;
            }
            
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

        /**Hàm xử lý nghiệp vụ cho các ô nhập liệu*/
        validateData(){
            //Thực hiện kiểm tra dữ liêu hợp lệ
            var isValid = true;
            //Kiểm tra tên khách hàng
            if(this.customer.customerName == "" | this.customer.customerName == null | this.customer.customerName == undefined){
                isValid = false;
                this.required.customerName = true;
            }else{
                this.required.customerName = false;
            }
            //Kiểm tra số điện thoại
            if(this.customer.phoneNumber == "" | this.customer.phoneNumber == null | this.customer.phoneNumber == undefined){
                isValid = false;
                this.required.phoneNumber = true;
            }else{
                this.required.phoneNumber = false;
            }
            //Kiểm tra Email
            if(this.customer.email == "" | this.customer.email == null | this.customer.email == undefined){
                isValid = false;
                this.required.email = true;
            }else{
                this.required.email = false;
            }

            //Kiểm tra địa chỉ
            if(this.customer.address == "" | this.customer.address == null | this.customer.address == undefined){
                isValid = false;
                this.required.address = true;
            }else{
                this.required.address = false;
            }
            return isValid;
        },

        /**Hàm format số tiền */
        formatMoney(){
            var newPrice = String(this.customer.price);
            //newPrice = newPrice.toString().split(".").join("");
            this.customer.price = newPrice.replace(/(\d)(?=(?:\d{3})+$)/g, '$1.');
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
        
            
    },
    created() {
        if(this.refCustomer != null){
            this.customer={...this.refCustomer};
            var date = String(this.customer.dateOfBirth).substr(0,10);
            this.customer.dateOfBirth = date;
        }
        //Format tiền ngay khi form được bật lên
    },
}
</script>

<style lang="css" scoped>
    @import url('../../../css/dictionary/product/productDetail.css');
    @import url('../../../css/dictionary/customer/customerDetail.css');
</style>