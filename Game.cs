using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Telebot
{
    enum Chapter2
    {
        InGame = 0
    }
    class Game
    {
        static int tries = 0;
        static Random rnd = new Random();
        private static int x = rnd.Next(1, 100);

        [Obsolete]
        public async void LowHigh(TelegramBotClient client, MessageEventArgs e)
        {
            
            var msg = e.Message;
            Console.WriteLine(x);
            try
            {
                int g = Convert.ToInt32(msg.Text);
                if (g > x)
                {
                    tries++;
                    await client.SendTextMessageAsync(msg.Chat.Id, "Меньше нало балбес. Ну ты и обосрышь");
                }
                else if (g < x)
                {
                    tries++;
                    await client.SendTextMessageAsync(msg.Chat.Id, "Больше надо чеееел. Ты что оболтус");
                }
                else
                {
                    tries++;
                    await client.SendTextMessageAsync(msg.Chat.Id, $"Красава брат. Ты угадунькал со стольких попыток: {tries}");

                    var f = new Program();
                    Program.inGame = false;
                }
            } catch (Exception)
            {
                await client.SendTextMessageAsync(msg.Chat.Id, "Invalid input. Please enter only numbers");
            }

        }

        //private void Rand()
        //{
        //    Random rnd = new Random();
        //    x = rnd.Next(1, 100);
        //    Console.WriteLine(x);

        //}
    }
}
