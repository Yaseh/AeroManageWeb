import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { ReservationService } from '../../Services/reservation.service';
import { Reservation } from '../../models/reservation.model';
import { VolService } from '../../Services/vol.service';
import { Vol } from '../../models/vol.model';
import { PassagerService } from '../../Services/passager.service';
import { Passager } from '../../models/passager.model';

@Component({
  selector: 'app-reservations',
  imports: [ReactiveFormsModule],
  templateUrl: './reservations.html',
  styleUrl: './reservations.css',
})
export class Reservations implements OnInit {
  private reservationService = inject(ReservationService);
  private fb = inject(FormBuilder);
  private volService = inject(VolService);
  private passagerService = inject(PassagerService);

  reservations = signal<Reservation[]>([]);
  erreur = signal<string | null>(null);
  afficherFormulaire = signal(false);
  modeEdition = signal<Reservation | null>(null);
  volsDisponibles = signal<Vol[]>([]);
  passagersDisponibles = signal<Passager[]>([]);

  reservationForm = this.fb.group({
    numerosiege: ['', Validators.required],
    idVol: [null as number | null, Validators.required],
    idPassager: [null as number | null, Validators.required],
  });

  ngOnInit(): void {
    this.chargerReservations();
    this.chargerListesReferences();
  }


  chargerListesReferences(): void {
  this.volService.getAll().subscribe(data => this.volsDisponibles.set(data));
  this.passagerService.getAll().subscribe(data => this.passagersDisponibles.set(data));
}

  chargerReservations(): void {
    this.reservationService.getAll().subscribe({
      next: (data) => this.reservations.set(data),
      error: () => this.erreur.set('Erreur de chargement des réservations.'),
    });
  }

  ouvrirFormulaire(): void {
    this.modeEdition.set(null);
    this.reservationForm.reset();
    this.afficherFormulaire.set(true);
  }

  ouvrirEdition(reservation: Reservation): void {
    this.modeEdition.set(reservation);
    this.reservationForm.patchValue({
      numerosiege: reservation.numerosiege,
      idVol: reservation.idVol,
      idPassager: reservation.idPassager,
    });
    this.afficherFormulaire.set(true);
  }

  fermerFormulaire(): void {
    this.afficherFormulaire.set(false);
    this.modeEdition.set(null);
  }

  soumettre(): void {
    if (this.reservationForm.invalid) return;
    const valeurs = this.reservationForm.value;
    const edition = this.modeEdition();

    if (edition) {
      this.reservationService.update(edition.idReservation, {
        numerosiege: valeurs.numerosiege!,
        idVol: valeurs.idVol!,
        idPassager: valeurs.idPassager!,
      }).subscribe({
        next: () => { this.chargerReservations(); this.fermerFormulaire(); },
        error: () => this.erreur.set('Erreur lors de la modification.'),
      });
    } else {
      this.reservationService.create({
        numerosiege: valeurs.numerosiege!,
        idVol: valeurs.idVol!,
        idPassager: valeurs.idPassager!,
      }).subscribe({
        next: (nouvelleReservation) => { this.reservations.update(liste => [...liste, nouvelleReservation]); this.fermerFormulaire(); },
        error: () => this.erreur.set('Erreur lors de la création.'),
      });
    }
  }

  supprimer(id: number): void {
    if (!confirm('Voulez-vous vraiment supprimer cette réservation ?')) return;
    this.reservationService.delete(id).subscribe({
      next: () => this.reservations.update(liste => liste.filter(r => r.idReservation !== id)),
      error: () => this.erreur.set('Erreur lors de la suppression.'),
    });
  }
}
