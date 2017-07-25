import * as mdc from 'material-components-web';
import Vue from 'vue';
import VueRouter from 'vue-router';
import * as Home from './home/home'
Vue.use(VueRouter);

const routes = [
    { path: '/', component: Home },   
];

new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h('./app.vue.html')
});

mdc.autoInit();


//// 2. Specify a file with the types you want to augment
////    Vue has the constructor type in types/vue.d.ts
//declare module 'vue/types/vue' {
//    // 3. Declare augmentation for Vue
//    interface Vue {
//        $myProperty: string
//    }
//}