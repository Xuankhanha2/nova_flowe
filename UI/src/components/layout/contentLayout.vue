<template>
    <div class="contentLayout">
        <router-view v-bind="myProps"></router-view>
    </div>
</template>

<script>
import axios from 'axios'
export default {
    computed:{
        myProps(){
            if(this.$route.name == 'home'){   
                return { products: this.products}
            }
            if(this.$route.name == 'cart'){   
                return { products: this.products}
            }
            else
                return "";
        }
    },
    props:{
    },
    data() {
        return {
            products:[],
            categories: [],    
        }
    },
    components:{

    },
    methods: {
        
    },
    mounted(){
        
    },
    async created() {
        //Lấy danh sách sản phẩm
        await axios.get('https://localhost:44368/api/v1.0/products').then((result)=>{
            this.products = result.data;
        }).catch(()=>{
            console.log("Đã xảy ra lỗi khi lấy sản phẩm.");
        });
    }
    
}
</script>
<style lang="css">
.contentLayout{
    margin: 0px auto;
    padding: 0px;
    width: 1200px;
    margin-top: 10px;
    min-height: 400px;
}

@import url('../../css/layout/content.css')
</style>