﻿namespace VpNet
{
    /// <summary>
    ///     Represents a class which contains a configuration for <see cref="VirtualParadiseClient" />.
    /// </summary>
    public class VirtualParadiseClientConfiguration
    {
        /// <summary>
        ///     Gets or sets the name of the client application.
        /// </summary>
        /// <value>The name of the client application.</value>
        public string ApplicationName { get; set; }

        /// <summary>
        ///     Gets or sets the version of the client application.
        /// </summary>
        /// <value>The version of the client application.</value>
        public string ApplicationVersion { get; set; }

        /// <summary>
        ///     Gets or sets the name of the bot as it appears in-world.
        /// </summary>
        /// <value>The name of the bot as it appears in-world.</value>
        public string BotName { get; set; }

        /// <summary>
        ///     Gets or sets the username with which the instance should log in.
        /// </summary>
        /// <value>The username with which the instance should log in.</value>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the world into which this instance should enter.
        /// </summary>
        /// <value>The world into which this instance should enter.</value>
        public World World { get; set; }
    }
}
