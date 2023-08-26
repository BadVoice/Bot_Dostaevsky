using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;



namespace DostaevskyBot {


    class Program {

        // Bot Token
        public static TelegramBotClient Client;

        //Messages and user info
        long chatId = 0;
        string messageText;
        int messageId;
        string firstName;
        string lastName;
        long id;
        Message sentMessage;

        public static CancellationTokenSource cts;


         static void Main(string[] args)
        {
            Client = new TelegramBotClient("6558772967:AAF9pO5ILzUVCqtpcIIG3cNygzkmkia71ac");
            cts = new CancellationTokenSource();

            Program me = new Program();

            me.TestBot(Client, cts);


        }
         
        public void TestBot(ITelegramBotClient Client, CancellationTokenSource cts)
        {
            var bot = Client.GetMeAsync().Result;

            Console.WriteLine($"{bot.Id}, {bot.Username}");


            cts.Cancel();
        }

        // Начало приема бота, не блокирует вызывающий поток.
        // Прием осуществляется на ThreadPool.
        
        public void StartReceiving(ITelegramBotClient Client, CancellationTokenSource cts)
        {
            var receiverOptions = new ReceiverOptions { AllowedUpdates = { } };

            Client.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken: cts.Token);
        }




        // Ответ бота в input

       async Task HandleUpdateAsync(ITelegramBotClient Client, Update update, CancellationToken cts)
        {
            if (update.Type != UpdateType.Message)
                return;

            if (update.Message!.Type != MessageType.Text)
                return;

            //set variables
            chatId = update.Message.Chat.Id;
            messageText = update.Message.Text;

           
        }


        Task HandleErrorAsync(ITelegramBotClient Client, Exception exception, CancellationToken cts)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}

