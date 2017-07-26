import Vue from 'vue';
import { Component } from 'vue-property-decorator';
@Component({
    components: {
        LayoutComponent: require('./layout/layout.vue.html')
    }
})
export class AppComponent extends Vue {

    beforeCreate() { }
    created() { }
    beforeMount() { }
    mounted() { }
    beforeUpdate() { }
    updated() { }
    beforeDestroy() { }
    destroyed() { }
}
