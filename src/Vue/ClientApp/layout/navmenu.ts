import { Models, Services, Component, Vue } from './../root';

@Component
export class NavmenuComponent extends Vue {
    private user: Models.AppPrincipal;

    beforeMount = async () => {
        this.user = await Services.CurrentUserService.get();
    }
}