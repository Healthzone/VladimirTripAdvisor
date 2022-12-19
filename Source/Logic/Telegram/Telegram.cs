using Microsoft.Build.Execution;
using System.Threading.Channels;
using TL;
using WTelegram;

namespace VladimirTripAdvisor.Logic.Telegram
{
    public static class TelegramGroup
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
        public static void InitialiseTelegramClient()
        {
            using var client = new WTelegram.Client(Config);
        }

        public static async void TelegramTest()
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
    }
}
