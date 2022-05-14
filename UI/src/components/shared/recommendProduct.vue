<template>
    <div class="recommendProduct">
        <div class="catogoryTitle">
            <i class="fas fa-carrot"></i> SẢN PHẨM NỔI BẬT
        </div>
        <div class="suggestedProductContent">
            <ul>
                <li v-for="product in products"
                    :key="product.productId"
                >
                    <img :src="product.image">
                    <div class="suggestedProductInfor">
                        <p>{{product.productName}}</p>
                        <h3>{{product.price}}<sup>₫</sup></h3>
                        <h4>
                            <i class="fas fa-star"></i>
                            <i class="fas fa-star"></i>
                            <i class="fas fa-star"></i>
                            <i class="fas fa-star"></i>
                            <i class="fas fa-star"></i>
                        </h4>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</template>
<script>
/**created by: VXKHANH
 * created date: 31/10/2021
 */
import axios from 'axios'
export default {
    data() {
        return {
            products: [],
        }
    },
    async created(){
        await axios.get('https://localhost:44368/api/v1.0/products').then((result)=>{
            this.products = result.data.slice(0,6);
        }).catch(()=>{
            return;
        });
    }
}
</script>
<style lang="css" scoped>
    
</style>