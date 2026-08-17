import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { VolService } from '../../Services/vol.service';
import { Vol } from '../../models/vol.model';
import { AvionService } from '../../Services/avion.service';
import { PersonnelService } from '../../Services/personnel.service';
import { AeroportService } from '../../Services/aeroport.service';
import { Aeroport } from '../../models/aeroport.model';
import { Avion } from '../../models/avion.models';
import { Personnel } from '../../models/personnel.model';
@Component({
  selector: 'app-vols',
  imports: [ReactiveFormsModule],
  templateUrl: './vols.html',
  styleUrl: './vols.css',
})
export class Vols implements OnInit {
  private volService = inject(VolService);
  private fb = inject(FormBuilder);
  private avionService = inject(AvionService);
  private personnelService = inject(PersonnelService);
  private aeroportService = inject(AeroportService);

   vols = signal<Vol[]>([]);
  avionsDisponibles = signal<Avion[]>([]);
  commandantsDisponibles = signal<Personnel[]>([]);
  aeroportsDisponibles = signal<Aeroport[]>([]);
  erreur = signal<string | null>(null);
  afficherFormulaire = signal(false);
  modeEdition = signal<Vol | null>(null);

  volForm = this.fb.group({
    numeroVol: ['', Validators.required],
    dateDepart: ['', Validators.required],
    dateArrivee: ['', Validators.required],
    statut: ['Prévu', Validators.required],
    aeroportDepart: ['', Validators.required],
    aeroportArrivee: ['', Validators.required],
    idAvion: [null as number | null, Validators.required],
    idCommandant: [null as number | null, Validators.required],
  });

  ngOnInit(): void {
    this.chargerVols();
    this.chargerListesReferences();
  }

  chargerListesReferences(): void {
    this.avionService.getAll().subscribe(data => this.avionsDisponibles.set(data));
    this.personnelService.getAll().subscribe(data =>
      this.commandantsDisponibles.set(data.filter(p => p.role === 'Commandant'))
    );
    this.aeroportService.getAll().subscribe(data => this.aeroportsDisponibles.set(data));
  }
  
  chargerVols(): void {
    this.volService.getAll().subscribe({
      next: (data) => this.vols.set(data),
      error: () => this.erreur.set('Erreur de chargement des vols.'),
    });
  }

  ouvrirFormulaire(): void {
    this.modeEdition.set(null);
    this.volForm.reset({ statut: 'Prévu' });
    this.afficherFormulaire.set(true);
  }

  ouvrirEdition(vol: Vol): void {
    this.modeEdition.set(vol);
    this.volForm.patchValue({
      numeroVol: vol.numeroVol,
      dateDepart: vol.dateDepart,
      dateArrivee: vol.dateArrivee,
      statut: vol.statut,
      aeroportDepart: vol.aeroportDepart,
      aeroportArrivee: vol.aeroportArrivee,
      idAvion: vol.idAvion,
      idCommandant: vol.idCommandant,
    });
    this.afficherFormulaire.set(true);
  }

  fermerFormulaire(): void {
    this.afficherFormulaire.set(false);
    this.modeEdition.set(null);
  }

  soumettre(): void {
    if (this.volForm.invalid) return;
    const valeurs = this.volForm.value;
    const edition = this.modeEdition();

    if (edition) {
      this.volService.update(edition.idVol, {
        numeroVol: valeurs.numeroVol!,
        dateDepart: valeurs.dateDepart!,
        dateArrivee: valeurs.dateArrivee!,
        statut: valeurs.statut!,
        aeroportDepart: valeurs.aeroportDepart!,
        aeroportArrivee: valeurs.aeroportArrivee!,
        idAvion: valeurs.idAvion!,
        idCommandant: valeurs.idCommandant!,
      }).subscribe({
        next: () => { this.chargerVols(); this.fermerFormulaire(); },
        error: () => this.erreur.set('Erreur lors de la modification.'),
      });
    } else {
      this.volService.create({
        numeroVol: valeurs.numeroVol!,
        dateDepart: valeurs.dateDepart!,
        dateArrivee: valeurs.dateArrivee!,
        statut: valeurs.statut!,
        aeroportDepart: valeurs.aeroportDepart!,
        aeroportArrivee: valeurs.aeroportArrivee!,
        idAvion: valeurs.idAvion!,
        idCommandant: valeurs.idCommandant!,
      }).subscribe({
        next: (nouveauVol) => { this.vols.update(liste => [...liste, nouveauVol]); this.fermerFormulaire(); },
        error: () => this.erreur.set('Erreur lors de la création.'),
      });
    }
  }

  supprimer(id: number): void {
    if (!confirm('Voulez-vous vraiment supprimer ce vol ?')) return;
    this.volService.delete(id).subscribe({
      next: () => this.vols.update(liste => liste.filter(v => v.idVol !== id)),
      error: () => this.erreur.set('Erreur lors de la suppression.'),
    });
  }
}
