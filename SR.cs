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
    class SR : Data
    {
       
        public async void WriteOut(TelegramBotClient client, MessageEventArgs e)
        {
            string zadacha;
             StreamReader sr = new StreamReader(filepath, true);

            var msg = e.Message;
            try
            {
            zadacha = sr.ReadToEnd();
                sr.Close();
                await client.SendTextMessageAsync(msg.Chat.Id, zadacha);
                Console.WriteLine("ama here");

            }
            catch (Exception)
            {
                zadacha = "There is nothing here yet";
            }
           
        }
    }
}
