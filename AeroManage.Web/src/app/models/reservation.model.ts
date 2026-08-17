export interface Reservation {
  idReservation: number;
  numerosiege: string;
  idVol: number;
  idPassager: number;
}

export interface CreateReservationDto {
  numerosiege: string;
  idVol: number;
  idPassager: number;
}
