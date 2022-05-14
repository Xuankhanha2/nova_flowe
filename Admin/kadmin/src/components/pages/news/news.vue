<template>
    <div class="revenue">
        <div class="grid">
            <!-- Dòng chứa nút sắp xếp dữ liệu -->
            <div class="filter-line">
                <div class="filter-left">
                    
                </div>
                <div class="filter-right">
                    <newButton 
                        :Text="'Thêm mới'"
                        :second="false"
                        @click.native="openForm(false, {})"
                        />
                    <newButton
                    :Text="'Sắp lại thứ tự'"
                        :second="true"
                        />
                    <div class="trash-btn"></div>
                </div>
            </div>
            <!-- / -->

            <!-- Lưới dữ liệu -->
            <div class="grid-list">
                <div class="outline-table">
                <table cellspacing="0" cellpadding="0" border="0">
                    <!-- Dòng tiêu đề của bảng -->
                    <tr>
                        <th colspan="1"> </th>

                        <!-- Ảnh hiển thị  -->
                        <th colspan="1" class="td-to-check">Ảnh hiển thị</th>
                        <!-- /Ảnh hiển thị -->

                        <!-- Tiêu đề của tin tức -->
                        <th colspan="1" class="col-group-revenue">
                            <div class="th-text">Tiêu đề tin</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th>
                        <!-- /Tiêu đề của tin tức -->

                        <!-- Nguồn tin tức hay tác giả -->
                        <!-- <th colspan="1" class="col-group-revenue">
                            <div class="th-text">Nguồn</div>
                            <div class="btn-filter"><input type="text"></div>
                        </th> -->
                        <!-- /Nguồn tin tức hay tác giả -->

                        <!-- Ngày tạo tin tức -->
                        <th colspan="1" class="td-to-check">Ngày tạo tin</th>
                        <!-- / -->

                        <!-- Tin hot -->
                        <th colspan="1" class="td-to-check">Tin hot</th>
                        <!-- /Tin hot -->

                        <!-- Nút chức năng -->
                        <th colspan="1" class="maxWidth100"></th>
                        <!-- /Nút chức năng -->
                    </tr>
                    <!-- Các dòng dữ liệu -->
                    <tr v-for="news in newsList" :key="news.newsId"
                        :class="{'row-focus':news.selectedItem}"
                    >
                        <td colspan="1">
                            <div class="select-line"
                                :class="{'selected-line':news.selectedItem}"
                                @click="news.selectedItem = !news.selectedItem"
                            ></div>
                        </td>

                        <!-- Ảnh chính của tin tức -->
                        <td colspan="1" class="imageCell">
                            <img :src="news.newsImage" />
                        </td>
                        <!-- /Ảnh của tin tức -->

                        <!-- Tiêu đề của tin tức -->
                        <td colspan="1" class="colLinked"
                            @click="openForm(true, news)"
                        >{{news.newsTitle}}</td>
                        <!-- /tiêu đề tin tức -->

                        

                        <!-- Nguồn tin tức -->
                        <!-- <td colspan="1" class="">{{news.newsBy}}</td> -->
                        <!-- /Nguồn tin tức -->
                        
                        <!-- Ngày tạo tin tức -->
                        <td colspan="1" class="">{{formatDate(String(news.createdDate))}}</td>
                        <!-- / -->

                        <!-- Tin hot -->
                        <td colspan="1" class="td-to-check"><div :class="{'cell-checking':news.hotNews}"></div></td>
                        <!-- /Tin hot -->

                        <!-- Nút chức năng -->
                        <td colspan="1" class="maxWidth100">
                            <div class="last-cell">
                                <div class="btn-edit" title="Chỉnh sửa dòng dữ liệu này"
                                    @click="openForm(true, news)"
                                ></div>
                                <div class="btn-delete" title="Xóa dòng dữ liệu này"
                                    @click="showDeletePopup(news.newsId)"
                                ></div>
                            </div>
                        </td>
                        <!-- /Nút chức năng -->
                    </tr>
                </table>
                </div>
            </div>
            <div class="footer">
                <div class="result">
                    Tổng số: <span class="count">{{newsList.length}}</span> kết quả.
                </div>
            </div>
            <!-- /Lưới dữ liệu -->
        </div>
        <!-- Form thêm sửa tin tức -->
        <newsDetail
            @loadData="loadData"
            v-if="showPopup"
            :isUpdate="isUpdate"
            :pageTitle="newsDetailTitle"
            :refNews="this.news"
            :config="config"
            @closeForm="closeForm"
        />
        <!-- /Form thêm sửa tin tức -->

        <!-- Popup xóa tin tức -->
        <popup 
            :text="'Bạn có muốn xóa danh mục này?'"
            v-if="deletePopup"
            :firstBtn="true"
            :secondBtn="true"
            @closePopup="closePopup"
            @negativeFunction="deleteCategory"
            :param="popupParam"
        />
        <!-- /Popup xóa tin tức -->

        <!-- Popup Thông báo khi có lỗi -->
        <popup
            :text="notifyText"
            :thirdBtn="true"
            @closePopup="closePopup"
            v-if="notifyPopup"
        />
        <!--/Popup thong thông báo khi có lỗi -->
        
        <!-- Ảnh loading của trang  -->
        <div class="preload" v-show="loading"></div>
    </div>
</template>

<script>
/*
    created by: vu xuan khanh
    created date: 27/7/2021
*/
import axios from 'axios'
import newsDetail from './newsDetail.vue'
import newButton from '../../layout/button.vue'
import popup from '../../popup/notifyPopup.vue'
import apiPath from '../../../path'
export default {
    data() {
        return {
            /**Biến tiêu đề của popup thêm - sửa tin tức */
            newsDetailTitle: '',
            /** Biến kiểm tra popup thêm sửa hiển thị lên là thêm hay sửa*/
            isUpdate:false,
            /**Biến mở - đóng popup thêm - sửa */
            showPopup: false,
            //Biến dùng để lưu danh sách tin tức, sử dụng để tải dữ liệu vào ô chọn danh mục sản phẩm
            newsList: [{}],
            //Biến hiển thị thông báo hỏi khi xóa sản phẩm
            deletePopup: false,
            //Biến hiển thị thông báo
            notifyPopup:false,
            //Biến chứa thông điệp khi thông báo
            notifyText: 'Đã có lỗi xảy ra.',
            //Biến dùng để truyền prop cho popup
            popupParam:'',
            //Biến category dùng để truyền prop cho form cập nhật danh mục sản phẩm
            news: {},
            //Biến sử dung để hiển thị loading khi load dữ liệu 
            loading: false,
            config: {},
        }
    },
    components:{
        newsDetail,
        newButton,
        popup
    },
    watch:{
        
    },

    methods:{
        /**Bảng mở form thêm - sửa sản phẩm */
        openForm(check, news){
            //nếu true  thì form mở lên là form sửa
            this.isUpdate = check;
            if(this.isUpdate){
                this.newsDetailTitle="Cập nhật tin tức"
                this.news = news;
            }   
            else{
                this.newsDetailTitle="Thêm tin tức mới"
                this.news = null;
            }
               
            this.showPopup = true;
        },
        /**Hàm đóng form thêm sửa sản phẩm */
        closeForm(){
            this.showPopup = false;
        },
        /**Hàm hiển thị nhứng sản phẩn đã ngừng theo dõi */
        showStopBusiness(){
            this.stopBusiness = !this.stopBusiness;
        },
        
        /**Hàm hiển thị popup hỏi người dùng có xóa sản phẩm này hay ko */
        showDeletePopup(id){
            this.popupParam = id;
            this.deletePopup = true;
        },
        /**Hàm đóng popup */
        closePopup(){
            this.notifyText= "";
            this.deletePopup = false;
            this.notifyPopup = false;
        },
        /**Hàm xóa sản phẩm theo id */
        async deleteCategory(id){
            //Gửi yêu cầu lên server
            await axios.delete(apiPath.news+id, this.config).then((result)=>{
                //Đưa ra thông báo thành công
                this.notifyText = "Đã xóa sản phẩm vừa chọn.";
                //Hiển thi thông báo
                this.notifyPopup = true;
                //Load lại dữ liêu khi xóa thành công
                this.loadData();
                return result;
            }).catch(()=>{
                //Gán thông báo lỗi vào biến thông báo
                this.notifyText = "Không thể xóa sản phẩm này";
                //Hiển thi thông báo lỗi
                this.notifyPopup = true;
            })
        },
        /**Hàm load lại dữ liêu sau khi thêm sửa xóa */
        async loadData(){
            //Hiển thị ảnh load khi dữ liệu chưa tải xong
            this.loading = true;
            //Lấy dữ liệu trên server
            await axios.get(apiPath.news).then((result)=>{
                //Gán dữ liệu trả về vào biến newsList
                this.newsList = result.data;
            }).catch(()=>{
                //Hiển thị lỗi trên màn hình console
                console.log('Đã có lỗi xảy ra khi lấy categories');
            });
            //Ẩn ảnh load dữ liệu khi dữ liệu đã được tải xong
            this.loading = false;
        },

        /**Hàm định dạng ngày */
        formatDate(datetime){
            if(datetime != null){
                var date = datetime.substr(0, 10);
                date = date.split("-");
                var formatedDate = [date[2],date[1],date[0]].join("/");
                return formatedDate;
            }
            return "";
        },
        /**
         * created date: 27/04/2022
         * created by: khanhvx
         * hàm lấy tokken từ localStorage
         */
        getToken(){
            if(localStorage.getItem('accessToken'))
                return localStorage.getItem('accessToken');
            else
                return "";
        }

    },

    async created() {
        this.config = {
            headers: { Authorization: `Bearer ${this.getToken()}` }
        }
        //Lấy dữ liệu trên server
        this.loadData()
    },
}
</script>

<style lang="css">
    @import '../../../css/dictionary/product/productPage.css';  
</style>