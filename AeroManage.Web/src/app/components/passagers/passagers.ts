import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { PassagerService } from '../../Services/passager.service';
import { Passager } from '../../models/passager.model';

@Component({
  selector: 'app-passagers',
  imports: [ReactiveFormsModule],
  templateUrl: './passagers.html',
  styleUrl: './passagers.css',
})
export class Passagers implements OnInit {
  private passagerService = inject(PassagerService);
  private fb = inject(FormBuilder);

  passagers = signal<Passager[]>([]);
  erreur = signal<string | null>(null);
  afficherFormulaire = signal(false);
  modeEdition = signal<Passager | null>(null);

  passagerForm = this.fb.group({
    nom: ['', Validators.required],
    prenom: ['', Validators.required],
    nationalite: ['', Validators.required],
  });

  ngOnInit(): void {
    this.chargerPassagers();
  }

  chargerPassagers(): void {
    this.passagerService.getAll().subscribe({
      next: (data) => this.passagers.set(data),
      error: () => this.erreur.set('Erreur de chargement des passagers.'),
    });
  }

  ouvrirFormulaire(): void {
    this.modeEdition.set(null);
    this.passagerForm.reset();
    this.afficherFormulaire.set(true);
  }

  ouvrirEdition(passager: Passager): void {
    this.modeEdition.set(passager);
    this.passagerForm.patchValue({ nom: passager.nom, prenom: passager.prenom, nationalite: passager.nationalite });
    this.afficherFormulaire.set(true);
  }

  fermerFormulaire(): void {
    this.afficherFormulaire.set(false);
    this.modeEdition.set(null);
  }

  soumettre(): void {
    if (this.passagerForm.invalid) return;
    const valeurs = this.passagerForm.value;
    const edition = this.modeEdition();

    if (edition) {
      this.passagerService.update(edition.idPassager, {
        nom: valeurs.nom!,
        prenom: valeurs.prenom!,
        nationalite: valeurs.nationalite!,
      }).subscribe({
        next: () => { this.chargerPassagers(); this.fermerFormulaire(); },
        error: () => this.erreur.set('Erreur lors de la modification.'),
      });
    } else {
      this.passagerService.create({
        nom: valeurs.nom!,
        prenom: valeurs.prenom!,
        nationalite: valeurs.nationalite!,
      }).subscribe({
        next: (nouveauPassager) => { this.passagers.update(liste => [...liste, nouveauPassager]); this.fermerFormulaire(); },
        error: () => this.erreur.set('Erreur lors de la création.'),
      });
    }
  }

  supprimer(id: number): void {
    if (!confirm('Voulez-vous vraiment supprimer ce passager ?')) return;
    this.passagerService.delete(id).subscribe({
      next: () => this.passagers.update(liste => liste.filter(p => p.idPassager !== id)),
      error: () => this.erreur.set('Erreur lors de la suppression.'),
    });
  }
}
