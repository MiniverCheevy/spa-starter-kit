import { Api, Models } from './../root';

class ListServicePrototype
    implements Models.ListsResponse {
    sites?: Models.IListItem[] = [];
    roles?: Models.IListItem[] = [];
    lists?: Models.IListItem[] = [];
    dayTypes?: Models.IListItem[] = [];
    noteTypes?: Models.IListItem[] = [];
    sqlOperations?: Models.IListItem[] = [];
    applicationSettings?: Models.IListItem[] = [];
    equipments?: Models.IListItem[] = [];

    personnels?: Models.IListItem[] = [];
    signers?: Models.IListItem[] = [];
    status?: Models.IListItem[] = [];
    constructor() {

    }

    //TODO: don't fetch the enums if they're already here

    public get = async (request: Models.ListsRequest) => {
        return Api.Lists.get(request).then(this.updateLists);
    }
    private updateLists = (response: Models.ListsResponse) => {
        for (var list in response) {
            var source = response[list];
            if (source != undefined && Array.isArray(source) && source.length > 0)
                this[list] = source;
        }
    }
}

export const ListService = new ListServicePrototype();


