import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { PersonnelService } from '../../Services/personnel.service';
import { Personnel } from '../../models/personnel.model';

@Component({
  selector: 'app-personnel',
  imports: [ReactiveFormsModule],
  templateUrl: './personnel.html',
  styleUrl: './personnel.css',
})
export class PersonnelComponent implements OnInit {
  private personnelService = inject(PersonnelService);
  private fb = inject(FormBuilder);

  personnels = signal<Personnel[]>([]);
  erreur = signal<string | null>(null);
  afficherFormulaire = signal(false);
  modeEdition = signal<Personnel | null>(null);
  

  personnelForm = this.fb.group({
    nom: ['', Validators.required],
    prenom: ['', Validators.required],
    role: ['', Validators.required],
  });

  ngOnInit(): void {
    this.chargerPersonnels();
  }

  chargerPersonnels(): void {
    this.personnelService.getAll().subscribe({
      next: (data) => this.personnels.set(data),
      error: () => this.erreur.set('Erreur de chargement du personnel.'),
    });
  }

  ouvrirFormulaire(): void {
    this.modeEdition.set(null);
    this.personnelForm.reset();
    this.afficherFormulaire.set(true);
  }

  ouvrirEdition(personnel: Personnel): void {
    this.modeEdition.set(personnel);
    this.personnelForm.patchValue({ nom: personnel.nom, prenom: personnel.prenom, role: personnel.role });
    this.afficherFormulaire.set(true);
  }

  fermerFormulaire(): void {
    this.afficherFormulaire.set(false);
    this.modeEdition.set(null);
  }

  soumettre(): void {
    if (this.personnelForm.invalid) return;
    const valeurs = this.personnelForm.value;
    const edition = this.modeEdition();

    if (edition) {
      this.personnelService.update(edition.idPersonnel, {
        nom: valeurs.nom!,
        prenom: valeurs.prenom!,
        role: valeurs.role!,
      }).subscribe({
        next: () => { this.chargerPersonnels(); this.fermerFormulaire(); },
        error: () => this.erreur.set('Erreur lors de la modification.'),
      });
    } else {
      this.personnelService.create({
        nom: valeurs.nom!,
        prenom: valeurs.prenom!,
        role: valeurs.role!,
      }).subscribe({
        next: (nouveauPersonnel) => { this.personnels.update(liste => [...liste, nouveauPersonnel]); this.fermerFormulaire(); },
        error: () => this.erreur.set('Erreur lors de la création.'),
      });
    }
  }

  supprimer(id: number): void {
    if (!confirm('Voulez-vous vraiment supprimer ce membre du personnel ?')) return;
    this.personnelService.delete(id).subscribe({
      next: () => this.personnels.update(liste => liste.filter(p => p.idPersonnel !== id)),
      error: () => this.erreur.set('Erreur lors de la suppression.'),
    });
  }
}
