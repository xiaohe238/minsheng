using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinShengQingGou
{
    class MinDelegate
    {
        string str = DateTime.Now.ToString("dddd");
        public delegate bool WedDelegate(string str);

        private bool Wednesday(string str)
        {
            return this .str == "星期三";
        }

        

      
    }
}
