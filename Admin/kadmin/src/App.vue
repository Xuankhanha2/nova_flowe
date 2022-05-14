<template>
  <div id="app">
    <contentLayout v-if="true"/>
  </div>
</template>
<script>
import contentLayout from './components/layout/content.vue'
// import loginPage from './components/pages/login.vue'
export default {
  name: 'App',
  data() {
    return {
    }
  },
  components: {
    // loginPage,
    contentLayout,
  },
  created(){
    // Kiểm tra login 
    if(localStorage.getItem('accessToken')){
      this.redirectToLogin()
    }
  },
  methods:{
    /**
     * created by: khanhvx
     * created date: 23/04/2022
     * Hàm chuyển vê trang đănh nhập
     */
    redirectToLogin(){
      this.$router.push('/login');
    }
  }
}
</script>
<style>
body{
  width: 100%;
  height: 100vh;
  margin: 0px;
  background-color: #EAECEF;
}
@font-face{
  font-family: OpenSan-Regular;
  src: url('./assets/Fonts/OpenSans-Regular.ttf');
}
@font-face {
  font-family: OpenSan-Bold;
  src: url('./assets/Fonts/OpenSans-Bold.ttf');
}
@font-face {
  font-family: OpenSan-Semibold;
  src: url('./assets/Fonts/OpenSans-Semibold.ttf');
}
@font-face {
  font-family: OpenSan-Italic;
  src: url('./assets/Fonts/OpenSans-Italic.ttf');
}
#app {
  font-family: OpenSan-Regular;
  margin: 0px;
  width: 100%;
  display: flex;
}

select, input{
  font-family: OpenSan-Regular;
}
@import url('./css/fontawesome-free-6.1.1-web/css/all.css');
@import url('./css/bootstrap4/css/bootstrap.css');
</style>
