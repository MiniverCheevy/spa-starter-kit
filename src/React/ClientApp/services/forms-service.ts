import { Models } from './../root';

class FormsServicePrototype {
    getProperties(metadata): Models.UIMetadata[] {
        let properties: Models.UIMetadata[] = [];
        if (!metadata)
            return properties;

        for (let key in metadata) {
            if (metadata.hasOwnProperty(key) && !metadata[key].isHidden)
            {
                properties.push(<Models.UIMetadata>metadata[key]);
            }
        }
        return properties;
    }
}

export const FormsService = new FormsServicePrototype();