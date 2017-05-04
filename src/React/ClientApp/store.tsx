import * as Models from './models.generated';
import * as Api from './api.generated';

export const GET_USERS = 'GET_USERS';
export const GET_USERS_ISOK = 'GET_USERS_SUCCESS';
export const GET_USERS_FAIL = 'GET_USERS_FAIL';

const INITIAL_STATE: Models.IUserQueryResponse = {};

export class Action {
    public type: string;
    public result: Models.IResponse | Promise<Models.IResponse>;
    public isLoading: boolean;
}

//action
export function getUsers(request: Models.IUserQueryRequest) {
    return {
        type: GET_USERS,
        result: Api.UserList.get(request),
        isLoading: true
    };
}
export function getUsersIsOk(response: Models.IUserQueryResponse)
{
    return {
        type: GET_USERS_ISOK,
        result: response,
        isLoading: false
    };
}
export function getUsersFail(response: Models.IUserQueryResponse) {
    return {
        type: GET_USERS_FAIL,
        result: response,
        isLoading: false
    };
}

export function reducer(state = INITIAL_STATE, action: Action)
{
    switch (action.type)
    {
        case GET_USERS:
            return Object.assign(state, action.result);

        default:
            return state;
    }
}

