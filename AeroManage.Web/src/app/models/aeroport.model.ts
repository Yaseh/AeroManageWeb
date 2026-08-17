export interface Aeroport {
  idIata: string;
  nom: string;
  ville: string;
  pays: string;
}

export interface CreateAeroportDto {
  idIata: string;
  nom: string;
  ville: string;
  pays: string;
}
