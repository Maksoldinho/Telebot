using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;


namespace Telebot
{
    class Data
    {
        public static string filepath = @"";
        
        public static async void createFile(TelegramBotClient client, MessageEventArgs e)
        {
            var msg = e.Message;
        
         filepath = $"C:\\Users\\admin\\OneDrive\\Desktop\\TelebotData\\TestSchedul{msg.From}.txt";
            TextWriter tw = new StreamWriter(filepath, true);
            tw.Close();
        }
    }
}
