<template>
    <div class="popup">
        <div class="form">
            <!-- <button @click="ValideteForm()">Load</button> -->
            <div class="form-title">
                {{pageTitle}}
            </div>
            <div class="form-content">
                
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
                        :Text="'Chấp nhận'"
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
// import newLabel from '../../layout/label.vue'
import newButton from '../../layout/button.vue'
import axios from 'axios'
import newPopup from '../../popup/notifyPopup.vue'
import apiPath from '../../../path'
export default {
    props:{
        pageTitle:String,
        focusOn: Boolean,
        isUpdate: Boolean,
        refOrder: Object,
        config: Object,
    },
    data() {
        return {
            //Biến product lưu giá trị của sản phẩm dược cập nhật
            cloneOrder:{
                "createdDate": null,
                "createdBy": "",
                "modifiedDate": null,
                "selectedItem": true,
                "orderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "orderCode": "",
                "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "status": 0,
                "orderDetails": [
                    {
                        "createdDate": null,
                        "createdBy": "string",
                        "modifiedDate": null,
                        "selectedItem": true,
                        "orderDetailId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                        "orderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                        "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                        "quantity": 1
                    }
                ]
            },
            /**Biến kiểm tra các ô không được để dữ liêu trống */
            required:{
                status: false
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
        // newLabel,
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
            let processResult;
            //Validate dữ liệu
            const isValid = this.validateData();
            //Kiem tra dữ liệu hợp lệ. Nếu hợp lệ thì thực hiện thêm - sửa
            if(isValid){
                //Thục hiện sửa
                await axios.put(apiPath.order, this.cloneOrder, this.config).then((result)=>{
                    processResult = result.data;
                    //Hiển thị kết quả sau khi xử lý
                    this.showNotifyPopup(processResult.message);
                    this.loadData();
                }).catch(()=>{
                    this.showNotifyPopup("Đã có lỗi xảy ra.");
                })
            }else{
                //Nếu dữ liệu chưa hợp lệ thì kết thúc hàm bằng lệnh return 
                return;
            }
            
        },
        /**Hàm xử lý nghiệp vụ cho các ô nhập liệu*/
        validateData(){
            let isValid =  true;
            const quantity = this.cloneOrder.status;
            if(quantity === null || quantity !== ""){
                isValid = false;
                this.required.status = true;
            }
            return isValid;
            
        },
        /**Hàm format số tiền */
        formatMoney(native){
            var newPrice = String(native);
            newPrice = newPrice.toString().split(".").join("");
            return newPrice.replace(/(\d)(?=(?:\d{3})+$)/g, '$1.');
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
        if(this.refOrder){
            this.cloneOrder={...this.refOrder};
        }
    },
}
</script>

<style lang="css" scoped>
    @import'../../../css/dictionary/product/productDetail.css'
</style>