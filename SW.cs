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
    class SW : Clear
    {
        
        public bool err = false;
        public int step = 0;
        public DateTime date;
        public string zadacha;
        public static bool inSchedule = false;

        public async void WriteIn1(TelegramBotClient client, MessageEventArgs e)
        {
            var msg = e.Message;
            zadacha = msg.Text;
           

        }
        public async void WriteIn2(TelegramBotClient client, MessageEventArgs e)
        {
            var tim = new _Timer();
            var msg = e.Message;
            Console.WriteLine(msg.Text);
            Console.WriteLine(err);
            var sw = new StreamWriter(filepath, true);
            try
            {
                string when = msg.Text;
                date = Convert.ToDateTime(when);
                //Date data start
                int year = date.Year;
                int month = date.Month;
                int day = date.Day;
                int hour = date.Hour;
                int minutes = date.Minute;
                int seconds = date.Second;
                //Date data end
                
                //Timer start
                _Timer.schedule_Timer_Start(year, month, day, hour, minutes, seconds);
                Console.WriteLine(_Timer.Timer_HasElapsed + "In SW class ");
                //Timer end

                sw.WriteLine("\nYour task is: " + zadacha + "\n" +
                    "And you wanna do it at this date: " + date);
                sw.Close();
            }
            catch (Exception)
            {
                sw.Close();
                await client.SendTextMessageAsync(msg.Chat.Id, "Invalid input. Неправильный ввод. Данные не сохранены");
                step = 2;
            }

        }
        public async void Schedule(TelegramBotClient client, MessageEventArgs e)
        {
            var msg = e.Message;

             if (msg != null && step == 0)
            {
                Console.WriteLine("User said yes");
                //ScheduleInput.WriteIn1(client, e);
                if (msg.Text == "yes" || msg.Text == "да")
                {
                    step++;
                    await client.SendTextMessageAsync(msg.Chat.Id, "Что мы будем делать?");
                    Console.WriteLine(msg.Text);
                }
                else
                {
                    await client.SendTextMessageAsync(msg.Chat.Id, "До следующих встреч");
                   step = 0;
                    inSchedule = false;
                }
            }

            //Schedule Step2
            else if (msg != null && step == 1)
            {
                Console.WriteLine("Stage with what to do");
                //await client.SendTextMessageAsync(msg.Chat.Id, "На какую дату поставить заметку?");
                WriteIn1(client, e);
                step++;
                Console.WriteLine(msg.Text);
                await client.SendTextMessageAsync(msg.Chat.Id, "На какую дату?\n(Пример записи даты: 25.05.2016, " +
                    "можно также добавить и в какое время, тогда запись выглядит вот так: 25.05.2016 18:00:56)");

            }
            //Schedule Step3
            else if (msg != null && step == 2)
            {
                Console.WriteLine(msg.Text);
              WriteIn2(client, e);
                step++;
                if (step == 3)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id, "Записано! \n" +
                            "Если хотите, то командой /clear можно очистить записи.");
                }


            }
            else if (msg != null && step == 3)
            {
                Console.WriteLine("Stage with /clear");
                if (msg.Text == "/clear")
                {
                    clear();
                    step = 0;
                    inSchedule = false;
                    await client.SendTextMessageAsync(msg.Chat.Id, "Заметки были успешно очищены!"
                   );
                }
                else
                {
                    await client.SendTextMessageAsync(msg.Chat.Id, "Покеда"
                    );
                   step = 0;
                    inSchedule = false;
                }
            }
        }
    }
}
