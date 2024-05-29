import { AccountConfigModule } from '@smartsoftware/ng.account/config';
import { CoreModule } from '@smartsoftware/ng.core';
import { IdentityConfigModule } from '@smartsoftware/ng.identity/config';
import { SettingManagementConfigModule } from '@smartsoftware/ng.setting-management/config';
import { TenantManagementConfigModule } from '@smartsoftware/ng.tenant-management/config';
import { ThemeBasicModule } from '@smartsoftware/ng.theme.basic';
import { ThemeSharedModule } from '@smartsoftware/ng.theme.shared';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CmsKitConfigModule } from '@my-company-name/cms-kit/config';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule.forRoot({
      environment,
      sendNullsAsQueryParam: false,
      skipGetAppConfiguration: false,
    }),
    ThemeSharedModule.forRoot(),
    AccountConfigModule.forRoot(),
    IdentityConfigModule.forRoot(),
    CmsKitConfigModule.forRoot(),
    TenantManagementConfigModule.forRoot(),
    SettingManagementConfigModule.forRoot(),
    ThemeBasicModule.forRoot(),
  ],
  providers: [APP_ROUTE_PROVIDER],
  declarations: [AppComponent],
  bootstrap: [AppComponent],
})
export class AppModule {}
