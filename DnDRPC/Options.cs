namespace DnDRPC;
using System.CommandLine;
using System;
using System.Text;

class Options : RootCommand
{
  static readonly Option<bool> isDM = new(new[] { "--dm", "--is-dm" }, () => false, "If you're the DM");
  static readonly Option<string> name = new(new[] { "--name", "-n" }, "Character name");
  static readonly Option<string> className = new(new[] { "--class", "-c" }, "Character class");
  static readonly Option<byte> level = new(new[] { "--level", "--lvl", "-l" }, "Character level");
  static readonly Option<string> color = new(new[] { "--color", "-C" }, "Color for the D&D logo that appears on Discord");
  public Options(Action<string, string, byte, string, bool> callback) : base("Discord RPC for Dungeons and Dragons")
  {
    className.FromAmong(Character.Class.Artificer,
                        Character.Class.Barbarian,
                        Character.Class.Bard,
                        Character.Class.Cleric,
                        Character.Class.Druid,
                        Character.Class.Fighter,
                        Character.Class.Monk,
                        Character.Class.Paladin,
                        Character.Class.Ranger,
                        Character.Class.Rogue,
                        Character.Class.Sorcerer,
                        Character.Class.Warlock,
                        Character.Class.Wizard);
    color.FromAmong("red", "black", "white", "silver");

    color.SetDefaultValue("red");

    AddValidator((res) =>
    {
      StringBuilder errorMessage = new();

      var dmValue = res.GetValueForOption(isDM);
      string? nameValue = res.GetValueForOption(name);
      string? classValue = res.GetValueForOption(className);
      var levelValue = res.GetValueForOption(level);
      var validLevel = levelValue >= 1 && levelValue <= 20;

      if (dmValue && (classValue is not null || nameValue is not null || levelValue != 0))
        errorMessage.AppendLine("You can't specify a class, name or level for a DM");

      if (!dmValue && (classValue is null || nameValue is null))
        errorMessage.AppendLine("You must specify a class and name");

      if (!validLevel && !dmValue)
        errorMessage.AppendLine("Level must be between 1 and 20");

      string renderedMessage = errorMessage.ToString();
      res.ErrorMessage = renderedMessage.Length > 0 ? renderedMessage : null;
    });

    AddOption(name);
    AddOption(className);
    AddOption(level);
    AddOption(color);
    AddOption(isDM);
    this.SetHandler(callback, name, className, level, color, isDM);
  }
}
