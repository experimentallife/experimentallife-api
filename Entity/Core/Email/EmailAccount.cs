using Project.Common.DI;
using Project.Entity.Core.Base;

using SqlSugar;

namespace Project.Entity.Core.Email;

[SugarTable("email_account")]
public class EmailAccount : EntityRoot<string>, ILocalizedTable
{
  [SugarColumn(
    ColumnName = "email",
    IsNullable = false
  )]
  public string Email { get; set; }

  [SugarColumn(
    ColumnName = "display_name",
    IsNullable = false
  )]
  public string DisplayName { get; set; }

  [SugarColumn(
    ColumnName = "host",
    IsNullable = false
  )]
  public string Host { get; set; }

  [SugarColumn(
    ColumnName = "port",
    ColumnDataType = "int",
    IsNullable = false
  )]
  public int Port { get; set; }

  [SugarColumn(
    ColumnName = "username",
    IsNullable = false
  )]
  public string Username { get; set; }

  [SugarColumn(
    ColumnName = "password",
    IsNullable = true
  )]
  public string Password { get; set; }

  [SugarColumn(
    ColumnName = "enable_ssl",
    ColumnDataType = "bit",
    IsNullable = false
  )]
  public bool EnableSsl { get; set; }

  [SugarColumn(
    ColumnName = "use_default_credentials",
    ColumnDataType = "bit",
    IsNullable = false
  )]
  public bool UseDefaultCredentials { get; set; }
}