import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard } from '@bcwdev/auth0provider-client'
import HomeChildRoute from '../src/components/HomeChildRoute.vue'
import FavoritesChildRoute from '../src/components/FavoritesChildRoute.vue'
import MyRecipesChildRoute from '../src/components/MyRecipesChildRoute.vue'
function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage'),
    children: [
      {
        path: '/',
        name: 'HomeChild',
        component: HomeChildRoute
      },
      {
        path: 'favorite',
        name: 'Favorite',
        component: FavoritesChildRoute
      },
      {
        path: 'recipe',
        name: 'Recipe',
        component: MyRecipesChildRoute
      }
    ]
  },

  {
    path: '/about',
    name: 'About',
    component: loadPage('AboutPage')
  },
  {
    path: '/account',
    name: 'Account',
    component: loadPage('AccountPage'),
    beforeEnter: authGuard
  }
]

export const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})
