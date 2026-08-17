export interface Personnel {
  idPersonnel: number;
  nom: string;
  prenom: string;
  role: string;
}

export interface CreatePersonnelDto {
  nom: string;
  prenom: string;
  role: string;
}
