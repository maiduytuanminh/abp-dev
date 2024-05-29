import { AccountConfigModule } from '@smartsoftware/ng.account/config';
import { CoreModule } from '@smartsoftware/ng.core';
import { registerLocale } from '@smartsoftware/ng.core/locale';
import { IdentityConfigModule } from '@smartsoftware/ng.identity/config';
import { SettingManagementConfigModule } from '@smartsoftware/ng.setting-management/config';
import { TenantManagementConfigModule } from '@smartsoftware/ng.tenant-management/config';
import { ThemeLeptonXModule } from '@smartsoftware/ng.theme.lepton-x';
import { SideMenuLayoutModule } from '@smartsoftware/ng.theme.lepton-x/layouts';
import { ThemeSharedModule } from '@smartsoftware/ng.theme.shared';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { FeatureManagementModule } from '@smartsoftware/ng.feature-management';
import { SmartSoftwareOAuthModule } from '@smartsoftware/ng.oauth';

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
    }),
    SmartSoftwareOAuthModule.forRoot(),
    ThemeSharedModule.forRoot(),
    AccountConfigModule.forRoot(),
    IdentityConfigModule.forRoot(),
    TenantManagementConfigModule.forRoot(),
    SettingManagementConfigModule.forRoot(),
    ThemeLeptonXModule.forRoot(),
    SideMenuLayoutModule.forRoot(),
    FeatureManagementModule.forRoot(),
  ],
  declarations: [AppComponent],
  providers: [APP_ROUTE_PROVIDER],
  bootstrap: [AppComponent],
})
export class AppModule {}
