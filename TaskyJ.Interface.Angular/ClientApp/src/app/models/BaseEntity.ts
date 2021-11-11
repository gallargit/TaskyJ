export interface BaseEntity {
  id: number;

  Equals(source: BaseEntity): boolean;
  CopyFrom(source: BaseEntity): void;
}
