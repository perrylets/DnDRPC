# D&D RPC for Discord

Basic Discord RPC for Dungeons & Dragons.

## Usage

```txt
dnd-rpc [options]

Options:
  -n, --name <name>                                           Character name
  -c, --class                                                 Character class
  <artificer|barbarian|bard|cleric|druid|fighter|monk|paladin|ranger|rogue|sorcerer|warlock|wizard>
  -l, --level, --lvl <level>                                  Character level
  -C, --color <black|red|silver|white>                        Color for the D&D logo that appears on Discord [default: red]
  --dm, --is-dm                                               If you're the DM [default: False]
  --version                                                   Show version information
  -?, -h, --help                                              Show help and usage information
```

Supports all classes from the Player's Handbook and the Artificer.
Unless you are the DM (marked with the --dm flag)
you need to pass a character name,
class and level (between 1 and 20).
You can also pass the color for the D&D logo that will be displayed on Discord.

## Build and Run

### Requirements

* .NET SDK 8
* Discord Game SDK

### Steps

1. Download the Discord Game SDK
1. Copy the csharp folder into the root of the project.
1. Rename it to DiscordGameSDK.
1. Run `dotnet publish -c Release -r (your os' rid)`
1. Copy the appropriate binary file from the Discord Game SDK into the publish folder
1. Download the [D&D 5e vector icons](https://www.dropbox.com/s/4hw05xk8rhcth53/D%26D%205e%20Icons.zip?dl=0)
from [morepurplemorebetter](https://www.reddit.com/user/morepurplemorebetter/)
1. Turn the class icons into 1040 x 1040 PNGs
1. Create a discord application
1. Go to Rich Presence > Assets
1. Upload the PNGs to the Assets tab
    1. Name each of them the class name entirely in lowercase (eg. `rogue`)
1. Get the ampersand version of the D&D logo
(there's black, red, white and silver options, all of them are needed)
    1. Resize the images if they're not big enough
    1. Add them to the Assets tab with the names set to `ampersand_color`
    (eg. `ampersand_red`)
1. Add your application ID as a environment variable named `DISCORD_CLIENT_ID`
