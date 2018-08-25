using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace RogueBot {
    public class Program {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync() {
            var client = new DiscordSocketClient();

            client.Log += Log;
            client.MessageReceived += MessageReceived;
            string token = Secret.token; // Remember to keep this private!
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message) {
            if (message.Channel.Id == 455410123926405121) {
                if (message.Content == "!bot") {
                    await message.Channel.SendMessageAsync(
                        "!cache - Manual to delete all saved data\n" +
                        "!logs - Log locations\n" +
                        "!map - Link to the ongoing mapping progress\n" +
                        "!pins - Let people know about the pins\n" +
                        "!rt - Link to the mod download\n" +
                        "!rtfm - Link to the RogueTech manual\n" +
                        "!wartech - Link to the WarTech manual\n" +
                        "!wiki - Link to the wiki\n"
                        );
                }

                if (message.Content == "!wiki") {
                    await message.Channel.SendMessageAsync("https://roguetech.wikia.com/");
                }
                if (message.Content == "!cache") {
                    await message.Channel.SendMessageAsync(
                        "DELETE C:\\Users[USERNAME]\\AppData\\LocalLow\\Harebrained Schemes\\BATTLETECH\\profiles.dat (best delete it all)\n" +
                        "DELETE C:\\Users[USERNAME]\\AppData\\Local\\HarebrainedSchemes (delete it all too)\n" +
                        "DELETE C:\\Users[USERNAME]\\AppData\\Local\\Temp\\Harebrained Schemes\n" +
                        "DELETE steam\\steamapps\\shadercache\\637090\n" +
                        "DELETE steam\\userdata[USERID]\\637090\\remote\\C0\\settings_cloud.sav\n" +
                        "\n" +
                        "also delete in your registry HKEY_CURRENT_USER\\Software\\Harebrained Schemes\\BATTLETECH"
                        );
                }
                if (message.Content == "!pins") {
                    await message.Channel.SendMessageAsync("Please check the pins located at the top right (the little pin needle symbol).");
                }
                if (message.Content == "!map") {
                    await message.Channel.SendMessageAsync("https://docs.google.com/spreadsheets/d/1FXDZMTEZwp71qCxqnaW3MpaB1ZfovaJudvZrM6pnOGk/edit?usp=sharing");
                }
                if (message.Content == "!ping") {
                    await message.Channel.SendMessageAsync("Pong!");
                }
                if (message.Content == "!rtfm") {
                    await message.Channel.SendMessageAsync("https://www.nexusmods.com/battletech/mods/79?tab=articles");
                }
                if (message.Content == "!rt") {
                    await message.Channel.SendMessageAsync("https://www.nexusmods.com/battletech/mods/79");
                }
                if (message.Content == "!wartech") {
                    await message.Channel.SendMessageAsync("https://www.nexusmods.com/battletech/articles/76");
                }
                if (message.Content == "!logs") {
                    await message.Channel.SendMessageAsync("BATTLETECH\\Mods\\BTModLoader.log\nBATTLETECH\\Mods\\\\.modtek\\ModTek.log\nBATTLETECH\\BattleTech_Data\\output_log.txt\n+ log from relevant mod folder if possible (example: BATTLETECH\\Mods\\MODNAME\\log.txt)\n\nJust drag and drop them in here.");
                }
                if (message.Content == "!pone") {
                    await message.Channel.SendMessageAsync("<@248490263457038336>");

                    await message.Channel.SendMessageAsync("\n▒▒▒▒░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓\n▒▒▒░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓\n▒▒░▐▌░▒░░░░░░▒▒▒▒▒▒▒▒▒\n▒▒░░▌░░░░░░░░░░▒▒▒▒▒▒▒\n▒▒▒▒░░░░░░░░░░░░▓▓▓▒▒▒\n▒▒▒▒▒▒░░▀▀███░░░░▓▒▒▒▓\n▒▒▒▒▒▒░▌▄████▌░░░▓▒▒▒▓\n▒▒▒▒▒░░███▄█▌░░░▓▓▒▓▓▓\n▒▒▒▒▒▒▒░▀▀▀▀░░░░▓▓▒▒▓▓\n▒▒▒▒▒▒▒▒░░░░░░░░░▓▓▓▓▓\n▒▒▒▒▒▒▒░░░░░░▐▄░▓▓▓▓▓▓\n▒▒▒▒▒▒░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
                }
            }
        }

        private Task Log(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
