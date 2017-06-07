using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

class App
{
    private static readonly
        TelegramBotClient Bot = new TelegramBotClient("");
    private static readonly String[] Filter = File.ReadAllLines("Filter.txt");
    private static readonly Char[] Spliters = "#@\"\\/',.?!:;*&/+~- \n".ToCharArray();

    private static async void BotOnMessage(Object sender, MessageEventArgs e)
    {
        if (e.Message.Chat.Type == ChatType.Private || e.Message.Type != MessageType.TextMessage)
        {
            return;
        }
        if (e.Message.Text.ToLower().Split(Spliters).Any(i => Filter.Contains(i)))
        {
            try
            {
                await Bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException error)
            {

            }
        }
    }

    private static void Main()
    {
        Bot.OnMessage += BotOnMessage;
        Bot.StartReceiving();
        Console.ReadKey();
    }
}