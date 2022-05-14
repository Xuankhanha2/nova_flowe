<template>
    <div class="sideBar">
            <div class="catogory-title">
                <i class="fas fa-carrot"></i> DANH MỤC SẢN PHẨM
            </div>
            <div class="catogory-content">
                <ul> 
                    <li 
                        v-for="category in categories" 
                        :key="category.categoryId"
                        @click="pushToCategory(category.categoryId)"
                    >
                        <span>
                            <i class="fab fa-envira"></i>{{category.categoryName}}
                        </span>
                    </li>
                </ul>
            </div>
        </div>
</template>

<script>
import axios from 'axios'
export default {
    data() {
        return {
            categories: [],
        }
    },
    methods:{
        /***
         * created date: 12/11/2021
         * created by: vxkhanh
         * Hàm chuyển đến trang danh mục với id tương ứng
         */
        pushToCategory(categoryId){
            var id = String(categoryId);
            this.$router.push({ name: 'category', path:'/category', params: {categoryId: id}});
        }
    },
    async created() {
        await axios.get('https://localhost:44368/api/v1.0/categories').then((result)=>{
            this.categories = result.data;
        })
    },
}
</script>

<style lang="css">
.slideBar {
    margin: 0px;
    padding: 0px;
    width: 100%;
}

.catogory-title {
    line-height: 35px;
    background-color: #f99717;
    border-radius: 5px 5px 0px 0px;
    padding-left: 15px;
    color: white;
    font-weight: bold;
    font-size: 16px;
}

.catogory-content{
    margin: 0px;
    padding: 0px;
    width: 100%;
    height: 300px;
    border: 1px solid #f99717;
    padding-bottom: 5px;
    border-radius: 0px 0px 5px 5px;
    overflow-y: scroll;
}

.catogory-content::-webkit-scrollbar {
    width: 5px;
    border-radius: 5px;
    display: none;
}

.catogory-content::-webkit-scrollbar-button {
    display: none;
}

.catogory-content::-webkit-scrollbar-thumb {
    background: darkred;
}

.catogory-content ul {
    margin: 0px;
    padding: 0px;
    list-style: none;
}

.catogory-content ul li {
    line-height: 35px;
    padding-left: 25px;
    background-color: #fef79c;
    margin-bottom: 1px;
    cursor: pointer;
}

.catogory-content ul li span {
    color: #5c3d21;
    font-size: 14px;
    text-decoration: none;
}

.catogory-content ul li:hover {
    background-color: #ffcc4d;
}

.catogory-content ul li i {
    color: #258031;
    margin-right: 10px;
}
</style>