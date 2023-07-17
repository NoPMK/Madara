import { EnumModel } from "./types/enum-model";

export interface FormInitdataModel {

    comforts: Array<EnumModel>;
    conditions: Array<EnumModel>;
    counties: Array<EnumModel>;
    districts: Array<EnumModel>;
    floors: Array<EnumModel>;
    heats: Array<EnumModel>;
    parkings: Array<EnumModel>;
    properties: Array<EnumModel>;
}