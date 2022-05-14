<template>
    <div class="mainMenu">
        <div class="menuContent">
            <div class="menuLeft">
                <ul>
                    <router-link to="/home">
                    <li>
                        Trang chủ
                    </li>
                    </router-link>
                    
                    <li>
                        <router-link to="/category"><span>Danh mục</span></router-link>
                        <div id="subMenu">
                            <div class="row justify-content-start">
                                <div class="col-xl-3 categoryItem"
                                    v-for="category in categories"
                                    :key="category.categoryId"
                                    @click="pushToCategory(category.categoryId)"
                                >
                                    {{category.categoryName}}
                                </div>
                            </div>
                        </div>
                    </li>
                    
                    <!-- <router-link to="/provider">
                    <li>
                        Chủ đề
                    </li>
                    </router-link> -->

                    <router-link to="/news">
                    <li>
                        Tin tức
                    </li>
                    </router-link>

                    <router-link to="/introduction">
                    <li>
                        Giới thiệu
                    </li>
                    </router-link>

                    <router-link to="/contact">
                    <li>
                        Liên hệ
                    </li>
                    </router-link>
                </ul>
            </div>
            <div class="search">
                <div class="search-bg" style="position: relative">
                    <input type="text" name="search" id="search" placeholder="Tìm kiếm..." 
                        @keyup="searchProduct()" autocomplete="off">
                    <i class="fas fa-search" style="color: black; cursor: pointer" onclick="Search();"></i>
                    <div id="smartSearch">
                        <ul>
                            <li v-for="product in products"
                                :key="product.productId"
                                @click="pushToDetail(product.productId)"
                            >
                                <img :src="product.image">
                                <div class="productSearchContent">
                                    <h2><span>{{product.productName?product.productName:''}}</span></h2>
                                    <h3><span>{{formatMoney(product.price)}}₫</span></h3>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios'
import path from '../../path'
export default {
    data() {
        return {
            //Biến lưu danh sách danh mục sản phẩm
            categories: [],
            //Biến lưu danh sách sản phẩm khi tìm kiếm
            products: [],

        }
    },
    methods: {
        /***
         * created date: 12/11/2021
         * created by: vxkhanh
         * Hàm chuyển đến trang danh mục với id tương ứng
         */
        pushToCategory(categoryId){
            var id = String(categoryId);
            if(id && String(id).trim() !== "")
            {
                this.categories = [];
                this.$router.push({ name: 'category', path:'/category', params: {categoryId: id}});
            }
        },
        /**
            created date: 16/11/2021
            created by: vxkanh
            Hàm tìm kiếm sản phẩm
         */
        async searchProduct(){
            var key = document.getElementById('search').value;
            if(key && String(key).trim() !== "")
            {
                await axios.get(path.productKeySearch+key).then((result)=>{
                this.products = result.data;
                console.log(result.data);
                }).catch(()=>{
                    
                })
            }
        },
        /**Hàm format giá tiền sản phẩm
         * created by: VXKHANH
         * created date: 16/11/2021
         */
        formatMoney(money){
            //chuyển đổi tiền
            var formatedMoney = String(money).replace(/(\d)(?=(?:\d{3})+$)/g, '$1.');
            return formatedMoney;
        },
        /**
         * created by: vxkhanh
         * created date: 11/11/2021
         * Hàm chuyển đến trang chi tiết sản phẩm
         */
        pushToDetail(productId){
            var id = String(productId);
            if(productId && id.trim() !== ""){
                this.products = null;
                this.$router.push({
                    name: 'productDetail', 
                    path: '/productDetail', 
                    params: { productId: id}
                });
            }
        },
    },
    async created() {
        this.products = new Array();
        await axios.get(path.categories).then((result)=>{
            this.categories = result.data;
        })
    },
    
};
</script>


<style lang="css" scoped>
@import url('../../css/layout/menu.css');
</style>