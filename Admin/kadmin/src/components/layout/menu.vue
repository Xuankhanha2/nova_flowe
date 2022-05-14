<template>
    <div class="menu">
        <div class="logo-box">
            <div class="logo"> </div>
            <div class="web-title">
                Admin
            </div>
            <div class="toggle-btn"></div>
        </div>
        <div class="nav-content">
            <router-link to="overview">
                <div class="nav-item">
                    <div class="select-highline"></div>
                    <div class="nav-item-icon dashroad-icon"></div>
                    <div class="nav-item-text">Tổng quan</div>
                </div>
            </router-link>

            <router-link to="product">
                <div class="nav-item">
                    <div class="select-highline"></div>
                    <div class="nav-item-icon plan-icon"></div>
                    <div class="nav-item-text">Sản phẩm</div>
                </div>
            </router-link>

            <router-link to="category">
                <div class="nav-item">
                    <div class="select-highline"></div>
                    <div class="nav-item-icon manage-revenue-icon"></div>
                    <div class="nav-item-text">Danh mục sản phẩm</div>
                </div>
            </router-link>


            <div class="nav-item">
                <div class="select-highline"></div>
                <div class="nav-item-icon manage-bill-icon"></div>
                <div class="nav-item-text">Quản lý hóa đơn</div>
            </div>

            <router-link to="news">
                <div class="nav-item">
                    <div class="select-highline"></div>
                    <div class="nav-item-icon mountofdebt-icon"></div>
                    <div class="nav-item-text">Tin tức</div>
                </div>
            </router-link>

            <router-link to="customer">
                <div class="nav-item">
                    <div class="select-highline"></div>
                    <div class="nav-item-icon report-icon"></div>
                    <div class="nav-item-text">Khách hàng</div>
                </div>
            </router-link>

            <div class="nav-item" @click="systemToggle()">
                <div class="select-highline"></div>
                <div class="nav-item-icon system-icon"></div>
                <div class="nav-item-text">Hệ thống</div>
                <!-- sub nav -->
                <div class="sub-nav" id="sub-system-nav" 
                    @click="signOut()"
                >
                    <div class="sub-nav-item">
                        <div class="nav-item-icon"><i class="fa-solid fa-arrow-right-from-bracket"></i></div>
                        <div class="sub-nav-item-text">Sign Out</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import $ from 'jquery'
export default {
    methods: {
        /**
         * created by: khanhvx
         * created date: 24/4/2022
         * Hàm hiện các option của tùy chọn System
         */
        systemToggle(){
            $("#sub-system-nav").slideToggle(200);
        },

        signOut(){
            localStorage.removeItem('accessToken');
            this.$router.push('/login');
        }
    },
}
</script>

<style lang="css">
@font-face {
  font-family: OpenSan-Bold;
  src: url('../../assets/Fonts/OpenSans-Bold.ttf');
}
    .menu{
        background-color: #2f3136 ;
        width: 220px;
        min-width: 220px;
        max-width: 220px;
        height: 100vh;
        margin:  0px;
    }
    .logo-box{
        height: 46px;
        box-shadow: 0px 0px 1px #000000;
        display: flex;
        align-items: center;
        position: relative;
        background:  #202225;
    }
    .logo{
        height: 32px;
        width: 32px;
        margin-left: 10px;
        background-image: url('../../assets/image/K_logo.jpg');
        background-position: center;
        background-size: cover;
        background-repeat: no-repeat;
        border-radius: 4px;
    }
    .web-title{
        font-family: OpenSan-Semibold;
        color: #ffffff;
        font-size: 15px;
        margin-left: 8px;
    }
    .toggle-btn{
        position: absolute;
        top: 50%;
        right: 16px;
        transform: translate(0%,-50%);
        width: 16px;
        height: 16px;
        background-repeat: no-repeat;
        background-size: contain;
        background-position: center;
        background-image: url('../../assets/image/hamburger-menu.svg');
        
    }

    .nav-content{
        margin-top: 16px;
        width: 100%
    }
    .nav-content a{
        text-decoration: none;
        
    }
    .nav-item{
        width: auto;
        height: 46px;
        display: flex;
        align-items: center;
        background-color: #2f3136;
        padding-left: 10px;
        cursor: pointer;
        position: relative;
    }
    .nav-item:hover{
        background-color:#4D4F5C;
        border-left: 4px solid #00d469;
        padding-left: 6px !important;
    }
    .nav-item:hover .nav-item-text{
        color: #00d469;
        text-decoration: none;
    }
    .nav-item-icon{
        width: 32px;
        height: 32px;
        background-position: center;
        background-position: center;
        background-repeat: no-repeat;
        text-align: center;
        color: #fff;
    }
    .nav-item-text{
        color:  #ffffff;
        font-size: 13px;
        margin-left: 16px;
    }
    a:hover{
        text-decoration: none !important;
    }
        .dashroad-icon{
            background-image: url('../../assets/image/ic_TongQuan_32.svg');
        }
        .plan-icon{
            background-image: url('../../assets/image/ic_LapKeHoachThu_32.svg');
        }
        .manage-revenue-icon{
            background-image: url('../../assets/image/ic_QuanLyThu_32.svg');
        }
        .manage-bill-icon{
            background-image: url('../../assets/image/ic_HoaDon_32.svg');
        }
        .mountofdebt-icon{
            background-image: url('../../assets/image/ic_SoThu_32.svg');
        }
        .report-icon{
            background-image: url('../../assets/image/ic_BaoCao_32.svg');
        }
        .message-icon{
            background-image: url('../../assets/image/ic_TinNhan_32.svg');
        }
        .system-icon{
            background-image: url('../../assets/image/ic_HeThong_32.svg');
        }
        .sign-out-icon{
            background-image: url('../../assets/image/logout-svgrepo-com.svg');
        }

    .sub-nav{
        width: calc(100% - 24px);
        display: none;
        position: absolute;
        top: 46px;
        left: 24px;
    }
    .sub-nav-item{
        width: auto;
        height: 46px;
        display: flex;
        align-items: center;
        background-color: #2f3136;
        padding-left: 10px;
        cursor: pointer;
        position: relative;
    }
    .nav-item:hover .sub-nav{
        left: 20px;
    }
    .sub-nav-item-text{
        color:  #ffffff;
        font-size: 13px;
        margin-left: 16px;
    }
    .sub-nav-item:hover .sub-nav-item-text{
        color: #00d469;
    }
</style>