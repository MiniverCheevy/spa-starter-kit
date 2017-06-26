import * as React from 'react';

export const ToolBarIconLink = (route: string, icon: string, title: string) => {
        var url = "#/" + route;
        var iconName = "mdi mdi-" + icon;
        return <a href={url} className="material-icons mdc-toolbar__icon" aria-label={title} alt={title} title={title}><i className={iconName}></i></a>
}