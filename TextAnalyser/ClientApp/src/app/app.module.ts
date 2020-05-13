import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CarouselModule, WavesModule, InputsModule, ButtonsModule } from 'angular-bootstrap-md'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { TooltipModule } from 'ng2-tooltip-directive';
import { HighchartsChartModule, HighchartsChartComponent } from 'highcharts-angular';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CarouselComponent } from './components/carousel/carousel.component';
import { TextAnalyserComponent } from './text-analyser/text-analyser.component';
import { GalleryComponent } from './gallery/gallery.component';
import { SpeakCorrectlyComponent } from './speak-correctly/speak-correctly.component';
import { FooterComponent } from './components/footer/footer.component';
import { HelpComponent } from './help/help.component';
import { AntysurzhykComponent } from './cards/antysurzhyk/antysurzhyk.component';
import { ForChildrenComponent } from './cards/for-children/for-children.component';
import { AccentComponent } from './cards/accent/accent.component';
import { OrthographyComponent } from './cards/orthography/orthography.component';
import { ParonymsComponent } from './cards/paronyms/paronyms.component';
import { SynonymsComponent } from './cards/synonyms/synonyms.component';
import { WordUsageComponent } from './cards/word-usage/word-usage.component';
import { PhraseologismsComponent } from './cards/phraseologisms/phraseologisms.component';
import { GoToTopButtonComponent } from './components/go-to-top-button/go-to-top-button.component';
import { OrthographyTabComponent } from './text-analyser/tabs/orthography-tab/orthography-tab.component';
import { SeoTabComponent } from './text-analyser/tabs/seo-tab/seo-tab.component';
import { ReadTabComponent } from './text-analyser/tabs/read-tab/read-tab.component';
import { MapTabComponent } from './text-analyser/tabs/map-tab/map-tab.component';

import 'hammerjs';
import 'mousetrap';
import { GalleryModule } from '@ks89/angular-modal-gallery';
import { BackButtonComponent } from './components/back-button/back-button.component';
import { LanguageToolService } from './Services/language-tool.service';
import { SeoService } from './Services/seo.service';
import { ZipfLawTabComponent } from './text-analyser/tabs/zipf-law-tab/zipf-law-tab.component';
import { WordCategoriesChartComponent } from './text-analyser/charts/word-categories-chart/word-categories-chart.component';
import { WaterChartComponent } from './text-analyser/charts/water-chart/water-chart.component';
import { WordChartComponent } from './text-analyser/charts/word-chart/word-chart.component';
import { UnicWordComponent } from './text-analyser/charts/unic-word/unic-word.component';
import { SignWordComponent } from './text-analyser/charts/sign-word/sign-word.component';
import { StopWordComponent } from './text-analyser/charts/stop-word/stop-word.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CarouselComponent,
    TextAnalyserComponent,
    GalleryComponent,
    SpeakCorrectlyComponent,
    FooterComponent,
    HelpComponent,
    AntysurzhykComponent,
    ForChildrenComponent,
    AccentComponent,
    OrthographyComponent,
    ParonymsComponent,
    SynonymsComponent,
    WordUsageComponent,
    PhraseologismsComponent,
    GoToTopButtonComponent,
    OrthographyTabComponent,
    SeoTabComponent,
    ReadTabComponent,
    MapTabComponent,
    BackButtonComponent,
    HighchartsChartComponent,
    ZipfLawTabComponent,
    WordCategoriesChartComponent,
    WaterChartComponent,
    WordChartComponent,
    UnicWordComponent,
    SignWordComponent,
    StopWordComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    TooltipModule,
    ButtonsModule,
    //HighchartsChartModule,
    CarouselModule.forRoot(),
    WavesModule.forRoot(),
    InputsModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'text-analyser', component: TextAnalyserComponent },
      { path: 'gallery', component: GalleryComponent },
      { path: 'speak-correctly', component: SpeakCorrectlyComponent },
      { path: 'speak-correctly/antysurzhyk', component: AntysurzhykComponent },
      { path: 'speak-correctly/for-children', component: ForChildrenComponent },
      { path: 'speak-correctly/accent', component: AccentComponent },
      { path: 'speak-correctly/orthography', component: OrthographyComponent },
      { path: 'speak-correctly/paronyms', component: ParonymsComponent },
      { path: 'speak-correctly/synonyms', component: SynonymsComponent },
      { path: 'speak-correctly/word-usage', component: WordUsageComponent },
      { path: 'speak-correctly/phraseologisms', component: PhraseologismsComponent },
      { path: 'help', component: HelpComponent },
    ]),
    GalleryModule.forRoot()
  ],
  providers: [LanguageToolService, SeoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
