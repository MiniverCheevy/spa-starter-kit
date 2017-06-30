import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import * as Services from './../services';
import * as Api from './../api.generated';
import { AjaxServiceStatic } from '../services/ajax-service';
import { LayoutComponent } from './layout/layout.component';
import { NavmenuComponent } from './layout/navmenu.component';
import { HomeComponent } from './home/home.component';
import { AppComponent } from './app.component';
import { ToolbarIconLinkComponent } from './components/toolbar-icon-link.component';
import { ScratchComponent } from './scratch/scratch.component';
import { InputFieldComponent } from './components/forms/input-field.component';
import { SwitchComponent } from './components/forms/switch.component';
import { SorterComponent } from './components/data-tables/sorter.component';
import { GridComponent } from './components/data-tables/grid.component';





export class GlobalErrorHandler implements ErrorHandler {
    handleError(ex) {
        var error: any = {};
        var exc: any = ex;
        if (ex._nativeError != null)
            exc = ex._nativeError;

        error.errorMsg = exc.message;
        error.ErrorObject = exc.originalStack;
        AjaxServiceStatic.logError(exc, null, exc.originalStack);
        //(<any>window).jQuery.ajax({
        //    type: "POST",
        //    url: "api/clienterror",
        //    data: error
        //});
        console.log(ex);
        console.error(exc.originalStack);
    };
}

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        NavmenuComponent,
        HomeComponent,
        LayoutComponent,
        NavmenuComponent,
        ToolbarIconLinkComponent,
        AppComponent,
        InputFieldComponent,
        SwitchComponent,
        ScratchComponent,
        SorterComponent,
        GridComponent
    ],    
    providers: [
        { provide: 'ORIGIN_URL', useValue: location.origin },
        { provide: ErrorHandler, useClass: GlobalErrorHandler },

        Services.MessengerService,
        Services.CurrentUserService,
        Services.AjaxService,
        Api.providers
        
    ],
    exports: [InputFieldComponent],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            
            { path: 'home', component: HomeComponent },
            { path: 'scratch', component: ScratchComponent },
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: '**', redirectTo: 'home' }
        ], { useHash:true })
    ]
})
export class AppModule {
}
