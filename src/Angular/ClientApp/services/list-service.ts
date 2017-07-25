import { Api, Models } from './../app/root';
import { Injectable } from '@angular/core';
@Injectable()
export class ListService
    implements Models.ListsResponse
{
    sites?: Models.IListItem[]=[];
    roles?: Models.IListItem[]=[];
    lists?: Models.IListItem[]=[];
    dayTypes?: Models.IListItem[]=[];
    noteTypes?: Models.IListItem[]=[];
    sqlOperations?: Models.IListItem[]=[];
    applicationSettings?: Models.IListItem[]=[];
    equipments?: Models.IListItem[] = [];

    personnels?: Models.IListItem[] = [];
    signers?: Models.IListItem[] = [];
    status?: Models.IListItem[] = [];
    constructor(private api: Api.Lists) {
        
    }

    //TODO: don't fetch the enums if they're already here

    public get = async (request: Models.ListsRequest) => {
        return this.api.get(request).then(this.updateLists);
    }
    private updateLists=(response: Models.ListsResponse)=>
    {
        for (var list in response)
        {
            var source = response[list];
            if (source != undefined && Array.isArray(source) && source.length > 0)
                this[list] = source;
        }
    }
}




