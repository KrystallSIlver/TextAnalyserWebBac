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
    new Image (0,{ img: 'https://i.ibb.co/p1jBYd6/quote.png'}),
    new Image (1,{ img: 'https://i.ibb.co/0fY4Tqh/quote-1.png'}),
    new Image (2,{ img: 'https://i.ibb.co/N3fqGTM/quote-2.png'}),
    new Image (3,{ img: 'https://i.ibb.co/cLkWQqp/quote-3.png'}),
    new Image (4,{ img: 'https://i.ibb.co/PMPLHL0/quote-4.png'}),
    new Image (5,{ img: 'https://i.ibb.co/4W0xnTC/quote-5.png'}),
    new Image (6, {img: 'https://i.ibb.co/jMGzJLc/quote-6.png'}),
    new Image (7, {img: 'https://i.ibb.co/rHHbt0n/quote-7.png'}),
    new Image (8, {img: 'https://i.ibb.co/Mhzc5ZK/quote-8.png'}),
    new Image (9, {img: 'https://i.ibb.co/TbyjqhS/quote-9.png'}),
    new Image (10, {img: 'https://i.ibb.co/PxbMV7m/quote-10.png'}),
    new Image (11, {img: 'https://i.ibb.co/k3qWN0L/quote-11.png'}),
    new Image (12, {img: 'https://i.ibb.co/LRHm57C/quote-12.png'}),
    new Image (13, {img: 'https://i.ibb.co/XzsXKGK/quote-13.png'}),
    new Image (14, {img: 'https://i.ibb.co/0txjLt0/quote-14.png'}),
    new Image (15, {img: 'https://i.ibb.co/Tg83xfD/quote-15.png'}),
    new Image (16, {img: 'https://i.ibb.co/XJ15b7j/quote-16.png'}),
    new Image (17, {img: 'https://i.ibb.co/dL3NkgC/quote-17.png'}),
    new Image (18, {img: 'https://i.ibb.co/WPN31rW/quote-18.png'}),
    new Image (19, {img: 'https://i.ibb.co/pdCSLNs/quote-19.png'}),
    new Image (20, {img: 'https://i.ibb.co/cNKvv34/quote-20.png'}),
    new Image (21, {img: 'https://i.ibb.co/HYYtRjj/quote-21.png'}),
    new Image (22, {img: 'https://i.ibb.co/vz9g9cD/quote-22.png'}),
    new Image (23, {img: 'https://i.ibb.co/M1T6ht6/quote-23.png'}),
    new Image (24, {img: 'https://i.ibb.co/QFV4GW9/quote-24.png'}),
    new Image (25, {img: 'https://i.ibb.co/Q6VLWZZ/quote-25.png'}),
    new Image (26, {img: 'https://i.ibb.co/mtxz6wd/quote-26.png'}),
    new Image (27, {img: 'https://i.ibb.co/qrSmGMZ/quote-27.png'}),
    new Image (28, {img: 'https://i.ibb.co/K5VS4r0/quote-28.png'}),
    new Image (29, {img: 'https://i.ibb.co/swS6SMW/quote-29.png'}),
    new Image (30, {img: 'https://i.ibb.co/T8jpc93/quote-30.png'}),
    new Image (31, {img: 'https://i.ibb.co/m4TrZg8/quote-31.png'}),
    new Image (32, {img: 'https://i.ibb.co/zQQdYtb/quote-32.png'}),
    new Image (33, {img: 'https://i.ibb.co/djVmVYW/quote-33.png'}),
    new Image (34, {img: 'https://i.ibb.co/h8n6VX5/quote-34.png'}),
    new Image (35, {img: 'https://i.ibb.co/NZgSmw5/quote-35.png'}),
    new Image (36, {img: 'https://i.ibb.co/Y3vGr1J/quote-36.png'}),
    new Image (37, {img: 'https://i.ibb.co/jHShsw1/quote-37.png'}),
    new Image (38, {img: 'https://i.ibb.co/yQd2X6Q/quote-38.png'}),
    new Image (39, {img: 'https://i.ibb.co/JsGxSrG/quote-39.png'}),
    new Image (40, {img: 'https://i.ibb.co/44WZ97H/quote-40.png'}),
    new Image (41, {img: 'https://i.ibb.co/6yY6RHR/quote-41.png'}),
    new Image (42, {img: 'https://i.ibb.co/sbLvWFr/quote-42.png'}),
    new Image (43, {img: 'https://i.ibb.co/0rfy7Cv/quote-43.png'}),
    new Image (44, {img: 'https://i.ibb.co/6txwjyz/quote-44.png'}),
    new Image (45, {img: 'https://i.ibb.co/f0q8fdc/quote-45.png'}),
    new Image (46, {img: 'https://i.ibb.co/HqxMgYn/quote-46.png'}),
    new Image (47, {img: 'https://i.ibb.co/3ypC7Dw/quote-47.png'}),
    new Image (48, {img: 'https://i.ibb.co/rmx7wvb/quote-48.png'}),
    new Image (49, {img: 'https://i.ibb.co/bJMGZ9S/quote-49.png'}),
    new Image (50, {img: 'https://i.ibb.co/4SBwd0K/quote-50.png'}),
    new Image (51, {img: 'https://i.ibb.co/CMWwwSf/quote-51.png'}),
    new Image (52, {img: 'https://i.ibb.co/jHK3bkx/quote-52.png'}),
    new Image (53, {img: 'https://i.ibb.co/S5P3GmG/quote-53.png'}),
    new Image (54, {img: 'https://i.ibb.co/SwCgBdk/quote-54.png'}),
    new Image (55, {img: 'https://i.ibb.co/Y3fkR6c/quote-55.png'})
  ]

}
