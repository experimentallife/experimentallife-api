using SqlSugar;

namespace Project.Entity.Core.Base;

public class EntityRoot<T> where T : IEquatable<T>
{
  [SugarColumn(
    ColumnName = "id",
    ColumnDataType = "varchar",
    IsNullable = false,
    IsPrimaryKey = true
  )]
  public T Id { get; set; }

  [SugarColumn(
    ColumnName = "create_by",
    IsNullable = true
  )]
  public string CreateBy { get; set; }

  [SugarColumn(
    ColumnName = "create_time",
    IsNullable = true
  )]
  public DateTime? CreateTime { get; set; }

  [SugarColumn(
    ColumnName = "update_by",
    IsNullable = true
  )]
  public string UpdateBy { get; set; }

  [SugarColumn(
    ColumnName = "update_time",
    IsNullable = true
  )]
  public DateTime? UpdateTime { get; set; }

  [SugarColumn(
    ColumnName = "is_deleted",
    IsNullable = true
  )]
  public bool IsDeleted { get; set; } = false;
}