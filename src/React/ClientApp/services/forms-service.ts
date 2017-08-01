import { Models } from './../root';

class FormsServicePrototype {
    getValueAfterLastSlash(location) {
        //var search = location.search.toString(); //this is empty for /some-page/1
        //TODO: will URLSearchParams handle this? pollyfill for IE?

        if (location != null && location.pathname) {
            var array = location.pathname.split('/');
            var index = array.length - 1;
            return array[index];
        }
        return null;

    }
    getProperties(metadata): Models.UIMetadata[] {
        let properties: Models.UIMetadata[] = [];
        if (!metadata)
            return properties;

        for (let key in metadata) {
            if (metadata.hasOwnProperty(key) && !metadata[key].isHidden) {
                properties.push(<Models.UIMetadata>metadata[key]);
            }
        }
        return properties;
    }
}

export const FormsService = new FormsServicePrototype();