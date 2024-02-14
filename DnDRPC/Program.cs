using System;
using System.CommandLine;
using System.Threading.Tasks;
using Discord;
namespace DnDRPC;
class Program
{

  static int Main(string[] args)
  {
    var options = new Options(Callback);
    return options.Invoke(args);
  }

  static void Callback(string name, string className, byte level, string color, bool isDM)
  {
    long clientId = long.Parse(Environment.GetEnvironmentVariable("DISCORD_CLIENT_ID")!);

    Character? character = null;
    var information = "";
    if (!isDM)
    {
      character = new Character(name, className, level);
      information = $"{character!.prettifiedClass} lvl {character.level}";
    }

    var discord = new Discord.Discord(clientId, (ulong)CreateFlags.Default);

    var activity = new Activity
    {
      ApplicationId = clientId,
      Assets = {
        LargeImage = $"ampersand_{color}",
        LargeText = !isDM ? character?.name! : "DMing the session",
        SmallImage = character?.rawClass!,
        SmallText = character is not null ? information : ""
      },
      State = "In a session",
      Timestamps = {
        Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
      },
      Details = isDM ? "DMing the session" : $"{character!.name} - {information}",
      Instance = true
    };

    var activityManager = discord.GetActivityManager();
    activityManager.UpdateActivity(activity, ResultCallback);
    var run = true;
    Console.CancelKeyPress += (_, e) =>
    {
      e.Cancel = true;
      run = false;
    };

    while (run)
    {
      Task.Delay(16).Wait();
      discord.RunCallbacks();
    }
    activityManager.ClearActivity(ResultCallback);
    discord.Dispose();
  }
  static void ResultCallback(Result res)
  {
    if (res != Result.Ok && res != Result.TransactionAborted)
      throw new ResultException(res);
  }

}


