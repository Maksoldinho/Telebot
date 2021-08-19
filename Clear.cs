using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telebot
{
    class Clear : Data
    {
        public void clear()
        {
            TextWriter tw = new StreamWriter(filepath, false);
            tw.Write(string.Empty);
            tw.WriteLine("");
            tw.Close();
        }
    }
}
