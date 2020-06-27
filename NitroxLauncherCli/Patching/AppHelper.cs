﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using NitroxModel.Logger;

namespace NitroxLauncherCli.Patching
{
    public class AppHelper
    {
        public static string ProgramFileDirectory = Environment.ExpandEnvironmentVariables("%ProgramW6432%");

        public static bool IsAppRunningInAdmin()
        {
            WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RestartAsAdmin()
        {
            if (!IsAppRunningInAdmin())
            {
                    try
                    {
                        // Setting up start info of the new process of the same application
                        ProcessStartInfo processStartInfo = new ProcessStartInfo(Assembly.GetEntryAssembly().CodeBase);

                        // Using operating shell and setting the ProcessStartInfo.Verb to “runas” will let it run as admin
                        processStartInfo.UseShellExecute = true;
                        processStartInfo.Verb = "runas";

                        // Start the application as new process
                        Process.Start(processStartInfo);
                        Environment.Exit(1);
                    }
                    catch (Exception)
                    {
                        Log.Error("Error while trying to instance an admin processus of the launcher, aborting");
                    }

                //We might exit the application if the user says no ?
            }
            else
            {
                Log.Info("Can't restart the launcher as admin, we already have the perms");
            }
        }
    }

}
