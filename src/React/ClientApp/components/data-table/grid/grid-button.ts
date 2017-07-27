export class GridButton {
    constructor(public text: string, public icon: string, public action: (any) => void)
    {

    }
    //text?: string;
    //icon?: string;
    //action: (any) => void;
    get key() {
        var key = '_';
        if (this.text != null)
            key = key + this.text + '_';
        if (this.icon != null)
            key = key + this.icon + '_';
        return key;

    }
}
