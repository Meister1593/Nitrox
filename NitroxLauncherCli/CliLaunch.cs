using System;

namespace NitroxLauncherCli
{
    class CliLaunch
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Subnautica Nitrox");
            MainLauncher launcher = new MainLauncher();
            launcher.LaunchGame(args);
        }
    }
}
