import * as React from 'react';
import { Models, Services, Components } from './../../../root';
import { RowState, EditRowMode } from './edit-row'

export class RowStateFactory {
    constructor(private metadataCallback: () => { }) {

    }

    getRowStateFromData(data: any): RowState {

        return this.getRowStatesFromData([data])[0];
    }

    getRowStatesFromData(data: any[]): RowState[] {
        return data.map((row, index) => {
            return { mode: EditRowMode.View, model: row, form: new Components.Form(this.metadataCallback()), index: index };
        });

    }

}