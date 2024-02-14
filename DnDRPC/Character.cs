namespace DnDRPC;

class Character
{
  internal string name { get; private init; }
  internal string rawClass { get; private init; }
  internal string prettifiedClass { get; private init; }
  internal byte level { get; private init; }
  public Character(string name, string className, byte level)
  {
    this.name = name;
    rawClass = className;
    prettifiedClass = $"{rawClass[0].ToString().ToUpper() + rawClass[1..]}";
    this.level = level;
  }

  internal static class Class
  {
    internal const string Artificer = "artificer";
    internal const string Barbarian = "barbarian";
    internal const string Bard = "bard";
    internal const string Cleric = "cleric";
    internal const string Druid = "druid";
    internal const string Fighter = "fighter";
    internal const string Monk = "monk";
    internal const string Paladin = "paladin";
    internal const string Ranger = "ranger";
    internal const string Rogue = "rogue";
    internal const string Sorcerer = "sorcerer";
    internal const string Warlock = "warlock";
    internal const string Wizard = "wizard";
  }
}
