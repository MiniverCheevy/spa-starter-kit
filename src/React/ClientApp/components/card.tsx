import * as React from 'react';
import { Components } from './../root';

export class CardProps {
    title?: string;
    subTitle?: string;
    buttons?: Components.PushButton[];
}
export class Card extends React.Component<CardProps, any>
{
    constructor(props: CardProps) {
        super(props);
    }
    render() {

        let hasHeader = this.props.title || this.props.subTitle;
        let hasButtons = this.props.buttons && this.props.buttons.length > 0;

        return <div className="mdc-card">
            {hasHeader && <section className="mdc-card__primary">
                {this.props.title && <h1 className="mdc-card__title mdc-card__title--large">{this.props.title}</h1>}
                {this.props.subTitle && <h2 className="mdc-card__subtitle">{this.props.subTitle}</h2>}
            </section>}
            <section className="mdc-card__supporting-text">
                {this.props.children}
            </section>
            {hasButtons && <section className="mdc-card__actions">
                {this.props.buttons}
            </section>}
        </div>
    }
}