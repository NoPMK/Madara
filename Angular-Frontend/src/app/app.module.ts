import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ErrorPageComponent } from './components/error-page/error-page.component';
import { CreatePropertyComponent } from './components/create-property/create-property.component';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { PropertyDetailsComponent } from './components/property-details/property-details.component';
import { AuthenticationInterceptor } from './utils/authentication.interceptor';
import { SearchComponent } from './components/search/search.component';
import { EditPropertyComponent } from './components/edit-property/edit-property.component';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import { SharedModule } from './shared/shared.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BackgroundComponent } from './components/background/background.component';
import { NgToastModule } from 'ng-angular-popup';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { NotificationModule } from './components/toastr-notifications/notification.module';
import { CarouselModalComponent } from './components/carousel-modal/carousel-modal.component';






@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
    AboutUsComponent,
    ErrorPageComponent,
    CreatePropertyComponent,
    PropertyDetailsComponent,
    SearchComponent,
    PropertyDetailsComponent,
    EditPropertyComponent,
    ForgotPasswordComponent,
    ConfirmEmailComponent,
    BackgroundComponent,
    CarouselModalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NoopAnimationsModule,
    MatSliderModule,
    BrowserAnimationsModule,
    SharedModule,
    FormsModule,
    NgxPaginationModule,
    MatIconModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    NgToastModule,
    NotificationModule,
    LeafletModule,
  ],
  providers: [
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
