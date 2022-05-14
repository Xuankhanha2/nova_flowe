import { createRouter, createWebHistory } from 'vue-router'
import category from '../components/page/categoryPage.vue'
import home from '../components/page/homePage.vue'
import productDetail from '../components/page/productDetailPage.vue'
import contact from '../components/page/contactPage.vue'
import introduction from '../components/page/introductionPage.vue'
import cart from '../components/page/cart.vue'
import news from '../components/page/newsPage.vue'
import login from '../components/page/loginPage.vue'
import register from '../components/page/registerPage.vue'
import provider from '../components/page/providerPage.vue'
import newsContent from '../components/page/newsContentPage.vue'
const routes = [
  {
    path: '/',
    name: 'firstPage',
    component: home
  },
  { name: 'category', path: '/category', component: category },
  { name: 'home', path: '/home', component: home },
  { name: 'productDetail', path: '/productDetail', component: productDetail },
  { name: 'contact', path: '/contact', component: contact },
  { name: 'introduction', path: '/introduction', component: introduction },
  { name: 'cart', path: '/cart', component: cart },
  { name: 'news', path: '/news', component: news },
  { name: 'login', path: '/login', component: login },
  { name: 'register', path: '/register', component: register },
  { name: 'provider', path: '/provider', component: provider },
  { name: 'newsContent', path: '/newsContent', component: newsContent},
  
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
