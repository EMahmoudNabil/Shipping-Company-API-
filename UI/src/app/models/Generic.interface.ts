import { Observable } from "rxjs";

export interface GenericCURD<T> {
getAll(): Observable<T[]>;
getById(id: number|string): Observable<T>;
create(item: T): Observable<T>;
update(id: number|string,item: T): Observable<T>;
delete(id: number|string): Observable<void>;
}