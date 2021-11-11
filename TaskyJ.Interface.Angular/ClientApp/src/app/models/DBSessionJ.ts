import { BaseEntity } from './BaseEntity';
import { DBRefreshTokenJ } from './DBRefreshTokenJ';

export class DBSessionJ implements BaseEntity {
  id: number;
  idUser: number;
  userName: string = null;
  vreateDate: Date = null;
  ipAddress: string = null;
  jwtToken: string = null;
  expires: Date = null;
  refreshToken: DBRefreshTokenJ;

  public Equals(source: DBSessionJ): boolean {
    return this.id == source.id;
  }

  public CopyFrom(source: DBSessionJ): void {
    this.id = source.id;
  }
}
