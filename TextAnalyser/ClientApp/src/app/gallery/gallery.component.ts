import { Component, OnInit } from '@angular/core';
import {
  AccessibilityConfig,
  Action,
  ButtonEvent,
  ButtonsConfig,
  ButtonsStrategy,
  ButtonType,
  Description,
  DescriptionStrategy,
  DotsConfig,
  GalleryService,
  Image,
  ImageModalEvent,
  KS_DEFAULT_BTN_CLOSE,
  KS_DEFAULT_BTN_DELETE,
  KS_DEFAULT_BTN_DOWNLOAD,
  KS_DEFAULT_BTN_EXTURL,
  KS_DEFAULT_BTN_FULL_SCREEN,
  PreviewConfig,
  LoadingConfig,
  LoadingType,
  CurrentImageConfig,
  PlainGalleryConfig,
  PlainGalleryStrategy,
  GridLayout
} from '@ks89/angular-modal-gallery';

@Component({
  selector: 'gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  
  plainGalleryGrid: PlainGalleryConfig = {
    strategy: PlainGalleryStrategy.GRID,
    layout: new GridLayout({ width: '30%', height: '50%' }, { length: 4, wrap: true })
  };

  images: Image[] = [
    new Image (0,{ img: 'https://i.ibb.co/p1jBYd6/quote.png' }),
    new Image (1,{ img: 'https://i.ibb.co/0fY4Tqh/quote-1.png' }),
    new Image (2,{ img: 'https://i.ibb.co/N3fqGTM/quote-2.png' }),
    new Image (3,{ img: 'https://i.ibb.co/PMPLHL0/quote-4.png' }),
    new Image (4,{ img: 'https://i.ibb.co/4W0xnTC/quote-5.png' }),
    new Image (5,{ img: 'https://i.ibb.co/jMGzJLc/quote-6.png' }),
  ]

}
