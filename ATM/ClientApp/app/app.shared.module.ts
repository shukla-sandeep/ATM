import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { DataService } from './service/data-service';

@NgModule({
    declarations: [
        AppComponent,
        FetchDataComponent
    ],
    providers: [DataService],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'fetch-data', pathMatch: 'full' },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'fetch-data' }
        ])
    ]
})
export class AppModuleShared {
}
