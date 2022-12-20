using Microsoft.Build.Execution;
using System.Threading.Channels;
using TL;
using WTelegram;

namespace VladimirTripAdvisor.Logic.Telegram
{
    public class Telegram
    {
        private static string Config(string what)
        {
            switch (what)
            {
                case "api_id": return "28837445";
                case "api_hash": return "f75175336330b8fb0deb87b39c5969bf";
                case "phone_number": return "+79106648980";
                case "verification_code": Console.Write("Code: "); return Console.ReadLine();
                case "first_name": return "John";      // if sign-up is required
                case "last_name": return "Doe";        // if sign-up is required
                case "password": return "450912";     // if user has enabled 2FA
                default: return null;                  // let WTelegramClient decide the default config
            }
        }
        public void InitialiseTelegramClient()
        {
            using var client = new WTelegram.Client(Config);
        }

        public async void TelegramTest()
        {
            using (var client = new WTelegram.Client(Config))
            {
                await client.ConnectAsync();

                var contacts = await client.Contacts_GetContacts();
                Console.WriteLine("This user has joined the following:");
                foreach (var users in contacts.users)
                {
                    Console.WriteLine($"{users.Key}: {users.Value}");
                }
                Console.Write("Type a chat ID to send a message: ");
                long contactsId = long.Parse(Console.ReadLine());
                var target = contacts.users[contactsId];
                Console.Write("Message: ");
                string message = Console.ReadLine();
                Console.WriteLine($"Sending a message in chat {contactsId}: {target.first_name} {target.last_name}");
                await client.SendMessageAsync(target, message);
            }
        }

        public async void CreateTelegramChannel()
        {
            using (var client = new WTelegram.Client(Config))
            {
                await client.ConnectAsync();
                var dialogs = await client.Messages_GetAllDialogs();

                //var channel = await client.Channels_CreateChannel("Мероприятие1", "Это мероприятие", false);
                //var user = client.Users_GetFullUser(new InputUser(878424347, 878424347));
                //user.RunSynchronously();
                var users = new InputUser[1];
                for (int i = 0; i < users.Length; i++)
                {
                    users[i] = new InputUser(5802032660, -1117118480243990476);
                }   
                //var channel = await client.Messages_CreateChat(users, "Тестовое мероприятие2");
            }
        }
    }
}
