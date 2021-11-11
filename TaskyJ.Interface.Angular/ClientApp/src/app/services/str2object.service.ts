import { Injectable } from '@angular/core';
import { DBCategoryJ } from '../models/DBCategoryJ';
import { DBRefreshTokenJ } from '../models/DBRefreshTokenJ';
import { DBSessionJ } from '../models/DBSessionJ';
import { DBTaskJ } from '../models/DBTaskJ';
import { DBUserJ } from '../models/DBUserJ';
import { BaseEntity } from '../models/BaseEntity';

@Injectable({ providedIn: 'root' })
export class Str2ObjectService {
  constructor() { }
    getDbObject(strObject: string): BaseEntity {
        if (strObject == "task")
            return new DBTaskJ();
        if (strObject == "category")
            return new DBCategoryJ();

        return null;
    }

    cloneObject(sourceObject: BaseEntity): BaseEntity {
      return JSON.parse(JSON.stringify(sourceObject));
    }
}
