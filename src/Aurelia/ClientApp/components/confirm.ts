import { autoinject, customElement } from "aurelia-framework";
import { MessengerService } from './../services/messenger-service';


@customElement('confirm')
@autoinject
export class Confirm
{
    public prompt: string;
    constructor(private messengerService: MessengerService) {

    }

    activate(data) {
        this.prompt = data;
    }
}