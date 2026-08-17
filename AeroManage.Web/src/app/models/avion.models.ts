export interface Avion {
    idAvion:number;
    model:string;
    capacite:number;
    statut:string;
}

export interface CreateAvionDto {
    model: string;
    capacite:number;
    statut:string;
}