import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AeroportService } from '../../Services/aeroport.service';
import { Aeroport } from '../../models/aeroport.model';

@Component({
  selector: 'app-aeroports',
  imports: [ReactiveFormsModule],
  templateUrl: './aeroports.html',
  styleUrl: './aeroports.css',
})
export class Aeroports implements OnInit {
  private aeroportService = inject(AeroportService);
  private fb = inject(FormBuilder);

  aeroports = signal<Aeroport[]>([]);
  erreur = signal<string | null>(null);
  afficherFormulaire = signal(false);
  modeEdition = signal<Aeroport | null>(null);

  aeroportForm = this.fb.group({
    idIata: ['', Validators.required],
    nom: ['', Validators.required],
    ville: ['', Validators.required],
    pays: ['', Validators.required],
  });

  ngOnInit(): void {
    this.chargerAeroports();
  }

  chargerAeroports(): void {
    this.aeroportService.getAll().subscribe({
      next: (data) => this.aeroports.set(data),
      error: () => this.erreur.set('Erreur de chargement des aéroports.'),
    });
  }

  ouvrirFormulaire(): void {
    this.modeEdition.set(null);
    this.aeroportForm.reset();
    this.aeroportForm.get('idIata')?.enable();
    this.afficherFormulaire.set(true);
  }

  ouvrirEdition(aeroport: Aeroport): void {
    this.modeEdition.set(aeroport);
    this.aeroportForm.patchValue({ idIata: aeroport.idIata, nom: aeroport.nom, ville: aeroport.ville, pays: aeroport.pays });
    this.aeroportForm.get('idIata')?.disable();
    this.afficherFormulaire.set(true);
  }

  fermerFormulaire(): void {
    this.afficherFormulaire.set(false);
    this.modeEdition.set(null);
    this.aeroportForm.get('idIata')?.enable();
  }

  soumettre(): void {
    if (this.aeroportForm.invalid) return;
    const valeurs = this.aeroportForm.getRawValue();
    const edition = this.modeEdition();

    if (edition) {
      this.aeroportService.update(edition.idIata, {
        idIata: edition.idIata,
        nom: valeurs.nom!,
        ville: valeurs.ville!,
        pays: valeurs.pays!,
      }).subscribe({
        next: () => { this.chargerAeroports(); this.fermerFormulaire(); },
        error: () => this.erreur.set('Erreur lors de la modification.'),
      });
    } else {
      this.aeroportService.create({
        idIata: valeurs.idIata!,
        nom: valeurs.nom!,
        ville: valeurs.ville!,
        pays: valeurs.pays!,
      }).subscribe({
        next: (nouvelAeroport) => { this.aeroports.update(liste => [...liste, nouvelAeroport]); this.fermerFormulaire(); },
        error: () => this.erreur.set('Erreur lors de la création.'),
      });
    }
  }

  supprimer(id: string): void {
    if (!confirm('Voulez-vous vraiment supprimer cet aéroport ?')) return;
    this.aeroportService.delete(id).subscribe({
      next: () => this.aeroports.update(liste => liste.filter(a => a.idIata !== id)),
      error: () => this.erreur.set('Erreur lors de la suppression.'),
    });
  }
}
