<template>
    <div class="header">
        <div class="header-left">
            <div class="header-left-text">Quản lý sản phẩm</div>
            <!-- <div class="header-left-icon"></div> -->
        </div>
        <div class="header-right">
            <div class="hr-item1">
                <!-- <input type ="text" class="hr-item-text" placeholder="Tìm kiếm...">
                <div class="hr-item-icon"><i class="fas fa-search"></i></div> -->
            </div>
            <!-- <div class="hr-item2">
                <select id="yearselect">
                    <option value="0">2020 - 2021</option>
                </select>
            </div> -->
            <div class="hr-item3">
                <a href="#"><div class="hr-item-icon1"></div></a>
                <a href="#"><div class="hr-item-icon2"></div></a>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    
}
</script>

<style lang="css">
@import url('../../css/fontawesome-free-6.1.1-web/css/all.css');
    .header{
        width: 100%;
        height: 46px;
        box-shadow: 1px 1px 1px #000000;
        display: flex;
        align-items: center;
        background-color: #FFFFFF;
        justify-content: space-between;
        z-index: 20;
    }
    .header-left{
        display: flex;
    }
    .header-left-text{
        font-family: OpenSan-Semibold;
        font-size: 16px;
        margin-right: 12px;
        margin-left: 16px;
    }
    .header-left-icon{
        background-image: url('../../assets/image/ic_Info_20.svg');
        width: 24px;
        height: 24px;
        background-repeat: no-repeat;
        background-size: contain;
        background-position: center;
    }
    .header-right{
        display: flex;
        align-items: center;
        font-size: 13px;
    }
    .hr-item1,.hr-item2,.hr-item3{
        height: 32px;
    }
    .hr-item1{
        display: flex;
        align-items: center;
    }
    .hr-item-icon{
        width: 32px;
        height: 32px;
        background-position: center;
        background-repeat: no-repeat;
        background-size: cover;
        margin-left: -2px;
        z-index: 50;
        margin-right: 16px;
        border-radius: 0px 4px 4px 0px;
        background-color: blueviolet;
        text-align: center;
        line-height: 32px;
        font-size: 18px;
        cursor:pointer;
    }
    .hr-item-icon i{
        color:#ffffff;
    }
    .hr-item-text{
        border: 1px solid #CECECE;
        height: 30px;
        width: 300px;
        line-height: 30px;
        padding: 0px 12px;
        border-radius: 4px 0px 0px 4px;
    }
    .hr-item-text:focus{
        outline:none;
    }
    .hr-item2 select{
        height: 32px;
        border: 1px solid #CECECE;
        padding: 0px 12px;
        border-radius: 4px;
        margin-right: 16px;
    }
    .hr-item2 select:focus{
        outline: none;
    }
    .hr-item3{
        display: flex;
        height: 32px;
        border-left: 1px solid #CCCCCC;
    }
    .hr-item-icon1, .hr-item-icon2{
        width: 32px;
        height: 32px;
        background-size: contain;
        background-position: center;
        margin-left: 16px;
        background-repeat: no-repeat;
    }
    .hr-item-icon1{
        background-image: url('../../assets/image/home.svg');
        border-radius: 6px;
        cursor:pointer;
        
    }
    .hr-item-icon2{
        background-image: url('../../assets/image/Avatar.png');
        margin-right: 16px;
        cursor:pointer;
    }
</style>