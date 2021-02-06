using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RogueBot
{
    public class Program
    {
        string PINKMECHMISSION = "I think you experience completely pink mechs in urban environment? That is a known (vanilla) issue relating to the higher memory usage on those maps. Your mechs will be fine, but a game restart will sort of help.";

        static DiscordSocketClient client = new DiscordSocketClient();
        List<string> rafflers = new List<string>();
        int ticketnr = 0;
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync() {
            LoadRafflers();
            LoadTicket();
            client.Log += Log;
            client.MessageReceived += MessageReceived;
            client.ChannelCreated += ChannelCreated;
            string token = Secret.token; // Remember to keep this private!
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task ChannelCreated(SocketChannel arg) {
            var channel = arg as SocketTextChannel;
            if(channel != null) {
                if (channel.Name.StartsWith("ticket-")) {
                    var server = channel.Guild as IGuild;
                    var categories = await server.GetCategoriesAsync();
                    var targetCategory = categories.FirstOrDefault(x => x.Name.ToLower() == "support") as SocketCategoryChannel;

                    if (targetCategory.Channels.Count >= 50) {
                        bool searchCategory = true;
                        for (int i = 2; searchCategory; i++) {
                            targetCategory = categories.FirstOrDefault(x => x.Name.ToLower() == "support" + i) as SocketCategoryChannel;
                            if (targetCategory == null) {
                                var role5 = channel.Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == "moderator");
                                var role4 = channel.Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == "admin");
                                await channel.SendMessageAsync(role5.Mention + role4.Mention + " Plx Halp, the ticket list is full please create: support" + i + " for me. Thanks <3");
                                return;
                            }
                            else if (targetCategory.Channels.Count < 50) {
                                searchCategory = false;
                            }
                        }
                    }
                    await channel.ModifyAsync(prop => prop.CategoryId = targetCategory.Id);
                }
            }
        }

        // Anemone221 - what does Large Collection of Mechs - by Eternus do?
        // Raza5 - It adds a large collection of new mechs @Anemone221
        private async Task MessageReceived(SocketMessage message) {

            foreach (SocketUser user in message.MentionedUsers) {
                if (user.Mention.Equals(client.CurrentUser.Mention)) {
                    await message.Channel.TriggerTypingAsync();
                    await message.Channel.SendMessageAsync("Woof!");
                }
            }

            if (message.Content == "!bot") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync(

                    "!assets - Link to asset bundle\n" +
                    "!beta - Asks for any unwanted installs\n" +
                    "!cache - Manual to delete all saved data\n" +
                    "!faq - Link to the FAQ page\n" +
                    "!logs - Log locations\n" +
                    "!map - Link to the ongoing mapping progress\n" +
                    "!output - output log locations\n" +
                    "!pins - Let people know about the pins\n" +
                    "!rt - Link to the mod download\n" +
                    "!rtfm - Link to the RogueTech manual\n" +
                    "!saves - Link to Savefiles\n" +
                    "!ticket - Tell people to open a ticket\n" +
                    "!warmap - Link to the Online Map\n" +
                    "!wiki - Link to the wiki\n" +
                    "!rafflecount - Shows number of rafflers\n" +
                    "!rafflereset - Resets Raffle, Admin only\n" +
                    "!raffle - enters the raffle\n" +
                    "!traits - traits bug\n" +
                    "!winner - draws the raffle winner, Admin only\n"
                    );
            }
            else if (message.Content == "!ticket") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("Please enter a support ticket regarding your issue, \ntickets can be opened by typing \"!openticket\"");
            }
            else if (message.Content == "!traits") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("As per <#565112288315703316>: - Item effects descriptions/Traits are not displayed in the mechlab - restart the game without updating, happens after update");
            }
            else if (message.Content == "!jf") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("Who?");
            }
            else if (message.Content == "!assets") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("https://www.nexusmods.com/battletech/mods/393");
            }
            else if (message.Content == "!beta") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("Are you on any beta?\nHave you installed anything not in the installer?\nDid you delete mods folder before install?");
            }
            else if (message.Content == "!faq") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("https://roguetech.gamepedia.com/FAQ");
            }
            else if (message.Content == "!wiki") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("https://roguetech.gamepedia.com/");
            }
            else if (message.Content == "!cache") {
                await message.Channel.TriggerTypingAsync();
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
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("Please check the pins located at the top right (the little pin needle symbol).");
            }
            else if (message.Content == "!map") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("https://docs.google.com/spreadsheets/d/1FXDZMTEZwp71qCxqnaW3MpaB1ZfovaJudvZrM6pnOGk/edit?usp=sharing");
            }
            else if (message.Content == "!ping") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("Pong!");
            }
            else if (message.Content.Contains("!rtfm") || message.Content == "<:RTFM:807604052325826631>") {
                string[] topic = message.Content.Split(' ');
                string extra = "";
                if (topic.Length > 1) {
                    extra = topic[1];
                }
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("https://roguetech.gamepedia.com/" + extra);
            }
            else if (message.Content == "!rt") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("https://www.nexusmods.com/battletech/mods/79");
            }
            else if (message.Content == "!warmap") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("https://roguetech.org");
            }
            else if (message.Content.StartsWith("!factionstats ")) {
                await message.Channel.TriggerTypingAsync();
                string payload = message.Content.Substring(14);
                string[] datas = payload.Split(' ');
                string minutes = datas[1];
                string faction = datas[0].ToLower();
                string html = string.Empty;
                var user = message.Author as SocketGuildUser;
                var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Admin");

                string url = @"http://roguetech.org:8000/warServices/Factions/" + faction + "/Companies/?MinutesBack=" + minutes;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream)) {
                    html = reader.ReadToEnd();
                }
                List<string> companies = JsonConvert.DeserializeObject<List<string>>(html);
                var output = "Active Companies for " + faction + " in the last " + minutes + " minutes:\n";
                if (!faction.Equals("wordofblake") || user.Roles.Contains(role)) {
                    foreach (string company in companies) {
                        output += company + "\n";
                    }

                }
                await message.Channel.SendMessageAsync(output);
            }
            /* else if (message.Content.StartsWith("!warstats ")) {
                 await message.Channel.TriggerTypingAsync();
                 string minutes = message.Content.Substring(10);
                 string html = string.Empty;
                 string url = @"http://roguetech.org:8000/warServices//Factions/Active/?MinutesBack=" + minutes;

                 HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                 request.AutomaticDecompression = DecompressionMethods.GZip;

                 using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                 using (Stream stream = response.GetResponseStream())
                 using (StreamReader reader = new StreamReader(stream)) {
                     html = reader.ReadToEnd();
                 }
                 JArray a = JArray.Parse(html);
                 var Resultobject = new Dictionary<string, int>();
                 foreach (JObject o in a.Children<JObject>()) {
                     Resultobject.Add(o.GetValue("Key").ToString(), (int)o.GetValue("Value"));
                 }
                 var sortedDict = from entry in Resultobject orderby entry.Value descending select entry;
                 var output = "Active Players per Faction in the last " + minutes + " minutes:\n";
                 var user = message.Author as SocketGuildUser;
                 var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Admin");
                 foreach (KeyValuePair<string, int> pair in sortedDict) {
                     if (!pair.Key.Equals("WordOfBlake") || user.Roles.Contains(role)) {
                         output += pair.Key + ": " + pair.Value + "\n";
                     }
                 }
                 await message.Channel.SendMessageAsync(output);
             }*/
            else if (message.Content.StartsWith("!warstats ")) {
                await message.Channel.TriggerTypingAsync();
                string minutes = message.Content.Substring(10);
                string html = string.Empty;
                string url = @"https://161.97.74.91:16001/api/roguewarservices/getwarstats/?MinutesBack=" + minutes;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ServerCertificateValidationCallback = (sender, certificate, chain, policyErrors) => { return true; };
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream)) {
                    html = reader.ReadToEnd();
                }
                JObject a = JObject.Parse(html);
                var Resultobject = new Dictionary<string, int>();
                foreach (var pair in a) {
                    Resultobject.Add(pair.Key, (int)pair.Value);
                }
                var sortedDict = from entry in Resultobject orderby entry.Value descending select entry;
                var output = "Active Players per Faction in the last " + minutes + " minutes:\n";
                var user = message.Author as SocketGuildUser;
                foreach (KeyValuePair<string, int> pair in sortedDict) {
                    output += pair.Key + ": " + pair.Value + "\n";
                }
                await message.Channel.SendMessageAsync(output);
            }
            else if (message.Content == "!logs" || message.Content == "<:logs:655931426792931349>") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("For <:logs:655931426792931349> just press the \"Gather Logs\" button in the launcher, then drag and drop the zip in here.");
            }
            else if (message.Content == "!output") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("BATTLETECH\\Mods\\cleaned_output_log.txt\nJust drag and drop it in here.");
            }
            else if (message.Content == "!saves") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("Saves can be found here:\nBattletech/RogueTechSaves");
            }
            else if (message.Content == "!pone") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("<@248490263457038336>");

                await message.Channel.SendMessageAsync("\n▒▒▒▒░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓\n▒▒▒░░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓\n▒▒░▐▌░▒░░░░░░▒▒▒▒▒▒▒▒▒\n▒▒░░▌░░░░░░░░░░▒▒▒▒▒▒▒\n▒▒▒▒░░░░░░░░░░░░▓▓▓▒▒▒\n▒▒▒▒▒▒░░▀▀███░░░░▓▒▒▒▓\n▒▒▒▒▒▒░▌▄████▌░░░▓▒▒▒▓\n▒▒▒▒▒░░███▄█▌░░░▓▓▒▓▓▓\n▒▒▒▒▒▒▒░▀▀▀▀░░░░▓▓▒▒▓▓\n▒▒▒▒▒▒▒▒░░░░░░░░░▓▓▓▓▓\n▒▒▒▒▒▒▒░░░░░░▐▄░▓▓▓▓▓▓\n▒▒▒▒▒▒░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
            }
            else if (message.Content == "!soda") {
                await message.Channel.TriggerTypingAsync();

                await message.Channel.SendFileAsync("C:/Users/Administrator/Desktop/soda.jpg");
            }
            else if (message.Content.Contains("!support")) {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("Thank you for using skynet!\nPlease explain your problem in detail and standby while we fetch our underpaid staff to help you.\nWhile you wait please press the gather logs button in the launcher and upload the zip here.\n\nAlso please check <#565112288315703316> if your question is already listed.\n\nThank you for your patience!");
            }
            else if (message.Content == "!raffle") {
                await message.Channel.TriggerTypingAsync();
                if (AddRaffler(message.Author.Mention)) {
                    await message.Channel.SendMessageAsync("Thanks " + message.Author.Mention + " you have entered the raffle!");
                }

            }
            else if (message.Content == "!rafflecount") {
                await message.Channel.TriggerTypingAsync();
                await message.Channel.SendMessageAsync("There is currently " + rafflers.Count + " rafflers!");
            }
            else if (message.Content == "!rafflereset") {
                await message.Channel.TriggerTypingAsync();
                var user = message.Author as SocketGuildUser;
                var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Admin");
                if (user.Roles.Contains(role)) {
                    if (ResetRaffle()) {
                        await message.Channel.SendMessageAsync("Raffle has been reset!");
                    }
                }
            }
            else if (message.Content == "!winner") {
                await message.Channel.TriggerTypingAsync();
                var user = message.Author as SocketGuildUser;
                var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Admin");
                if (user.Roles.Contains(role)) {
                    var random = new Random();
                    int index = random.Next(rafflers.Count);
                    await message.Channel.SendMessageAsync("Winner is " + rafflers[index]);
                }
            }
            else if (message.Content == "!openticket") {
                await message.Channel.TriggerTypingAsync();
                var user = message.Author as SocketGuildUser;
                var server = (user as IGuildUser).Guild;
                var categories = await server.GetCategoriesAsync();
                var targetCategory = categories.FirstOrDefault(x => x.Name.ToLower() == "support") as SocketCategoryChannel;

                if (targetCategory.Channels.Count >= 50) {
                    bool searchCategory = true;
                    for (int i = 2; searchCategory; i++) {
                        targetCategory = categories.FirstOrDefault(x => x.Name.ToLower() == "support" + i) as SocketCategoryChannel;
                        if (targetCategory == null) {
                            var role5 = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == "moderator");
                            var role4 = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == "admin");
                            await message.Channel.SendMessageAsync(role5.Mention + role4.Mention + " Plx Halp, the ticket list is full please create: support" + i + " for me. Thanks <3");
                            return;
                        }
                        else if (targetCategory.Channels.Count < 50) {
                            searchCategory = false;
                        }
                    }
                }

                ticketnr++;
                var channel = await server.CreateTextChannelAsync("RogueTicket-" + ticketnr, x => {
                    x.CategoryId = targetCategory.Id;
                    x.Topic = $"This channel was created at {DateTimeOffset.UtcNow} by {user}.";
                });
                OverwritePermissions permissions = new OverwritePermissions(PermValue.Deny, PermValue.Deny,
                    PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny,
                    PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Allow,
                    PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny, PermValue.Deny, PermValue.Allow, PermValue.Deny, PermValue.Deny);
                var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == "rt-crew");
                OverwritePermissions permissions3 = new OverwritePermissions(PermValue.Deny, PermValue.Deny,
                   PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny,
                   PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Allow,
                   PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny, PermValue.Deny, PermValue.Allow, PermValue.Deny, PermValue.Deny);
                var role3 = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == "modder");
                OverwritePermissions permissions2 = new OverwritePermissions(PermValue.Deny, PermValue.Allow,
                    PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny,
                    PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Allow,
                    PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny, PermValue.Deny, PermValue.Allow, PermValue.Deny, PermValue.Deny);
                var role2 = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == "moderator");
                await channel.AddPermissionOverwriteAsync(role, permissions);
                await channel.AddPermissionOverwriteAsync(role2, permissions2);
                await channel.AddPermissionOverwriteAsync(role3, permissions3);
                await channel.AddPermissionOverwriteAsync(user, permissions);
                await channel.SendMessageAsync("Thank you for using skynet!\nPlease explain your problem in detail and standby while we fetch our underpaid staff to help you.\nWhile you wait please press the gather logs button in the launcher and upload the zip here.\n\nAlso please check <#565112288315703316> if your question is already listed.\n\nThank you for your patience!");
                await channel.SendMessageAsync(user.Mention);
                await message.Channel.SendMessageAsync("Ticket opened: " + channel.Mention);
                SaveTickets(ticketnr);
            }

            else if (message.Content == "!close" && message.Channel.Name.ToLower().StartsWith("rogueticket")) {
                var channel = message.Channel as ITextChannel;
                var user = message.Author as SocketGuildUser;
                var server = (user as IGuildUser).Guild;
                var categories = await server.GetCategoriesAsync();
                var targetCategory = categories.FirstOrDefault(x => x.Name.ToLower() == "support - shit to be investigated");
                var originalname = channel.Name;
                await channel.ModifyAsync(x => {
                    x.Name = originalname + "-closed";
                    x.CategoryId = targetCategory.Id;
                });
                await channel.SyncPermissionsAsync();
                OverwritePermissions permissions = new OverwritePermissions(PermValue.Deny, PermValue.Deny,
                   PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny,
                   PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Allow,
                   PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny, PermValue.Deny, PermValue.Allow, PermValue.Deny, PermValue.Deny);
                var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == "rt-crew");
                OverwritePermissions permissions2 = new OverwritePermissions(PermValue.Deny, PermValue.Allow,
                    PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny,
                    PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Allow,
                    PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny, PermValue.Deny, PermValue.Allow, PermValue.Deny, PermValue.Deny);
                var role2 = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == "moderator");
                OverwritePermissions permissions3 = new OverwritePermissions(PermValue.Deny, PermValue.Deny,
                   PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny,
                   PermValue.Allow, PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Allow,
                   PermValue.Allow, PermValue.Allow, PermValue.Deny, PermValue.Deny, PermValue.Deny, PermValue.Allow, PermValue.Deny, PermValue.Deny);
                var role3 = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name.ToLower() == "modder");
                await channel.AddPermissionOverwriteAsync(role, permissions);
                await channel.AddPermissionOverwriteAsync(role2, permissions2);
                await channel.AddPermissionOverwriteAsync(role3, permissions3);
            }

            else if (message.Content == "!delete" && message.Channel.Name.ToLower().StartsWith("rogueticket") && message.Channel.Name.ToLower().EndsWith("closed")) {
                var channel = message.Channel as ITextChannel;
                await channel.DeleteAsync();
            }

            //AUTO HELPER

            else if (message.Content.ToLower().Contains("pink") && message.Content.ToLower().Contains("mech") && message.Content.ToLower().Contains("mission")) {
                var user = message.Author as SocketGuildUser;
                if (!user.IsBot) {
                    await message.Channel.TriggerTypingAsync();
                    await message.Channel.SendMessageAsync(PINKMECHMISSION);
                }
            }
            else if (message.Content.ToLower().Contains("pink") && message.Content.ToLower().Contains("mech") && message.Content.ToLower().Contains("battle")) {
                var user = message.Author as SocketGuildUser;
                if (!user.IsBot) {
                    await message.Channel.TriggerTypingAsync();
                    await message.Channel.SendMessageAsync(PINKMECHMISSION);
                }
            }
        }

        private Task Log(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private bool AddRaffler(string entry) {
            try {
                if (!rafflers.Contains(entry)) {
                    rafflers.Add(entry);
                    string result = JsonConvert.SerializeObject(rafflers, Formatting.Indented);
                    System.IO.File.WriteAllText("Saves/CurrentRafflers.json", result);
                    return true;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
            return false;
        }

        private bool ResetRaffle() {
            rafflers.Clear();
            string result = JsonConvert.SerializeObject(rafflers, Formatting.Indented);
            System.IO.File.WriteAllText("Saves/CurrentRafflers.json", result);
            return true;
        }

        private void LoadRafflers() {
            string load = System.IO.File.ReadAllText("Saves/CurrentRafflers.json");
            rafflers = JsonConvert.DeserializeObject<List<string>>(load);
        }

        private void LoadTicket() {
            string load = System.IO.File.ReadAllText("Saves/Tickets.json");
            ticketnr = int.Parse(load);
        }

        private bool SaveTickets(int ticketnr) {
            System.IO.File.WriteAllText("Saves/Tickets.json", ticketnr.ToString());
            return true;
        }
    }
}
