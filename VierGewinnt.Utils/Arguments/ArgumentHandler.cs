using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace VierGewinnt.Utils.Arguments
{
    public static class ArgumentHandler
    {
        /// <summary>
        /// Callback Delegates that will be called when a Argument is passed
        /// </summary>
        /// <param name="args">Arguments that are Aligned with the Argument Switch</param>
        public delegate void ArgumentCallback(string args);

        /// <summary>
        /// List of all registered Arguments
        /// </summary>
        private static readonly Dictionary<string, List<ArgumentCallback>> Arguments = new Dictionary<string, List<ArgumentCallback>>();

        /// <summary>
        /// Parameters of the Arguments
        /// </summary>
        private static Dictionary<string, string> _args = new Dictionary<string, string>();

        /// <summary>
        /// Register a Callback for a ArgumentSwitch
        /// </summary>
        /// <param name="Switch">Switch to register</param>
        /// <param name="callback">Callback to call when Argument is present</param>
        public static void Register(string Switch, ArgumentCallback callback)
        {
            Register(Switch, callback, false);
        }

        /// <summary>
        /// Register a Callback for a ArgumentSwitch
        /// </summary>
        /// <param name="Switch">Switch to register</param>
        /// <param name="callback">Callback to call when Argument is present</param>
        /// <param name="instante"></param>
        public static void Register(string Switch, ArgumentCallback callback, bool instante)
        {
            if (!Arguments.ContainsKey(Switch)) return;
            
            if (!instante)
            {
                Arguments[Switch].Add(callback);
            }
            else
            {
                callback(_args[Switch]);
            }
        }

        /// <summary>
        /// Init the ArgumentHandler with the CommandLine Arguments
        /// </summary>
        /// <param name="args">Comandline Arguments</param>
        public static void Init()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-"))
                    {
                        Arguments.Add(args[i].Replace("-", ""), new List<ArgumentCallback>());
                        _args.Add(args[i].Replace("-", ""), (args.Length > i + 1 && !args[i + 1].StartsWith("-")) ? args[i + 1] : "");

                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Runs the ArgumentHandler
        /// </summary>
        public static void Run()
        {
            foreach (KeyValuePair<string, List<ArgumentCallback>> pair in Arguments)
            {
                Debug.WriteLine(pair.Value.Count);

                foreach (ArgumentCallback del in pair.Value)
                {
                    del(_args[pair.Key]);
                }
            }
        }
    }
}
