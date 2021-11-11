import { BaseEntity } from './BaseEntity';

export class DBRefreshTokenJ implements BaseEntity {
  id: number;
  token: string = null;
  expires: Date = null;
  created: Date = null;
  createdByIp: string = null;
  revoked: Date = null;
  revokedByIp: string = null;
  replacedByToken: string = null;

  /*
          public bool IsExpired => DateTime.UtcNow >= Expires;
          public bool IsActive => Revoked == null && !IsExpired;
  */

  public Equals(source: DBRefreshTokenJ): boolean {
    return this.id == source.id;
  }

  public CopyFrom(source: DBRefreshTokenJ): void {
    this.id = source.id;
  }
}
