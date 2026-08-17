import { Routes } from '@angular/router';
import { Avions } from './components/avions/avions';
import { Aeroports } from './components/aeroports/aeroports';
import { PersonnelComponent } from './components/personnel/personnel';
import { Passagers } from './components/passagers/passagers';
import { Vols } from './components/vols/vols';
import { Reservations } from './components/reservations/reservations';

export const routes: Routes = [
  { path: '', redirectTo: 'avions', pathMatch: 'full' },
  { path: 'avions', component: Avions },
  { path: 'aeroports', component: Aeroports },
  { path: 'personnel', component: PersonnelComponent },
  { path: 'passagers', component: Passagers },
  { path: 'vols', component: Vols },
  { path: 'reservations', component: Reservations },
];
