using System;
using Telegram.Bot;

namespace Telegram_bot
{
    class Program
    {
        private static ITelegramBotClient botClient;

        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("6558772967:AAF9pO5ILzUVCqtpcIIG3cNygzkmkia71ac");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Bot id: {me.Id}. Имя бота: {me.FirstName}");
            Console.ReadKey();
        }
    }
}