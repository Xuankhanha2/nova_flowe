<template>
    <div class="productDetailPage">
        <!-- History -->
        <div class="widgetBar">
            <div class="history">
                <span class="parentPage"><a href="/Home">Trang chủ</a></span>
                <span style="padding: 0px 8px; color:brown">/</span>
                <span class="currentPage">Danh mục sản phẩm</span>
            </div>
        </div>
        <!-- history -->

        <!-- Mục hai -->
        <!-- chi tiet san pham -->
        <div class="productDetail">
            <!-- khoi trai -->
            <div class="blockLeft col-xl-4">
                <div class="productImage">
                    <div class="mainImage">
                        <img :src="product.image?product.image:'#'" id="anh-chinh" alt="">
                    </div>
                    <div class="otherImage">
                        <!-- <ul class="row">
                            <li class="col-xl-2"><img id="img" src="../../assets/images/tcmn-image1.jpg" alt="" class="active"></li>
                            <li class="col-xl-2"><img id="img" src="../../assets/images/tcmn-image1.jpg" alt=""></li>
                            <li class="col-xl-2"><img id="img" src="../../assets/images/tcmn-image1.jpg" alt=""></li>
                            <li class="col-xl-2"><img id="img" src="../../assets/images/tcmn-image1.jpg" alt=""></li>
                        </ul> -->
                    </div>
                </div>
            </div>
            <!-- /Khoi trai -->
            
            <!-- Khoi phai -->
            <div class="blockRight col-xl-4">
                <div class="productInfor">
                    <!-- Tên sản phẩm -->
                    <h1>{{product.productName}}</h1>
                    <!-- / -->

                    <!-- Số sao đánh giá -->
                    <div class="stars">
                        <i class="fas fa-star"></i>
                        <i class="fas fa-star"></i>
                        <i class="fas fa-star"></i>
                        <i class="fas fa-star"></i>
                        <i class="fas fa-star"></i>
                    </div>
                    <!-- / -->

                    <!-- Giá của sản phẩm -->
                    <h2>{{formatMoney(product.price)}}<sup>₫</sup></h2>
                    <!-- / -->

                    <!-- Mổ tả ngắn về sản phẩm -->
                    <div class="detail">
                        {{product.description}}
                    </div>
                    <!-- / -->

                    <!-- Thêm vào sản phẩm ưa thích -->
                    <div>
                        <a href="#" style="text-decoration: none !important;"><i class="far fa-heart"></i> Ưa thích</a>
                    </div>
                    <!-- / -->

                    <!-- Trạng thái của sản phẩm -->
                    <div class="productStatus">
                        Tình trạng:
                            <!-- Còn hàng -->
                            <span v-if="product.status">còn hàng <i class="far fa-check-circle"></i></span>
                            <!-- Hết hàng -->
                            <span v-else style="color: red !important;">Hết hàng <i class="far fa-times-circle"></i></span>
                    </div>
                    <!-- / -->

                    <!-- Số lượng trước khi thêm vào giỏ hàng -->
                    <div class="quantity">
                        <input type="button" id="decreaseBtn" value="-"
                            @click="decreaseNumber()"
                        >
                        <input type="text" name="number" id="soluong" v-model="number" size="1">
                        <input type="button" id="increaseBtn" value="+"
                            @click="increaseNumber()"
                        >
                    </div>
                    <!-- / -->

                    <!-- Nút thêm vào giỏ hàng -->
                    <input type="submit" class="btn btn-block btn-success" id="btnAddCart" value="Thêm vào giỏ">
                    <!-- / -->

                </div>
                <div class="extendInfor">
                    <div class="keyword">từ khóa: <a href="#">hoa quả</a></div>
                    <div class="page">
                        <a href="#"><i class="fab fa-facebook-f"></i></a>
                        <a href="#"><i class="fab fa-twitter"></i></a>
                        <a href="#"><i class="far fa-envelope"></i></a>
                        <a href="#"><i class="fab fa-pinterest"></i></a>
                        <a href="#"><i class="fab fa-invision"></i></a>
                    </div>
                </div>
            </div>
            <!-- /khoi phai -->
            <!-- Phan mo rong -->
            <div class="moreChoice col-xl-3 offset-xl-1">
                <!-- catogory -->
                <sideCategoryBar/>
                <!-- /catogory -->

                <!-- quang cao -->
                <!-- /quang cao -->
            </div>
            <!-- /Phan mo rong -->
        </div>
        <!-- end noi dung chi tiet san pham -->

        <!-- Mo ta - danh gia san pham -->
        <div class="otherDetailContent">
            <div class="descriptionAndRating col-xl-8">
                <!-- description -->
                <div class="">
                    <div class="descriptionTitle">Mô tả</div>
                    <div class="descriptionContent shortenContent">
                        {{product.description}}
                    </div>
                </div>
                <!-- end description -->

                <!-- Rating -->
                <div class="descriptionTitle">Đánh giá</div>
                <div class="rating">
                    <div class="ratingTitle">Đánh giá cho Name product</div>
                    <div class="ratingContent">
                        <div class="curentScore">
                            <span>0.00 </span>
                            <i class="fas fa-star starColor"></i>
                            <i class="fas fa-star starColor"></i>
                            <i class="fas fa-star starColor"></i>
                            <i class="fas fa-star starColor"></i>
                            <i class="fas fa-star starColor"></i>
                        </div>
                        <ul>
                            <li>
                                <span>5</span><i class="fas fa-star"></i><div class="ratingPercentBar"><div class="ratingPercent"> </div></div>
                                <label for="">0% | 0 Đánh giá</label>
                            </li>
                            <li>
                                <span>4</span><i class="fas fa-star"></i><div class="ratingPercentBar"><div class="ratingPercent"> </div></div>
                                <label for="">0% | 0 Đánh giá</label>
                            </li>
                            <li>
                                <span>3</span><i class="fas fa-star"></i><div class="ratingPercentBar"><div class="ratingPercent"> </div></div>
                                <label for="">0% | 0 Đánh giá</label>
                            </li>
                            <li>
                                <span>2</span><i class="fas fa-star"></i><div class="ratingPercentBar"><div class="ratingPercent"> </div></div>
                                <label for="">0% | 0 Đánh giá</label>
                            </li>
                            <li>
                                <span>1</span><i class="fas fa-star"></i><div class="ratingPercentBar"><div class="ratingPercent"> </div></div>
                                <label for="">0% | 0 Đánh giá</label>
                            </li>
                        </ul>
                        <div class="ratingBtn bg-primary" @click="ratingRow()">Đánh giá ngay</div>
                    </div>

                    <div class="ratingForm">
                        <form action="#" method="post">
                            <!-- <input type="textarea" row id="txtbox" placeholder="viết đánh giá..."> -->
                            <div class="ratingStars">
                                <h2>Bạn đánh giá sản phẩm này bao nhiêu sao?</h2>
                                <p id="move-rating">
                                    <i class="fas fa-star" id="star1"></i>
                                    <i class="fas fa-star" id="star2"></i>
                                    <i class="fas fa-star" id="star3"></i>
                                    <i class="fas fa-star" id="star4"></i>
                                    <i class="fas fa-star" id="star5"></i>
                                </p>
                            </div>
                            <textarea name="ratingComment" ref="txt" id="txtbox" placeholder="viết đánh giá..."></textarea>
                            <button class="btnSend">Gửi đánh giá</button>
                        </form>
                    </div>

                    <div class="ratingTitle" style="margin-top: 30px; margin-bottom: 5px;">Một số đánh giá của khách hàng</div>
                    <div class="productRatingList">
                        <div class="ratingLine">
                            <div class="userInfor">
                                <div class="userAvatar"><img src="../../assets/images/avatar-placeholder.png" alt=""></div>
                                <div class="userRatingContent">
                                    <h3>Vu Xuan Khanh</h3>
                                    <!-- <div class="timeRating">
                                        <span class="hour">11</span>:<span class="minute">20</span>
                                    </div> -->
                                    <h4>
                                        <i class="fas fa-star starColor" ></i>
                                        <i class="fas fa-star starColor" ></i>
                                        <i class="fas fa-star starColor" ></i>
                                        <i class="fas fa-star" ></i>
                                        <i class="fas fa-star" ></i>
                                    </h4>
                                    <p>Sản phẩm rất tốt</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- san pham noi bat -->
            <div class="suggestedProduct col-xl-3 offset-xl-1">
                <recommendProduct/>
            </div>
        </div>
        <!-- end Mo ta - danh gia san pham -->
        <!-- chi tiet san pham -->
    </div>
</template>

<script>
import path from '../../path'
import sideCategoryBar from '../shared/sideCategoryBar.vue'
import axios from 'axios'
import recommendProduct from '../shared/recommendProduct.vue'
/**
 * created date: 19/06/2021
 * created by: VXKHANH
 */
export default {
    data() {
        return {
            number: 1,
            product: {},
        }
    },
    components:{
        sideCategoryBar,
        recommendProduct
    },
    methods: {
        /**
         * created by: vxkhanh
         * created date: 21/6/2021
         * Hàm dùng để chuyển đến phần dánh giá sản phẩm khi nhấn nút "Đánh giá ngay"
         */
        ratingRow(){
            this.$refs.txt.focus();
            //var element = this.$refs.txt;
            //var top = element.offsetTop;
            //window.scrollTo(0, top+200);
        },
        /**
        created date: 12/7/2021
        created by: VXKHANH
        Hàm sửu dụng để cộng thên số lượng sản phẩm
         */
        increaseNumber() {
            this.number++;
        },

        /**Hàm giảm số lượng sản phẩm 
         * created by: VXKHANH
         * created date 1/11/2021
         */
        decreaseNumber(){
            if(this.number > 1)
                this.number--;
        },
        /**Hàm format giá tiền sản phẩm
         * created by: VXKHANH
         * created date: 30/10/21
         */
        formatMoney(money){
            //Chuyển đổi tiền
            var formatedMoney = String(money).replace(/(\d)(?=(?:\d{3})+$)/g, '$1.');
            return formatedMoney;
        },
        
    },
    async created() {
        let id = this.$route.params.productId
        if(id && String(id).trim() !== null)
            localStorage.setItem('productId', btoa(id));
        id = atob(localStorage.getItem('productId'));
        //Lấy thông tin sản phẩm theo id trên router
        await axios.get(path.products+id).then((result)=>{
            this.product = result.data;
        }).catch(()=>{
            return;
        });
    },
}
</script>

<style lang="css" scoped>
    @import url("../../css/page/productDetailPage.css")
</style>