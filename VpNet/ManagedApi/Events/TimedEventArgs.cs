using System;
using System.ComponentModel;

namespace VpNet
{
    /// <summary>
    ///     Provides an additional timestamp as an event argument.
    /// </summary>
    public abstract class TimedEventArgs : EventArgs
    {
        /// <summary>
        ///     Gets the date and time at which this event was fired.
        /// </summary>
        /// <value>The date and time at which this event was fired.</value>
        public DateTimeOffset CreationTimestamp { get; } = DateTimeOffset.Now;
    }
}