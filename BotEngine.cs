using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace DostaevskyBot
{
    public class BotEngine
    {
        private readonly TelegramBotClient _botClient;

        public BotEngine(TelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        //крч создать слушатель который будет отслеживать (ожидать) сообщений от пользователя
        public async Task ListenForMessagesAsync()
        {
            using var cts = new CancellationTokenSource();

            //написать StartReceiving по документации
            var receiverOptions = new ReceiverOptions { AllowedUpdates = { } }; // создаем эземпляр

            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
                ); // передаем аргументы в метод StartReceiving, далее пишем по документации обработку обновления и обработку ошибок 
                   // HandleUpdateAsync, HandlePollingErrorAsync капипастим с документации


            var me = await _botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Id}");
            Console.ReadLine();
        }

        private async Task HandleUpdateAsync(ITelegramBotClient _botClient, Update update, CancellationToken cts)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Запрос '{messageText}' сообщения из чата {chatId}.");

            // Echo received message text
            Message sentMessage = await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Ты написал:\n" + messageText,
                cancellationToken: cts);
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient _botClient, Exception exception, CancellationToken cts)
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
