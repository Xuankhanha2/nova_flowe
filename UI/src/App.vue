<template>
  <div id="app">
      <headerLayout/>
      <bannerLayout/>
      <menuLayout/>
      <contentLayout/>
      <footerLayout/>
  </div>
</template>

<script>

import headerLayout from './components/layout/headerLayout'
import bannerLayout from './components/layout/bannerLayout'
import contentLayout from './components/layout/contentLayout'
import footerLayout from './components/layout/footerLayout'
import menuLayout from './components/layout/menuLayout'

export default {
  components:{
    headerLayout,
    bannerLayout,
    contentLayout,
    footerLayout,
    menuLayout
  }
}
</script>

<style>
html{
  padding: 0px; 
  margin: 0px;
}
body{
  margin: 0px;
  padding: 0px;
}
#app {
  font-family: sans-serif;
}
@import url('./css/bootstrap/bootstrap.css')
</style>
