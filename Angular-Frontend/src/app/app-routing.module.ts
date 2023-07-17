import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ErrorPageComponent } from './components/error-page/error-page.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { RegisterComponent } from './components/register/register.component';
import { CreatePropertyComponent } from './components/create-property/create-property.component';
import { PropertyDetailsComponent } from './components/property-details/property-details.component';
import { EditPropertyComponent } from './components/edit-property/edit-property.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { BackgroundComponent } from './components/background/background.component';
import { CarouselModalComponent } from './components/carousel-modal/carousel-modal.component';

const routes: Routes = [
  {path:'',component: BackgroundComponent},
  {path:'home',component: HomeComponent},
  {path:'register',component: RegisterComponent},
  {path:'newProperty',component: CreatePropertyComponent},
  {path:'propertyDetails/:id',component: PropertyDetailsComponent},
  {path:'editProperty/:id',component: EditPropertyComponent},
  {path:'forgotPassword',component: ForgotPasswordComponent},
  {path:'confirmEmail',component: ConfirmEmailComponent},
  {path:'aboutUs',component: AboutUsComponent},
  {path:'carousel',component: CarouselModalComponent},
  {path:'**',component: ErrorPageComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
