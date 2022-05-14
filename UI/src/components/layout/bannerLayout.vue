<template>
  <div class="banner">
    <div class="bannerLogo">
      <a href="/Home/index"
        ><img src="../../assets/images/Banner-logo.png" alt=""
      /></a>
    </div>
    <div class="bannerContent">
      <ul>
        <li>
          <img src="../../assets/images/banner-image1.jpg" />
          <div>
            <h1>Giao hàng miễn phí</h1>
            <p>Bán kính 10 km</p>
          </div>
        </li>
        <li>
          <img src="../../assets/images/banner-image2.jpg" alt="" />
          <div>
            <h1>Hỗ trợ trực tuyến 24/7</h1>
            <p>Hotline: 19001070</p>
          </div>
        </li>
        <li>
          <img src="../../assets/images/banner-image3.png" alt="" />
          <div>
            <h1>Đảm bảo chất lượng</h1>
            <p>Hoa tươi nhập khẩu</p>
          </div>
        </li>
      </ul>

      <div class="cart">
        <i class="fas fa-shopping-bag"></i>
        <router-link to="/cart">Giỏ hàng</router-link>
        <div id="countCart"><span>0</span></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style lang="css" scoped>
@import url("../../css/layout/banner.css");
</style>