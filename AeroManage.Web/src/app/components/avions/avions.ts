import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AvionService } from '../../Services/avion.service';
import { Avion } from '../../models/avion.models';

@Component({
  selector: 'app-avions',
  imports: [ReactiveFormsModule],
  templateUrl: './avions.html',
  styleUrl: './avions.css',
})
export class Avions implements OnInit {
  private avionService = inject(AvionService);
  private fb = inject(FormBuilder);

  avions = signal<Avion[]>([]);
  erreur = signal<string | null>(null);
  afficherFormulaire = signal(false);
  modeEdition = signal<Avion | null>(null);

  avionForm = this.fb.group({
    model: ['', Validators.required],
    capacite: [null as number | null, [Validators.required, Validators.min(1)]],
    statut: ['En service', Validators.required],
  });

  ngOnInit(): void {
    this.chargerAvion();
  }

  chargerAvion(): void {
    this.avionService.getAll().subscribe({
      next: (data) => this.avions.set(data),
      error: (err) => this.erreur.set('Erreur de chargement des avions.'),
    });
  }

  ouvrirFormulaire(): void {
    this.modeEdition.set(null);
    this.avionForm.reset({ statut: 'En service' });
    this.afficherFormulaire.set(true);
  }

  ouvrirEdition(avion: Avion): void {
    this.modeEdition.set(avion);
    this.avionForm.patchValue({ model: avion.model, capacite: avion.capacite, statut: avion.statut });
    this.afficherFormulaire.set(true);
  }

  fermerFormulaire(): void {
    this.afficherFormulaire.set(false);
    this.modeEdition.set(null);
  }

  soumettre(): void {
    if (this.avionForm.invalid) return;
    const valeurs = this.avionForm.value;
    const edition = this.modeEdition();

    if (edition) {
      this.avionService.update(edition.idAvion, {
        model: valeurs.model!,
        capacite: valeurs.capacite!,
        statut: valeurs.statut!,
      }).subscribe({
        next: () => {
          this.chargerAvion();
          this.fermerFormulaire();
        },
        error: (err) => this.erreur.set('Erreur lors de la modification.'),
      });
    } else {
      this.avionService.create({
        model: valeurs.model!,
        capacite: valeurs.capacite!,
        statut: valeurs.statut!,
      }).subscribe({
        next: (nouvelAvion) => {
          this.avions.update(liste => [...liste, nouvelAvion]);
          this.fermerFormulaire();
        },
        error: (err) => this.erreur.set('Erreur lors de la création.'),
      });
    }
  }

  supprimer(id: number): void {
    if (!confirm('Voulez-vous vraiment supprimer cet avion ?')) return;
    this.avionService.delete(id).subscribe({
      next: () => {
        this.avions.update(liste => liste.filter(a => a.idAvion !== id));
      },
      error: (err) => this.erreur.set('Erreur lors de la suppression.'),
    });
  }
}