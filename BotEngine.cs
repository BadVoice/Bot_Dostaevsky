using System;
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

        }

    }
}
