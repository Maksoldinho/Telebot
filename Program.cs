using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telebot
{
    class Program
    {
        private static string cmdlist { get; set; } = "Такс такс таакс. Вот все существующие команды: \n" +
            "Команда: Yay \n" +
            "Команда: DosiaXGod \n" +
            "Команда: LOL \n " +
            "Команда: SOR (переводится как schedule of remarks) \n" +
            "Команда: Game (простейшая игра, смысл которой просто угадать загаданное программой число.) \n" +
            "NB! Все команды писать в строку так, как они показаны в листе. " +
            "Если команда начинается с большой буквы, то мы и начинаем с большой буквы";
        private static TelegramBotClient client;
        static Random rnd = new Random();
        private static int answer = rnd.Next(1,100);
        static SW ScheduleInput = new SW();
        private static ChatId chatid { get; set; } 
        
        public static bool inGame = Convert.ToBoolean(Chapter2.InGame);
       
       
        private static string token { get; set;} = "1865373748:AAFWN32-eJSQh-Psrcey95UJcLzv1M7hd9Y";
       
        static void Main(string[] args)
        {
          
            client = new TelegramBotClient(token);
            if (_Timer.Timer_HasElapsed)
            {
                Console.WriteLine("Time elapsed in Main");
                client.SendTextMessageAsync(chatid, "Время делать дела! \n" +
                    $"Запланированное действие: {ScheduleInput.zadacha}\n" +
                    $"В такое время: {ScheduleInput.date}");
                _Timer.Timer_HasElapsed = false;
            }
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            chatid = msg.Chat.Id;
           

            Console.WriteLine($"Bool inGame: {inGame}\n" +
              $"Bool inSchedule: {SW.inSchedule} \n" +
              $"Bool Timer_HasElapsed: {_Timer.Timer_HasElapsed}");
            
            if (msg != null && !inGame && !SW.inSchedule && !_Timer.Timer_HasElapsed)
            {

                Console.WriteLine($"{msg.From} от сообщение с текстом: {msg.Text} \n" +
                    $"{msg.Contact}");
               


                switch (msg.Text)
                {
                    case "/start":
                        await client.SendTextMessageAsync(chatid,
                            "Здарова, это тестовый бот, который ты можешь благополучно затестить. Создатель бота будет" +
                            "искренне рад если ты уделишь минутку внимания. Для получение списка комманд напиши в строку 'Command list'!");

                        break;
                    case "Yay":
                        var stic = await client.SendStickerAsync(
                     chatId: msg.Chat.Id,
                     sticker: "https://tgram.ru/wiki/stickers/img/Felix/gif/10.gif");
                        break;

                    case "DosiaXGod":
                        var video = await client.SendVideoAsync(
                            chatId: msg.Chat.Id,
                            video: "https://github.com/Maksoldinho/botMaksoldinho/raw/readme-edits/Dosia.mp4",
                       supportsStreaming: true);

                        break;

                    case "Command list":
                        await client.SendTextMessageAsync(
                            msg.Chat.Id, cmdlist);
                        break;

                    case "LOL":
                        var music = await client.SendAudioAsync(
                            msg.Chat,
                            "https://github.com/Maksoldinho/botMaksoldinho/raw/readme-edits/Rick%20Astley%20-%20Never%20Gonna%20Give%20You%20Up.mp3");
                        break;

                    case "Game":
                        inGame = true;
                        await client.SendTextMessageAsync(msg.Chat.Id, "You need to guess a number between 1 and 100");
                        break;

                    case "SOR":
                        Data.createFile(client, e);
                        SW.inSchedule = true;
                        await client.SendTextMessageAsync(msg.Chat.Id, "Вот ваши заметки: ");
                        SR sr = new SR();
                        sr.WriteOut(client, e);
                        await client.SendTextMessageAsync(msg.Chat.Id, "Хотите что то добавить? Если да, то напишите yes или да");
                        break;
                    case "Расскажи мне шутку":
                        var jk = new Jokes();
                        jk.LoadJoke(client, e);
                        break;

                    default:
                        await client.SendTextMessageAsync(msg.Chat.Id, msg.Text, replyMarkup: GetButtons());
                        break;
                }
            }
           
            //Game
            else if (msg != null && inGame)
            {
                var game = new Game();
                game.LowHigh(client, e);

            }
            //Schedule
            else if (msg!=null && SW.inSchedule)
            {
                ScheduleInput.Schedule(client, e);
            }
            //Timer
        

            //Schedule Step1
            //else if (msg != null && inSchedule && ScheduleInput.step == 0)
            //{
            //    Console.WriteLine("User said yes");
            //    //ScheduleInput.WriteIn1(client, e);
            //    if (msg.Text == "yes" || msg.Text == "да")
            //    {
            //        ScheduleInput.step++;
            //        await client.SendTextMessageAsync(msg.Chat.Id, "Что мы будем делать?");
            //        Console.WriteLine(msg.Text);
            //    }
            //    else
            //    {
            //        await client.SendTextMessageAsync(msg.Chat.Id, "До следующих встреч");
            //        inSchedule = false;
            //        ScheduleInput.step = 0;
            //    }
            //}

            ////Schedule Step2
            //else if (msg != null && inSchedule && ScheduleInput.step == 1)
            //{
            //    Console.WriteLine("Stage with what to do");
            //    //await client.SendTextMessageAsync(msg.Chat.Id, "На какую дату поставить заметку?");
            //    ScheduleInput.WriteIn1(client, e);
            //    ScheduleInput.step++;
            //    Console.WriteLine(msg.Text);
            //    await client.SendTextMessageAsync(msg.Chat.Id, "На какую дату?\n(Пример записи даты: 25.05.2016, " +
            //        "можно также добавить и в какое время, тогда запись выглядит вот так: 25.05.2016 18:00:56)");

            //}
            ////Schedule Step3
            //else if (msg != null && inSchedule && ScheduleInput.step == 2)
            //{
            //    Console.WriteLine(msg.Text);
            //    ScheduleInput.WriteIn2(client, e);
            //    ScheduleInput.step++;
            //    if (ScheduleInput.step == 3)
            //    {
            //        await client.SendTextMessageAsync(msg.Chat.Id, "Записано! \n" +
            //                "Если хотите, то командой /clear можно очистить записи.");
            //    }


            //}
            //else if (msg != null && inSchedule && ScheduleInput.step == 3)
            //{
            //    Console.WriteLine("Stage with /clear");
            //    if (msg.Text == "/clear")
            //    {
            //        ScheduleInput.clear();
            //        ScheduleInput.step = 0;
            //        inSchedule = false;
            //    }
            //    else
            //    {
            //        await client.SendTextMessageAsync(msg.Chat.Id, "Покеда"
            //        );
            //        ScheduleInput.step = 0;
            //        inSchedule = false;
            //    }
            //}
        }
       

     
     
        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{
                        new KeyboardButton { Text = "Yay" }, 
                        new KeyboardButton{Text = "Расскажи мне шутку"}
                    },
                    new List<KeyboardButton>{new KeyboardButton { Text = "DosiaXGod"} ,
                    new KeyboardButton{Text = "Command list"},
                    new KeyboardButton{Text = "Game"},
                    new KeyboardButton{Text = "SOR"}
                }
                    


            }
            };
            }
        //Console.WriteLine(answer);
        //try
        //{
        //    int c = Convert.ToInt32(msg.Text);
        //    //await client.SendTextMessageAsync(msg.Chat.Id, c.ToString());
        //    if (c > answer)
        //    {

        //        await client.SendTextMessageAsync(msg.Chat.Id, "It's higher than you need. Try again!");
        //    }
        //    else if (c < answer)
        //    {
        //        await client.SendTextMessageAsync(msg.Chat.Id, "It's lower than you need. Try again!");
        //    }
        //    else
        //    {
        //        inGame = false;
        //        await client.SendTextMessageAsync(msg.Chat.Id, "You win");
        //    }
        //}
        //catch (Exception)
        //{
        //    await client.SendTextMessageAsync(msg.Chat.Id, "Invalid input. Please enter only numbers");
        //}

    }
}
