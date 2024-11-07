import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { TransactionComponent } from './components/transaction/transaction.component';
import { LookupTransactionsComponent } from './components/lookup-transactions/lookup-transactions.component';
import { ConfigService } from './config.service';

export function initializeApp(configService: ConfigService)
{
  return () => configService.loadConfig().toPromise();
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SidebarComponent,
    TransactionComponent,
    LookupTransactionsComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'transaction', component: TransactionComponent },
      { path: 'lookup-transactions', component: LookupTransactionsComponent },
    ]),
    BrowserAnimationsModule
  ],
  providers: [
    ConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [ConfigService],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
