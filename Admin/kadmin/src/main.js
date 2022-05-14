
import App from './App.vue'

import Vue from 'vue'
import VueRouter from 'vue-router'

import productPage from './components/pages/product/product.vue'
import categoryPage from './components/pages/category/category.vue'
import newsPage from './components/pages/news/news.vue'
import customerPage from './components/pages/customer/customer.vue'
import loginPage from './components/pages/login.vue'
import dashboard from './components/pages/dashboard/dashboard.vue'
Vue.use(VueRouter)

import CKEditor from '@ckeditor/ckeditor5-vue2';

Vue.use(CKEditor);

const routes = [
  { path: '/login', name: 'login', component: loginPage },
  { path: '/overview', name: 'overview', component: dashboard },
  { path: '/product', name: 'product', component: productPage, meta: { requiresAuth: true } },
  { path: '/category', name: 'category', component: categoryPage, meta: { requiresAuth: true } },
  { path: '/news', name: 'news', component: newsPage, meta: { requiresAuth: true } },
  { path: '/customer', name: 'customer', component: customerPage, meta: { requiresAuth: true } }
]

const router = new VueRouter({
  routes // short for `routes: routes`
})

router.beforeEach(async (to, from, next) => {
  // Kiếm tra nếu router yêu cầu authen thì sẽ vào case authen
  if (to.matched.some(record => record.meta.requiresAuth)) {
    // if not, redirect to login page.
    if (localStorage.getItem('accessToken')) {
      next();
    } else {
      next({ name: 'login' });
    }
  } else {
    // Chuyển đến trang đích nếu không cần authen
    next();
  }
})

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')


// createApp(App).use(router).mount('#app')
