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
                if (message.Content == "!pone") {
                    await message.Channel.SendMessageAsync("<@248490263457038336>");

                    await message.Channel.SendMessageAsync("▒▒▒▒░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓\n▒▒▒░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓\n▒▒░▐▌░▒░░░░░░▒▒▒▒▒▒▒▒▒\n▒▒░░▌░░░░░░░░░░▒▒▒▒▒▒▒\n▒▒▒▒░░░░░░░░░░░░▓▓▓▒▒▒\n▒▒▒▒▒▒░░▀▀███░░░░▓▒▒▒▓\n▒▒▒▒▒▒░▌▄████▌░░░▓▒▒▒▓\n▒▒▒▒▒░░███▄█▌░░░▓▓▒▓▓▓\n▒▒▒▒▒▒▒░▀▀▀▀░░░░▓▓▒▒▓▓\n▒▒▒▒▒▒▒▒░░░░░░░░░▓▓▓▓▓\n▒▒▒▒▒▒▒░░░░░░▐▄░▓▓▓▓▓▓\n▒▒▒▒▒▒░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
                }
            }
        }

        private Task Log(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
