import { Models, Services, Component, Vue } from './../root';

@Component({
    components: {
        NavmenuComponent: require('../navmenu.vue.html')
    }
})
export class LayoutComponent extends Vue {
    private user: Models.AppPrincipal;

    beforeMount=async()=>
    {
        this.user = await Services.CurrentUserService.get();
    }
}