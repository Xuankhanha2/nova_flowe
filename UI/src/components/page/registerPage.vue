<template>
    <div class="registerPage col-xl-5 container">
        <!-- Thông báo khi có lỗi đăng nhập -->
        <div class="notify container col-xl-12">
            <div class="alert alert-danger alertFont" v-show="alert.show" ref="alert"><i class="fas fa-exclamation-triangle"></i> {{alert.message}}</div>
        </div>
        <!--  -->
        <div class="loginContent container col-xl-12">
            <!-- Logo đăng nhập -->
            <div class="loginLogo container col-xl-6">
                Đăng ký
            </div>
            <!-- / -->

            <!-- form đăng nhập -->
            <div class="loginForm">
                <!-- Tên người dùng -->
                <div class="formGroup">
                    <label class="col-form-label">Tên người dùng</label>
                    <input class="loginInput form-control shadow-none" type="username" name="username" placeholder="Full name" 
                        v-model="customer.customerName"
                    >
                </div>
                <!-- / -->

                <!-- Ngày sinh -->
                <div class="formGroup">
                    <label class="col-form-label">Ngày sinh</label>
                    <input class="loginInput form-control shadow-none" type="date" name="username" placeholder="Username" 
                        v-model="customer.dateOfBirth"
                    >
                </div>
                <!-- / -->

                <!-- Giới tính -->
                <div class="loginCheckbox">
                    <div class="rememberMe d-flex">
                        <label class="col-form-label col-xl-3" style="padding-left: 0px !important">Giới tính:</label>
                        <input type="checkbox" name="gender" id="checkBox" class="container col-xl-1" v-model="customer.gender">
                        <label class="col-form-label col-xl-8" style="padding-left: 0px !important">Nữ</label>
                    </div>
                </div>
                <!-- / -->

                <!-- Số điện thoại -->
                <div class="formGroup">
                    <label class="col-form-label">Số điện thoại</label>
                    <input class="loginInput form-control shadow-none" type="text" name="phoneNumber" placeholder="(+84)" 
                        v-model="customer.phoneNumber"
                    >
                </div>
                <!-- / -->

                <!-- Email -->
                <div class="formGroup">
                    <label class="col-form-label">Email</label>
                    <input class="loginInput form-control shadow-none" type="email" name="email" placeholder="Email" 
                        v-model="customer.email"
                    >
                </div>
                <!-- / -->

                <!-- Số điện thoại -->
                <div class="formGroup">
                    <label class="col-form-label">Địa chỉ</label>
                    <textarea class="loginInput form-control shadow-none"
                        v-model="customer.address"
                    ></textarea>
                </div>
                <!-- / -->

                <!-- Tên tài khoản -->
                <div class="formGroup">
                    <label class="col-form-label">Tên tài khoản</label>
                    <input class="loginInput form-control shadow-none" type="username" name="username" placeholder="Username" 
                        v-model="customer.username"
                    >
                </div>
                <!-- / -->

                <!-- Mật khẩu -->
                <div class="formGroup">
                    <label class="col-form-label shadow-none">Mật khẩu</label>
                    <input class="loginInput form-control shadow-none" type="password" name="password" placeholder="Password" 
                        v-model="customer.password"
                    >
                </div>
                <!-- / -->

                <!-- Xác nhận mật khẩu -->
                <div class="formGroup">
                    <label class="col-form-label shadow-none">Xác nhận mật khẩu</label>
                    <input class="loginInput form-control shadow-none" type="password" name="password" placeholder="Password" 
                        ref="confirmPassword"
                    >
                </div>
                <!-- / -->

                <!-- Nút đănh nhập -->
                <button class="btn btn-success btn-block" type="submit" style="font-size: 18px; margin-top: 15px"
                    @click="register()"
                >Đăng ký</button>
                <!-- / -->

                <!-- Đăng ký tài khoản khi chưa có tài khoản -->
                <div id="registerLink">
                    <p class="container">
                        Bạn đã có tài khoản?
                        <a href="/Account/Register">Đăng nhập</a>
                    </p>
                </div>
                <!-- / -->
            </div>
            <!-- / -->
        </div>
    </div>
</template>

<script>
import axios from 'axios'
export default {
    data() {
        return {
            customer:{},
            /**Biến gửi thông điệp ra màn hình khi có lỗi xảy ra*/
            alert:{
                message: "",
                show: false
            }

        }
    },
    async created() {
        this.customer = {
            customerId: "00000000-0000-0000-0000-000000000000",
            customerName: "",
            phoneNumber: "",
            address: "",
            email: "",
            dateOfBirth: "",
            gender: 0,
            username: "",
            password: ""
        }
        
    },
    methods: {
        /**
         * created date: 11/08/2021
         * created by: vxkhanh
         * Hàm kiểm tra các ô đã được nhập đầy đủ dữ liệu chưa
         */
        validate(){
            if(this.customer.customerName == "" || this.customer.customerName == null){
                this.alert.message = "Bạn chưa nhập tên người dùng.";
                return false;
            }
            else if(this.customer.dateOfBirth == "" || this.customer.dateOfBirth == null)
            {
                this.alert.message = "Bạn chưa chọn ngày sinh";
                return false;
            }
            else if(this.customer.phoneNumber == "" || this.customer.phoneNumber == null){
                this.alert.message = "Bạn chưa nhập số điện thoại";
                return false;
            }
            else if(this.customer.email == "" || this.customer.email == null){
                this.alert.message = "Bạn chưa nhập email";
                return false;
            }
            else if(this.customer.address == "" || this.customer.address == null){
                this.alert.message = "Bạn chưa nhập địa chỉ.";
                return false;
            }else if(this.customer.username == "" || this.customer.username == null){
                this.alert.message = "Bạn chưa nhập tên tài khoản";
                return false;
            }
            else if(this.customer.password == "" || this.customer.password == null){
                this.alert.message = "Bạn chưa nhập mật khẩu";
                return false;
            }
            else
            {
                var regPhoneNumber = /((09|03|07|08|05)+([0-9]{8})\b)/g;
                var confirmPassword = this.$refs.confirmPassword.value;
                if(regPhoneNumber.test(this.customer.phoneNumber) == false){
                    this.alert.message = "Số điện thoại cửa bạn không đúng định dạng!";
                    return false;
                }
                else if(this.customer.password != confirmPassword)
                {
                    this.alert.message = "Mật khẩu và xác nhận mật khẩu không giống nhau!";
                    return false;
                }
                return true;
            }

        },
        /**
         * created date: 11/08/2021
         * created by: vxkhanh
         * Hàm gửi yêu cầu đăng ký  
         */
        async register(){
            if(this.validate()){
                await axios.post('https://localhost:44368/api/v1.0/Customers',this.customer).then((result)=>{
                    this.alert(result.data.message);
                    this.alert.show = false;
                    this.$router.push("/login");
                })
            }else{
                this.alert.show = true;
                window.scrollTo(0, top);
            }
            
        }
    },
}
</script>
<style lang="css">
@import url('../../css/page/register.css');
</style>