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
        // Anemone221 - what does Large Collection of Mechs - by Eternus do?
        // Raza5 - It adds a large collection of new mechs @Anemone221
        private async Task MessageReceived(SocketMessage message) {
            if (message.Channel.Name.ToLower().Contains("roguetech") || message.Channel.Name.ToLower().Contains("ticket")) {
                if (message.Content == "!bot") {
                    await message.Channel.SendMessageAsync(
                        "!assets - Link to asset bundle\n"+
                        "!beta - Asks for any unwanted installs\n"+
                        "!cache - Manual to delete all saved data\n" +
                        "!faq - Link to the FAQ page\n" +
                        "!logs - Log locations\n" +
                        "!map - Link to the ongoing mapping progress\n" +
                        "!output - output log locations\n" +
                        "!pins - Let people know about the pins\n" +
                        "!rt - Link to the mod download\n" +
                        "!rtfm - Link to the RogueTech manual\n" +
                        "!ticket - Tell people to open a ticket\n" +
                        "!warmap - Link to the Online Map\n" +
                        "!wiki - Link to the wiki\n"
                        );
                }
                else if (message.Content == "!ticket") {
                    await message.Channel.SendMessageAsync("Please enter a support ticket regarding your issue\nTickets can be opened in <#552500784659431436>\nJust scroll to the top and click the little envolope.");
                }
                else if (message.Content == "!assets") {
                    await message.Channel.SendMessageAsync("https://www.nexusmods.com/battletech/mods/393");
                }
                else if (message.Content == "!beta") {
                    await message.Channel.SendMessageAsync("Are you on any beta?\nHave you installed anything not in the installer?\nDid you delete mods folder before install?");
                }
                else if (message.Content == "!faq") {
                    await message.Channel.SendMessageAsync("https://roguetech.gamepedia.com/FAQ");
                }
                else if (message.Content == "!wiki") {
                    await message.Channel.SendMessageAsync("https://roguetech.gamepedia.com/");
                }
                else if (message.Content == "!cache") {
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
                else if (message.Content == "!pins") {
                    await message.Channel.SendMessageAsync("Please check the pins located at the top right (the little pin needle symbol).");
                }
                else if (message.Content == "!map") {
                    await message.Channel.SendMessageAsync("https://docs.google.com/spreadsheets/d/1FXDZMTEZwp71qCxqnaW3MpaB1ZfovaJudvZrM6pnOGk/edit?usp=sharing");
                }
                else if (message.Content == "!ping") {
                    await message.Channel.SendMessageAsync("Pong!");
                }
                else if (message.Content == "!rtfm") {
                    await message.Channel.SendMessageAsync("https://roguetech.gamepedia.com/");
                }
                else if (message.Content == "!rt") {
                    await message.Channel.SendMessageAsync("https://www.nexusmods.com/battletech/mods/79");
                }
                else if (message.Content == "!warmap") {
                    await message.Channel.SendMessageAsync("https://roguetech.org");
                }
                else if (message.Content == "!logs") {
                    await message.Channel.SendMessageAsync("BATTLETECH\\Mods\\\\.modtek\\ModTek.log\nBATTLETECH\\Mods\\cleaned_output_log.txt\n+ log from relevant mod folder if possible (example: BATTLETECH\\Mods\\MODNAME\\log.txt)\n\nJust drag and drop them in here.");
                }
                else if (message.Content == "!output") {
                    await message.Channel.SendMessageAsync("BATTLETECH\\Mods\\cleaned_output_log.txt\nJust drag and drop it in here.");
                }
                else if (message.Content == "!pone") {
                    await message.Channel.SendMessageAsync("<@248490263457038336>");

                    await message.Channel.SendMessageAsync("\n▒▒▒▒░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓\n▒▒▒░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓\n▒▒░▐▌░▒░░░░░░▒▒▒▒▒▒▒▒▒\n▒▒░░▌░░░░░░░░░░▒▒▒▒▒▒▒\n▒▒▒▒░░░░░░░░░░░░▓▓▓▒▒▒\n▒▒▒▒▒▒░░▀▀███░░░░▓▒▒▒▓\n▒▒▒▒▒▒░▌▄████▌░░░▓▒▒▒▓\n▒▒▒▒▒░░███▄█▌░░░▓▓▒▓▓▓\n▒▒▒▒▒▒▒░▀▀▀▀░░░░▓▓▒▒▓▓\n▒▒▒▒▒▒▒▒░░░░░░░░░▓▓▓▓▓\n▒▒▒▒▒▒▒░░░░░░▐▄░▓▓▓▓▓▓\n▒▒▒▒▒▒░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
                }
                else if (message.Content.Contains("!support")) {
                    await message.Channel.SendMessageAsync("Thank you for using skynet!\nPlease standby while we fetch our underpayed staff to help you.\nWhile you wait please provide us with following logs.\n\nBATTLETECH\\Mods\\\\.modtek\\ModTek.log\nBATTLETECH\\Mods\\cleaned_output_log.txt\n+ log from relevant mod folder if possible (example: BATTLETECH\\Mods\\MODNAME\\log.txt)\n\nJust drag and drop them in here.\n\nAlso please check <#562644533582495744> if your question is already listed.\n\nThank you for your patience!");
                }
            }
        }

        private Task Log(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
