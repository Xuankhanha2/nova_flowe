<template>
  <div class="content">
    <menuLayout v-if="!isLogin"/>
    <div class="right-box" >
      <headerLayout v-if="!isLogin"/>
      <router-view/>
    </div>
  </div>
</template>

<script>
import menuLayout from '../../components/layout/menu.vue'
import headerLayout from '../../components/layout/header.vue'
export default {
  data() {
    return {
      isLogin: false,
    };
  },
  components: {
    menuLayout,
    headerLayout,
  },
  created(){

  },
  methods: {
    
  },
};
</script>

<style lang="css">
.content{
  width: 100%;
  display: flex;
  position: relative;
}
.preload {
  position: absolute;
  top: 0px;
  left: 0px;
  width: 100%;
  height: 100%;
  background-image: url("../../assets/image/messenger-typing-preloader.svg");
  background-repeat: no-repeat;
  background-position: center;
  background-size: 100px 100px;
  z-index: 200;
  background-color: #000000;
  opacity: 0.3;
}
.right-box{
  width: calc(100% - 220px);
}
</style>