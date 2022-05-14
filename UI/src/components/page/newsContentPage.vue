<template>
    <div class="newsContentPage">
        <!-- Thanh tiện ích -->
        <div class="widgetBar">
            <div class="history">
                <span class="parentPage"><router-link to="/home">Trang chủ</router-link></span>
                <span style="padding: 0px 8px; color:brown">/</span>
                <span class="currentPage">Tin tức</span>
            </div>
        </div>
        <!-- / -->

        <!-- Nội dun tin tức -->
        <div class="middleBox row">
            <div class="col-xl-9">
                <h1 id="newsTitle">{{news.newsTitle}}</h1>
                <div id="newsImage"><img :src="news.newsImage"></div>
                <div id="newsContent">
                    {{news.newsContent}}
                </div>
            </div>
            <div class="col-xl-3" id="sideBar">

            </div>
        </div>
        <!-- / -->

        <!-- Tin tức khác -->
        <div class="serviceTitle">
            <p>TIN TỨC KHÁC</p>
            <hr>
        </div>
        <div class="newsContent">
            <div class="newsList col-xl-12">
                <div class="newsCell"
                    v-for="news in otherNews"
                    :key="news.newsId"
                >
                    <img src="../../assets/images/tintuc-image1.jpg" alt="">
                    <h1><a href="#">{{news.newsTitle}}</a></h1>
                    <p>{{news.newsContent}}</p>
                    <a href="#"><div class="detailNewsBtn">Chi tiết</div></a>
                </div>
            </div>

            <nav class="newsPageList">
                <ul class="pagination alignRight">
                    <li class="page-item"><span class="page-link" @click="goToPageForNews(1)">Prev</span></li>
                    <li class="page-item"><span class="page-link" @click="goToPageForNews(1)">1</span></li>
                    <li class="page-item"><span class="page-link" @click="goToPageForNews(2)">2</span></li>
                    <li class="page-item"><span class="page-link" @click="goToPageForNews(3)">3</span></li>
                    <li class="page-item"><span class="page-link" @click="goToPageForNews(1)">Next</span></li>
                </ul>
            </nav>
        </div>
        <!-- /Tin tuc -->
        
    </div>
</template>

<script>
import axios from 'axios'
export default {
    data(){
        return{
            //Danh sách tin tức khác
            otherNews: [],
            //tin chính
            news:{},
        }
    },
    methods: {
        
    },
    async created(){
        console.log(this);
        this.otherNews = new Array;
        var id = this.$route.params.newsId;
        if(id != null){
            await axios.get('https://localhost:44368/api/v1.0/News/'+id).then((result)=>{
                 this.news = result.data;  
            }).catch(()=>{
                console.log('Đã xảy ra lỗi trong quá trình lấy dữ liệu tin tức');
            })
        }else{
            this.news = {};
        }
    },

    
}
</script>

<style lang="css">
    @import url('../../css/page/newsContent.css');
</style>