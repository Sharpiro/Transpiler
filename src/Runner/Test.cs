using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Test
    {
        private static void clear()
        {

        }

        private void log(string message)
        {

        }

        private void log(int message)
        {

        }

        private void wait(int delay)
        {

        }

        private void TestMethod()
        {
            clear();

            log("counting down:");
            for (var i = 10; i >= 0; i--)
            {
                log(i);
                wait(1);
            }
        }
    }
}
