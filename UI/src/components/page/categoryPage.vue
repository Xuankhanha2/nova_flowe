<template>
    <div class="categoryPage">
        <div class="widgetBar">
            <div class="history">
                <span class="parentPage"><router-link to="/home">Trang chủ</router-link></span>
                <span style="padding: 0px 8px; color:brown">/</span>
                <span class="currentPage">Danh mục sản phẩm</span>
            </div>
            <div class="filterPrice">
                <label>(Giá) Từ: </label>
                <input type="text" name="price_from" id="from" size="15" required />
                <label>Đến: </label>
                <input type="text" name="price_to" id="to" size="15" required />
                <button id="btnFilter">Lọc</button>
            </div>
            <div class="sort">
                <label for="">Sắp xếp: </label>
                <select name="sort" id="sort">
                    <option value="0">--Chọn--</option>
                    <option value="priceAsc">Giá tăng dần</option>
                    <option value="priceDesc">Giá giảm dần</option>
                    <option value="nameAsc">Sắp xếp A-Z</option>
                    <option value="nameDesc">Sắp xếp Z-A</option>
                </select>
            </div>
        </div>
        <div class="productRow">
            <div class="title">
                <div class="categoryName">
                    {{category.categoryName}}
                </div>
            </div>
            <div class="productList row">
                <!-- Ô sản phẩm -->
                <div class="marginProductCell col-xl-3" 
                    v-for="product in products" 
                    :key="product.productId"
                >
                    <productCell
                        @pushToDetail = "pushToDetail"
                        @addCart="addCart"
                        :product="product"
                    />
                </div>
                <!-- /Ô sản phẩm -->
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios'
import productCell from '../shared/productCell.vue'
import path from '../../path'
export default {
    data() {
        return {
            cart: {},
            products: [],
            category: {}
        }
    },
    components:{
        productCell
    },
    methods: {
        /**Hàm format giá tiền sản phẩm
         * created by: VXKHANH
         * created date: 30/10/21
         */
        formatMoney(money){
            //chuyển đổi tiền
            var formatedMoney = String(money).replace(/(\d)(?=(?:\d{3})+$)/g, '$1.');
            return formatedMoney;
        },
        /**
         * created by: vxkhanh
         * created date: 14/11/2021
         * Hàm load dữ liệu khi trang category được mở 
         */
        async loadData(){
            const categoryId  = this.$route.params.categoryId;
            if(categoryId != null && String(categoryId) != "")
            {
                //Hiển thị sản phẩm theo danh mục được chọn 
                await axios.get('https://localhost:44368/api/v1.0/Products/categoryId/'+categoryId).then((result)=>{
                    this.products = result.data;
                })
                // Lấy ra loại hoa tương ứng
                this.getCategory(categoryId);
            }
            else{
                console("Error")
                return;
            }
        },

         /**
         * created date: 09/11/2021
         * created by: vxkhanh
         * Hàm thêm sản phẩm vào giỏ hàng
         */
        async addCart(productId){
            if(String(localStorage.getItem('customer')).trim() !== "")
            {
                let customer = JSON.parse(atob(localStorage.getItem('customer')));
                if(productId && String(productId).trim !== ""){
                   
                    //Nhấn nút thêm -> mặc định số lượng thêm là 1 sp
                    this.cart.quantity = 1;
                    //Lấy mã khách hàng đã đăng nhập được lưu trong localStorage
                    this.cart.customerId = customer.customerId;
                    this.cart.productId = productId;
                    //Check tồn tại product trong cart chưa
                    await this.checkExistsProductInCart(this.cart);
                    if(this.isExistsCart){
                        this.cart.quantity += 1;
                        this.updateCart(this.cart);
                    }
                    else{
                        await axios.post(path.cart, this.cart).then((result)=>{
                            console.log(result.data)
                            alert("Đã thêm sản phẩm vào giỏ hàng.");
                            this.isExistsCart = false;
                        }).catch(()=>{
                            console.log("Đã có lỗi hệ thống xảy ra.");
                        })
                    }
                }
            }
            else{
                this.$router.push('/login');
            }
        },

        /**
         * created by: khanhvx
         * created date: 10/5/2022
         */
        async getCategory(categoryId){
            if(categoryId){
                await axios.get(path.categories+categoryId).then((res) => {
                    if(res.data){
                        this.category = res.data;
                    }else{
                        return;
                    }
                })
            }
        },
    },
    async created(){
        this.cart = new Object;
        this.loadData();
    },

}
</script>
<style lang="css">
@import url('../../css/page/categoryPage.css');
</style>