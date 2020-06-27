using System;

namespace NitroxLauncherCli.Events
{
    public class ServerStartEventArgs : EventArgs
    {
        /// <summary>
        ///     True if server has been started in embedded mode.
        /// </summary>
        public bool Embedded { get; }

        public ServerStartEventArgs(bool embedded)
        {
            Embedded = embedded;
        }
    }
}
