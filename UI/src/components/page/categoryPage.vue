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
            <!-- <div class="title"><div class="categoryName">TRÁI CÂY MỖI NGÀY</div></div> -->
            <div class="productList row">
                <!-- Ô sản phẩm -->
                <div class="marginProductCell col-xl-3" 
                    v-for="product in products" 
                    :key="product.productId"
                >
                    <div class="productCell">
                        <img :src="product.image"
                            class="productImage"
                            @click="pushToDetail(product.productId)"
                        >
                        <h1 class="productName"
                            @click="pushToDetail(product.productId)"
                        >{{product.productName}}</h1>
                        <!-- ratingStar -->
                        <div class="ratingStars">
                            <i class="fas fa-star"></i>
                            <i class="fas fa-star"></i>
                            <i class="fas fa-star"></i>
                            <i class="fas fa-star"></i>
                            <i class="fas fa-star"></i>
                        </div>
                        <!-- end rating Star -->

                        <!--Real price -->
                        <h2>100.000 đ</h2>

                        <!-- price after discount -->
                        <p>{{formatMoney(product.price)}} ₫</p>

                        <!-- discount percent -->
                        <div class="sale">-{{product.discount}}%</div>

                        <!-- button add wishlist -->
                        <div class="wishlist"><i class="far fa-heart"></i></div>

                        <!-- button Thêm vào giỏ -->
                        <div class="btn btn-primary" id="btnAddCart"
                            @click="addCart(product.productId)"
                        >Thêm vào giỏ</div>
                    </div>
                </div>
                <!-- /Ô sản phẩm -->
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios'
export default {
    data() {
        return {
            cart: {},
            products: [],
        }
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
         * created date: 09/11/2021
         * created by: vxkhanh
         * Hàm thêm sản phẩm vào giỏ hàng
         */
        async addCart(productId){
            if(localStorage.getItem('userId'))
            {
                if(productId != null && productId != ""){
                    this.cart.productId = productId;
                    //Nhấn nút thêm -> mặc định số lượng thêm là 1 sp
                    this.cart.quantity = 1;
                    //Lấy mã khách hàng đã đăng nhập được lưu trong localStorage
                    this.cart.customerId = localStorage.getItem('userId');
                    await axios.post('https://localhost:44368/api/v1.0/Carts', this.cart).then((result)=>{
                        console.log(result.data);
                        alert("Đã thêm sản phẩm vào giỏ hàng.");
                    }).catch(()=>{
                        console.log("Đã có lỗi hệ thống xảy ra.");
                    })
                }
            }
            else{
                this.$router.push('/login');
            }
        },
        /**
         * created by: vxkhanh
         * created date: 14/11/2021
         * Hàm load dữ liệu khi trang category được mở 
         */
        async loadData(){
            var categoryId  = this.$route.params.categoryId;
            if(categoryId != null && categoryId != ""){
                //Hiển thị sản phẩm theo danh mục được chọn 
                await axios.get('https://localhost:44368/api/v1.0/Products/categoryId/'+categoryId).then((result)=>{
                    this.products = result.data;
                })
            }
            else{
                // //Nếu ko có tham số categoryId thì mặc định hiện danh sách sản phẩm được phân theo danh mục
                // await axios.get('https://localhost:44368/api/v1.0/Products').then((result)=>{
                //     this.products = result.data;
                // })
                return;
            }
        }
    },
    async created(){
        this.cart = new Object;
        this.loadData();
    },
    async updated() {
        this.loadData();
    },

}
</script>
<style lang="css">
@import url('../../css/page/categoryPage.css');
</style>