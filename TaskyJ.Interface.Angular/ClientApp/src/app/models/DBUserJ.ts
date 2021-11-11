import { BaseEntity } from './BaseEntity';

export class DBUserJ implements BaseEntity {
  id: number;
  userName: string = null;
  password: string = null;

  public Equals(source: DBUserJ): boolean {
    return this.id == source.id &&
      this.userName == source.userName &&
      this.password == source.password;
  }

  public CopyFrom(source: DBUserJ): void {
    this.id = source.id;
    this.userName = source.userName;
    this.password = source.password;
  }
}
