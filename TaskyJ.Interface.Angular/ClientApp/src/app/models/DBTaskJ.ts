import { BaseEntity } from './BaseEntity';
import { TaskPriority } from './TaskPriority';

export class DBTaskJ implements BaseEntity {
  id: number;
  name: string = null;
  description: string = null;
  creationDate: Date = null;
  deadline: Date = null;
  finishDate: Date = null;
  //[Range(0, 100, ErrorMessage = "Value must range from 0 to 100")]
  priority: TaskPriority = null;
  deleted: boolean = null;
  idCategory: number = null;
  idUser: number = null;

  public Equals(source: DBTaskJ): boolean {
    return this.id == source.id &&
      this.name == source.name &&
      this.description == source.description &&
      this.creationDate == source.creationDate &&
      this.deadline == source.deadline &&
      this.finishDate == source.finishDate &&
      this.priority == source.priority &&
      this.deleted == source.deleted &&
      this.idCategory == source.idCategory &&
      this.idUser == source.idUser;
  }

  public CopyFrom(source: DBTaskJ): void {
    this.id = source.id;
    this.name = source.name;
    this.description = source.description;
    this.creationDate = source.creationDate;
    this.deadline = source.deadline;
    this.finishDate = source.finishDate;
    this.priority = source.priority;
    this.deleted = source.deleted;
    this.idCategory = source.idCategory;
    this.idUser = source.idUser;
  }
}
