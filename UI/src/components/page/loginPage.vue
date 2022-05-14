<template>
    <div class="loginPage col-xl-4 container">
        <!-- Thông báo khi có lỗi đăng nhập -->
        <div class="notify container col-xl-12">
            <div class="alert alert-danger" v-show="loginFail"><i class="fas fa-exclamation-triangle"></i> Tài khoản hoặc mật khẩu không đúng</div>
        </div>
        <!--  -->
        <div class="loginContent container col-xl-12">
            <!-- Logo đăng nhập -->
            <div class="loginLogo container col-xl-6">
                Đăng nhập
            </div>
            <!-- / -->

            <!-- form đăng nhập -->
            <div class="loginForm">
                <form method="post" v-on:submit.prevent>
                    <!-- Tên tài khoản -->
                    <div class="formGroup">
                        <label class="col-form-label">Tên tài khoản</label>
                        <input class="loginInput form-control shadow-none" type="username" name="username" placeholder="Username"
                            v-model="user.username"
                        >
                    </div>
                    <!-- / -->

                    <!-- Mật khẩu -->
                    <div class="formGroup">
                        <label class="col-form-label shadow-none">Mật khẩu</label>
                        <input class="loginInput form-control shadow-none" type="password" name="password" placeholder="Password" autocomplete="off"
                            v-model="user.password"
                        >
                    </div>
                    <!-- / -->

                    <!-- Ghi nhớ tài khoản và quên mật khẩu -->
                    <div class="loginCheckbox">
                        <div class="rememberMe d-flex">
                            <input type="checkbox" name="remember" id="checkBox" class="container col-xl-1">
                            <label class="col-form-label col-xl-11" style="padding-left: 0px !important">Ghi nhớ tài khoản này?</label>
                        </div>
                        <label class="col-form-label" id="forgotPassword">
                            <a href="#">Quên mật khẩu?</a>
                        </label>
                    </div>
                    <!-- / -->

                    <!-- Nút đănh nhập -->
                    <button class="btn btn-success btn-block" type="submit" style="font-size: 18px;"
                        @click="login()"
                    >Đăng nhập</button>
                    <!-- / -->

                    <!-- Đăng ký tài khoản khi chưa có tài khoản -->
                    <div id="registerLink">
                        <p class="container">
                            Bạn chưa có tài khoản?
                            <a href="/Account/Register">Đăng ký tại đây</a>
                        </p>
                    </div>
                    <!-- / -->
                </form>
            </div>
            <!-- / -->
        </div>
    </div>
</template>

<script>
import axios from 'axios'
import path from '../../path'
export default {
    data() {
        return {
            user:{
                username: "",
                password: ""
            },
            loginFail: false,
        }
    },
    created(){
        localStorage.removeItem('customer');
    },
    methods: {
        /**
         * created date: 05/11/2021
         * created by: vxkhanh
         * Hàm kiểm tra đầy đủ thông tin trước khi đăng nhập
         */
        validate(){
            if(String(this.user.username).trim() == "" || this.user.username == null){
                alert("Bạn chưa nhập tên tài khoản");
                return false;
            }
            if(String(this.user.password).trim() == "" || this.user.password == null){
                alert("Bạn chưa nhập mật khẩu");
                return false;
            }
            return true
        },
        /**
         * created by: vxkhanh
         * created date: 08/11/2021
         * Hàm gửi yêu cầu đăng nhập lên server
         */
        async login(){
            if(this.validate())
            {
                var response = await axios.post(path.customers+'login', this.user).then((result)=>{
                    return result.data;
                });
                //console.log(response);
                if(response != null && response != ""){
                    this.loginFail = false;
                    localStorage.setItem('customer', btoa(JSON.stringify(response)));
                    this.$router.push('/home');
                }
                else
                    return;
                    this.loginFail = true;
            }
        }
    },
}
</script>

<style lang="css">
    @import url('../../css/page/login.css');
</style>