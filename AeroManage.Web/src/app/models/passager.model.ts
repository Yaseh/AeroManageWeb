export interface Passager {
  idPassager: number;
  nom: string;
  prenom: string;
  nationalite: string;
}

export interface CreatePassagerDto {
  nom: string;
  prenom: string;
  nationalite: string;
}
