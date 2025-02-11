export interface event {
    eventId: number;
    name: string;
    description: string;
    dateTime: Date;
    location: string;
    maxCapacity: number;
    createByUserId: number;
}