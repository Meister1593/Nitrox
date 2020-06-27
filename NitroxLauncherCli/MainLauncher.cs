using System;
using System.Collections.Generic;
using System.IO;
using NitroxModel.Logger;

namespace NitroxLauncherCli
{
    class MainLauncher
    {
        private readonly LauncherLogic logic = new LauncherLogic();
        private readonly Dictionary<string, bool> arguments = new Dictionary<string, bool>()
        {
            {"-multi", true },
            {"-single", false },
            {"-server", false }
        };
        public void LaunchGame(string[] args)
        {
            ParseCmdlineArgs(args);
            if(logic.SubnauticaPath == null)
            {
                Console.WriteLine("No game path specified.");
                Environment.Exit(1);
            }
            arguments.TryGetValue("-server", out bool isServer);
            arguments.TryGetValue("-single", out bool isSingle);
            arguments.TryGetValue("-multi", out bool isMultiplayer);
            if (isServer)
            {
                Console.WriteLine("Launching Server");
                logic.StartServer(true);
            }
            else if (isSingle)
            {
                Console.WriteLine("Launching Singleplayer");
                logic.StartSingleplayer();
            }
            else if (isMultiplayer)
            {
                Console.WriteLine("Launching Multiplayer");
                logic.StartMultiplayer();
            }
            else
            {
                Console.WriteLine("No launch method, exiting.");
                Environment.Exit(0);
            }
        }

        private void ParseCmdlineArgs(string[] args)
        {
            foreach (string argument in args)
            {
                if (argument.Contains("-"))
                {
                    if (arguments.TryGetValue(argument, out bool _))
                    {
                        arguments[argument] = true;
                        Console.WriteLine("Argument found: " + argument);
                    }
                } else if (logic.SubnauticaPath == null && (argument.Contains(":") || argument.Contains("/")))
                {
                    StartupPrep(argument);
                }
            }
        }

        void StartupPrep(string gameFolderPath)
        {
            Log.Setup();

            // Error if running from a temporary directory because Nitrox Launcher won't be able to write files directly to zip/rar
            // Tools like WinRAR do this to support running EXE files while it's still zipped.
            if (Directory.GetCurrentDirectory().StartsWith(Path.GetTempPath(), StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid working directory: Nitrox launcher should not be executed from a temporary directory. Install Nitrox launcher properly by extracting ALL files and moving these to a dedicated location on your PC.");
                Environment.Exit(1);
            }
            logic.SetTargetedSubnauticaPath(gameFolderPath);
        }
    }
}
