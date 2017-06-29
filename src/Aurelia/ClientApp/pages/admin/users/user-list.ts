import * as Api from './../../../api.generated';
import * as Models from './../../../models.generated';

export class UserList
{
    request: Models.IUserListRequest = {};
    data: Models.IUserRow[] = [];

    public constructor(private userListService: Api.UserList)
    {

        this.refresh();    
    }
    public async refresh()
    {
        debugger;
        var response = await this.userListService.get(this.request);
        if (response.isOk)
            this.data = response.data;
    }
}
