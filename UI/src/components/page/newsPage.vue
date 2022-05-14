<template>
    <div class="newsPage">
        <div class="listTitle">
            Danh sách tin tức
        </div>
        <div class="listNews">
            <ul class="row col-xl-12">
                <li class="col-xl-6" 
                    v-for="item in news"
                    :key="item.newsId"
                >
                    <img :src="item.newsImage" class="News-img">
                    <div class="newsRow">
                        <h3 class="" @click="goToNewsContent(item.newsId)">{{item.newsTitle}}</h3>
                        <p 
                            @click="goToNewsContent(item.newsId)"
                        >
                            {{item.newsContent}}
                        </p>
                        <a href="/News/Detail/@item.id"><div class="btnDetail btn-success">Chi tiết</div></a>
                    </div>
                </li>
            </ul>
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

        <!-- New  -->
        <!-- Dịch vụ  -->
        <div class="serviceTitle">
            <p>CHÍNH SÁCH</p>
            <hr>
        </div>
        <div class="service">
            <div class="serviceContent">
                <img src="../../assets/images/policy_left.jpg">
                <div class="policy">
                    Chính sách vận chuyển<br>
                    <span>Miễn phí trong nội thành</span>
                </div>
            </div>
            <div class="serviceContent">
                <img src="../../assets/images/policy_right.jpg">
                <div class="policy">
                    Chính sách thanh toán<br>
                    <span>Thanh toán online được giảm 2%</span>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios'
export default {
    data() {
        return {
            news: [],
        }
    },
    methods: {
         /**
         * created date: 19/11/2021
         * created by: vxkhanh
         * Hàm xủ lý sư kiện chuyển số trang sang trang tiếp theo cho phần tin tức
         */
        async goToPageForNews(page){
            page = parseInt(page);
            await axios.get('https://localhost:44368/api/v1.0/news?pageNumber='+page+'&items=20').then((result)=>{
                this.news = result.data;
            }).catch(()=>{
                console.log("Đã có lỗi xảy ra.")
            })
        },
         /**
         * created date: 26/11/2021
         * created by: vxkhanh
         * Hàm chuyển đến trang nội dung tin tức
         */
        goToNewsContent(newsId){
            var id = String(newsId);
            this.$router.push({
                name: 'newsContent',
                path: '/newsContent',
                params: { newsId: id }
            });
        }
    },
    async created() {
        this.news = new Array;
        await axios.get('https://localhost:44368/api/v1.0/News?pageNumber=1&items=20').then((result)=>{
            this.news = result.data;
        }).catch(()=>{
            console.log("Đã có lỗi xảy ra khi lấy dữ liệu tin tức");
        })

    },
}
</script>

<style lang="css">
    @import url('../../css/page/newsPage.css');
</style>
