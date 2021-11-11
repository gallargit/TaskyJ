import { BaseEntity } from './BaseEntity';

export class DBCategoryJ implements BaseEntity {
  id: number;
  name: string = null;
  icon: string = null;
  
  public Equals(source: DBCategoryJ): boolean {
    return this.id == source.id &&
      this.name == source.name;
  }

  public CopyFrom(source: DBCategoryJ): void {
    this.id = source.id;
    this.name = source.name;
    this.icon = source.icon;
  }
}
