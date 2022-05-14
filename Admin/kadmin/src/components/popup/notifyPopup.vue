<template>
    <div class="popup">
        <div class="popup-content">
            <div class="popup-title">Thông báo</div>
            <div class="popup-message">{{text}}</div>
            <div class="popup-btn">
                <Button :Text="'Đồng ý'" :second="false" v-if="firstBtn" :minwidth="true" @click.native="negativeFunction"/>
                <Button :Text="'Hủy'" :second="true" v-if="secondBtn" :minwidth="true" :style="'border: 1px solid #4d4f5c'" @click.native="closePopup"/>
                <Button :Text="'Đóng'" :second="false" v-if="thirdBtn" :minwidth="true" @click.native="closePopup"/>
            </div>
            <div class="btn-close-popup"
                @click="closePopup">
            </div>
        </div>
        <div class="blur"></div>
    </div>
</template>

<script>
import Button from '../layout/button.vue'
export default {
    props:{
        thirdBtn: Boolean,
        secondBtn:Boolean,
        firstBtn:Boolean,
        text:String,
        param: String,
    },
    components:{
        Button,
    },
    methods:{
        /*Hàm đóng popup thông báo*/
        closePopup:function(){
            this.$emit('closePopup');
        },
        negativeFunction(){
            this.$emit('negativeFunction',this.param);
        }
    }
}
</script>

<style lang="css" scoped>
    .popup-content{
        font-family: OpenSan-Regular;
        position: relative;
    }
    .blur{
        top: 0px;
        left: 0px;
        width: 100%;
        height: 100%;
        background-color: black;
        position: absolute;
        opacity: 0.4;
        z-index: 100;
    }
    .popup-content{
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        min-width: 300px;
        padding: 24px 16px 16px 16px;
        z-index: 300;
        background-color: #ffffff;
    }
    .popup-title{
        font-family: OpenSan-Semibold;
        font-size: 15px;
        line-height: 32px;
    }
    .popup-message{
        font-size: 13px;
        margin-top: 16px;
        margin-bottom: 16px;
    }
    .popup-btn{
        display: flex;
        justify-content: flex-end;
        align-content: center;
    }
    .btn-close-popup{
        position: absolute;
        top: 16px;
        right: 16px;
        width: 20px;
        height: 20px;
        background-image: url('../../assets/image//ic_close_16.png');
        background-position: center;
        background-size: contain;
        background-repeat: no-repeat;
        cursor: pointer;
    }
</style>