import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@smartsoftware/ng.core';
import { ThemeSharedModule } from '@smartsoftware/ng.theme.shared';
import { CmsKitComponent } from './components/cms-kit.component';
import { CmsKitRoutingModule } from './cms-kit-routing.module';

@NgModule({
  declarations: [CmsKitComponent],
  imports: [CoreModule, ThemeSharedModule, CmsKitRoutingModule],
  exports: [CmsKitComponent],
})
export class CmsKitModule {
  static forChild(): ModuleWithProviders<CmsKitModule> {
    return {
      ngModule: CmsKitModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<CmsKitModule> {
    return new LazyModuleFactory(CmsKitModule.forChild());
  }
}
