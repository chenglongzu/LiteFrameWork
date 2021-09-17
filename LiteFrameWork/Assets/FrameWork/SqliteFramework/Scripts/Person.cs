using SQLite4Unity3d;
using LiteFramework;

public class Person:DataEntity  {

	public string Name { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Person: Id={0}, Name={1}, ]", Id, Name);
	}
}
