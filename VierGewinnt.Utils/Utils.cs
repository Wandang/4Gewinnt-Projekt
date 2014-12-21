using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Utils
{
    public static class Utils
    {
        /// <summary>
        /// Init all our Utils that need a init
        /// </summary>
        public static void Init()
        {
            Arguments.ArgumentHandler.Init(); //Init this first so we can register everything else here

            Logger.Logger.Init();
        }
    }
}
