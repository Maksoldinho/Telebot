using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace Telebot
{

    class lul
    {
        Random rnd = new Random();
       
        public string low;
        public void GameLH(string guess)
        {
            
            int ca = rnd.Next(1, 100);
            
            int gg = Convert.ToInt32(guess);
            do
            {
                if (gg > ca)
                    low = "High";
                else if (gg < ca)
                    low = "Low";
                else
                    low = "Correct";
            } while (ca != gg);
        }
    }
}
