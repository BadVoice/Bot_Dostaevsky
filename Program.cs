using DostaevskyBot;
using Telegram.Bot;

var botClient = new TelegramBotClient(AccessTokens.Telegram);

// Create a new bot instance
var dostaevskyBot = new BotEngine(botClient);

// Listen for messages sent to the bot
await dostaevskyBot.ListenForMessagesAsync();