export interface Vol {
  idVol: number;
  numeroVol: string;
  dateDepart: string;
  dateArrivee: string;
  statut: string;
  aeroportDepart: string;
  aeroportArrivee: string;
  idAvion: number;
  idCommandant: number;
}

export interface CreateVolDto {
  numeroVol: string;
  dateDepart: string;
  dateArrivee: string;
  statut: string;
  aeroportDepart: string;
  aeroportArrivee: string;
  idAvion: number;
  idCommandant: number;
}
