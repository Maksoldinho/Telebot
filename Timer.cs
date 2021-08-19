using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Telebot
{

      class _Timer
    {
        private static Timer timer;
        private static int Hours { get; set; }
        private static int Minutes { get; set; }
        private static int Seconds { get; set; }
        private static int year { get; set; }
        private static int Months { get; set; }
        private static int Days { get; set; }
        public static bool Timer_HasElapsed;
        //public async void Total_Start_Timer(MessageEventArgs e)
        //{
        //    if (!Timer_HasElapsed)
        //    {

        //    }
        //}
        public static void schedule_Timer_Start(int Year, int Month, int Day, int hours, int minutes, int seconds)
        {
            var tim = new _Timer();
            Console.WriteLine(_Timer.Timer_HasElapsed+ "in schedule_Timer_Start");
            year = Year;
            Months = Month;
            Days = Day;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;

            DateTime nowTime = DateTime.Now;
            DateTime scheduledTime = new DateTime(Year, Month, Day, hours, minutes, seconds, 0); //Specify your scheduled time HH,MM,SS [8am and 42 minutes]

            if (nowTime > scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }

            double tickTime = (scheduledTime - nowTime).TotalMilliseconds;
            timer = new Timer(tickTime);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }
        public static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            
            _Timer.Timer_HasElapsed = true;
            Console.WriteLine(_Timer.Timer_HasElapsed + "in timer_Elapsed");
            Console.WriteLine("### Timer Stopped ### \n");
            timer.Stop();
            Console.WriteLine("### Scheduled Task Started ### \n\n");
            Console.WriteLine("Hello World!!! - Performing scheduled task\n");
            Console.WriteLine("### Task Finished ### \n\n");
            Console.WriteLine(_Timer.Timer_HasElapsed + "in timer_Elapsed");

        }
    }
}
