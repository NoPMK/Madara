export interface GetAllPropertiesModel {
  id: number,
  photos: string,
  county: string,
  cityName: string,
  district: string,
  street: string,
  streetNumber: number,
  isForSale: boolean,
  propertySize: number,
  numberOfRooms: number,
  price: number,
  userId: string,
  isUserValidToEdit: boolean
}
