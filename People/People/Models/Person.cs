using SQLite;

namespace People.Models;

[Table("People")]
public class Person
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    [MaxLength(250), Unique]
    public string Name { get; set; } = string.Empty;
}
