using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Telegram.Bot.Args;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Telebot
{
    class Jokes
    {
        public static int id;
        public async void LoadJoke(TelegramBotClient client, MessageEventArgs e) {
            var msg = e.Message;
            //Стыреный метод
            string LoadPage(string url)
            {
            var result = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var receiveStream = response.GetResponseStream();
                if (receiveStream != null)
                {
                    StreamReader readStream;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    result = readStream.ReadToEnd();
                    readStream.Close();
                }
                response.Close();
            }
            return result;
        }
            //Тоже все стырено, НО адаптированно
        HtmlDocument doc = new HtmlDocument();
        var pageContent = LoadPage(@"https://shortiki.com/");
        var document = new HtmlDocument();
        document.LoadHtml(pageContent);
            rand();

        HtmlNodeCollection links = document.DocumentNode.SelectNodes($"//*[@id='{id}']/div[6]");

        Console.WriteLine(id);
            
            foreach (HtmlNode link in links)
            {
                if (link.InnerText != null)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id, link.InnerText);
                }
                else if (link.InnerText == null)
                {
                    rand();
}
            }
        
            Console.ReadLine();
        }
        private static void rand()
{
    Random rnd = new Random();
    id = rnd.Next(953, 982);
}
    }
   }

