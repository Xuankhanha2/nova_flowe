<template>
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
        <h2>{{formatMoney(product.price)}} đ</h2>

        <!-- price after discount -->
        <p>{{formatMoney( calculatePrice(product.price, product.discount) )}} ₫</p>

        <!-- discount percent -->
        <div class="sale" v-if="product.discount > 0">-{{product.discount}}%</div>

        <!-- button add wishlist -->
        <!-- <div class="wishlist"><i class="far fa-heart"></i></div> -->

        <!-- button Thêm vào giỏ -->
        <div class="btn btn-primary" id="btnAddCart"
            @click="addCart(product.productId)"
        >Thêm vào giỏ</div>
    </div>
</template>

<script>

export default {
    props:{
        product: Object
    },
    data() {
        return {
            
        }
    },
    created(){

    },
    methods:{
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
         * created by: khanhvx
         * created date: 8/5/2022
         * Hàm chuyển đến trang chi tiết sản phẩm 
         */
        pushToDetail(productId){
            this.$emit("pushToDetail", productId);
        },

        /**
         * created by: khanhvx
         * created date: 8/5/2022
         * Hàm thêm sản phẩm vào giỏ hàng
         */
        addCart(productId){
            this.$emit("addCart", productId);
        },

        /**
         * created by: khanhvx
         * created date: 8/5/2022
         * Hàm tính giá tiền sau khi discount
         */
        calculatePrice(price, discount){
            if(discount > 0){
                let acctualyPrice = price - Math.ceil((discount/100)*price);
                return acctualyPrice;
            }else{
                return price;
            }
        }
    }
}

</script>

<style lang="css">
.productCell{
    border: 1px solid #77778d;
    padding: 15px !important;
    border-radius: 16px;
    position: relative;
    text-align: center;
    margin: 0px !important;
}

.productCell img {
    width: 100%;
    height: 245px;
    object-fit: contain;
    object-position: center;
    cursor: pointer;
}

.productCell h1 {
    margin: 0px;
    padding: 0px;
    font-size: 16px;
    color: #ff5722;
    text-align: center;
    font-weight: bold;
    margin-top: 12px;
    cursor: pointer;
}


.productCell a:hover {
    text-decoration: none;
}

.productCell h1:hover {
    color: #5c3d21;
}

.ratingStars {
    margin: auto;
    text-align: center;
    margin-top: 8px;
}

.ratingStars i {
    color: #fe9727 !important;
}

.productCell h2 {
    padding: 0px;
    margin: 0px;
    text-decoration: line-through;
    color: #77778d;
    font-size: 13px;
    font-style: italic;
    text-align: center;
    line-height: 24px;
}

.productCell p {
    text-align: center;
    font-size: 16px;
    font-weight: bold;
    color: #f99717;
    padding: 0px;
    margin: 0px;
    line-height: 30px;
}
#btnAddCart{
    font-size: 16px !important;
    margin-right: 0px !important;
    margin-left: 0px !important;
}
.sale{
    font-size: 13px;
}

.productCell .wishlist {
    position: absolute;
    right: 10px;
    top: 10px;
    font-size: 18px;
    color: red;
}
.productCell a:hover{
    color: #ffffff;
    text-decoration: none;
}
.productCell .sale {
    background-color: red;
    color: white;
    position: absolute;
    top: 30px;
    left: 0px;
    padding: 5px 10px;
    z-index: 99;
}

</style>