import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CarouselModule, WavesModule, InputsModule, ButtonsModule } from 'angular-bootstrap-md'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CarouselComponent } from './carousel/carousel.component';
import { TextAnalyserComponent } from './text-analyser/text-analyser.component';
import { GalleryComponent } from './gallery/gallery.component';
import { SpeakCorrectlyComponent } from './speak-correctly/speak-correctly.component';
import { FooterComponent } from './footer/footer.component';
import { CardComponent } from './card/card.component';

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
    CardComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ButtonsModule,
    CarouselModule.forRoot(),
    WavesModule.forRoot(),
    InputsModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'text-analyser', component: TextAnalyserComponent },
      { path: 'gallery', component: GalleryComponent },
      { path: 'speak-correctly', component: SpeakCorrectlyComponent },
      { path: 'card', component: CardComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
