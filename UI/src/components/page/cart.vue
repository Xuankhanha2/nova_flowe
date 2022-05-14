<template>
    <div class="cartView">
        <h3 class="cartTitle"><i class="fas fa-shopping-cart">
            </i> GIỎ HÀNG 
            <span id="count_sp">(có {{cart.length}} sản phẩm)</span>
        </h3>
        <div class="cartContent row">
            <div class="cartContentLeft col-xl-9">
                <!-- drop-box -->
                <div>
                    <table class="listProduct">
                        <!-- Table header -->
                        <tr>
                            <th>Hình ảnh</th>
                            <th>Tên hàng</th>
                            <th>Đơn giá</th>
                            <th>Số lượng</th>
                            <th>Thành tiền</th>
                            <th> </th>
                        </tr>
                        <!-- / -->

                        <!-- Danh sách sản phẩm trong giỏ hàng -->
                        <tr v-for="cartItem in cart"
                            :key="cartItem.cartId"
                        >
                            <!-- Ảnh sản phẩm -->
                            <td><img :src="cartItem.product?cartItem.product.image:''" alt=""></td>
                            <!-- / -->

                            <!-- Tên sản phẩm -->
                            <td><h2
                                    @click="pushToDetail(cartItem.productId)"
                                >{{cartItem.product?cartItem.product.productName:''}}</h2></td>
                            <!-- / -->

                            <!-- Đơn giá của sản phẩm -->
                            <td>{{formatMoney(cartItem.product?cartItem.product.price:0)}} ₫</td>
                            <!-- / -->

                            <!-- Số lượng mua -->
                            <td>
                                <input type="number" min="1" id="number" name="number" v-model="cartItem.quantity" style="width: 50px">
                            </td>
                            <!-- / -->

                            <!-- Tổng tiền: tính theo số lượng * đơn giá -->
                            <td>{{formatMoney(cartItem.total?cartItem.total:0)}} ₫</td>
                            <!-- / -->


                            <td class="deleteProduct"><i class="fas fa-times" @click="deleteCart(cartItem.cartId)"></i></td>
                        </tr>
                        <!-- / -->

                        <!-- Hiển thị khi không có mặt hàng nào -->
                        <tr v-if="cart.length <= 0">
                            <td colspan="5">
                                Chưa có mặt hàng nào.
                            </td>
                        </tr>
                        <!-- / -->
                    </table>
                    <div id="cartFooter">
                        <router-link to="/home"><div class="btn btn-dark btnCart">Tiếp tục mua hàng</div></router-link>
                        <!-- <div class="btn btn-primary btnCart"><i class="fas fa-trash-alt"></i> Xóa toàn bộ</div> -->
                    </div>
                </div>
                <!-- end drop box -->
            </div>
            <div class="payInfor col-xl-3">
                <div class="saleTiket">
                    <p>Mã khuyến mại</p>
                    <span><a href="#"><i class="fas fa-ticket-alt"></i> Chọn mã khuyến mại</a></span>
                </div>
                <div class="payBox">
                    <p>Chọn khu vực giao hàng</p>
                    <form class="local">
                        <input type="radio" name="address"> <label>Bắc Từ Liêm - Hà Nội</label>
                        <br>
                        <input type="radio" name="address"> <label>Quận 10 - TP. HCM</label>
                    </form>
                    
                    <h4>Thành tiền: <span id="total">{{ formatMoney(total) }} ₫</span></h4>
                </div>
                <a href="/Cart/Checkout"><div class="btn-danger pay">Thanh toán</div></a>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios'
import path from '../../path'
export default {
    props:['products'],
    data() {
        return {
            //Biến lưu mảng chứa các cartItem
            cart: [],
            customer: {},
            total: 0,
        }
    },
    methods: {
        /**
         * created date: 10/11/2021
         * created by: vxkhanh
         * Hàm lấy thông tin sản phẩm từ mã id
         */
        getProductInfor(productId){
            for(var i=0; i<this.products.length; i++){
                //So sánh id để tìm được sản phẩm tương ứng
                if(this.products[i].productId == productId){
                    //Trả về thông tin sản phẩm
                    return this.products[i];
                }
            }
            //Không tìm thấy thì trả về null
            return null;
        },

        /**
         * created date: 11/11/2021
         * created by: vxkhanh
         * Hàm tính tổng giá tiền của cartItem
         */
        calculateTotal(price, quantity){
            var total = parseFloat(price) * parseFloat(quantity);
            return total
        },

        /**
         * created by: VXKHANH
         * created date: 11/11/2021
         * Hàm format giá tiền sản phẩm
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
            console.log(id);
            this.$router.push({
                name: 'productDetail', 
                path: '/productDetail', 
                params: { productId: id}
            });
        },

        /**
         * created date: 11/11/2021
         * created by: vxkhanh
         * Hàm xóa sản phẩm trong giỏ hàng
         */
        async deleteCart(cartId){
            var id = String(cartId);
            await axios.delete('https://localhost:44368/api/v1.0/carts/'+id).then(()=>{
                    this.loadCartData();
            }).catch(()=>{
                console.log("Đã có lỗi xảy ra");
            })
        },
        /**
         * created date: 11/11/2021
         * created by: vxkhanh
         * Hàm load lại dữ liệu khi xóa hoặc cập nhật giỏ hàng
         */
        async loadCartData(){
            if(localStorage.getItem('customer').trim() !== "")
            {
                this.customer = JSON.parse(atob(localStorage.getItem('customer')));
                await axios.get(path.cart+'customerId/'+this.customer.customerId).then((result)=>{
                    this.cart = result.data;
                    for(var j = 0; j<this.cart.length; j++){
                        //Lấy thông tin sản phẩm theo id trong biến cart
                        this.cart[j].product = new Object();
                        this.cart[j].product = this.getProductInfor(this.cart[j].productId);
                    }
                }).catch(()=>{
                    console.log("Đã có lỗi xảy ra");
                })
            }
        },

        /**
         * created date: 7/5/2022
         * created by: khanhvx
         * Hàm lấy ra tổng giá trị của các sản phẩm trong giỏ hàng
         */
        getTotal(){
            let all = 0;
            if(this.cart.length > 0){
                this.cart.forEach((element) => {
                    if(element.total){
                        all += parseInt(element.total);
                    }
                })
            }
            this.total = all;
        }
    },
    mounted() {
        
    },
    beforeCreate() {
        //console.log(this.products) 
    },
    beforeUpdated(){
        //console.log(this.products) 
    },
    async created() {
        //Gọi hàm load dữ liệu
        await this.loadCartData();
        this.getTotal();
    },
}
</script>

<style lang="css" scoped>
    @import url('../../css/page/cart.css');
</style>