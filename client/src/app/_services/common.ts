import { HttpParams } from "@angular/common/http";

export function getSchedulesParams(isInitial: boolean, direction: number, pageSize: number, date: Date){
    let params= new HttpParams();

    params = params.append('isInitial', isInitial.toString());
    params = params.append('loadDirection', direction == null ? 1 : direction .toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('date', date.toDateString());

    return params;
}