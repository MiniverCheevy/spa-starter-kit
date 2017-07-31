import * as Models from './../models.generated';

class GridServicePrototype {
    getRequest = (key: string, defaultValue) => {
        if (this.hasOwnProperty(key))
            return this[key];
        else {
            this[key] = defaultValue;
            return defaultValue;
        }
    }
    setRequest = (key: string, request) => {
        this[key] = request;
    }
}

export const GridService = new GridServicePrototype();