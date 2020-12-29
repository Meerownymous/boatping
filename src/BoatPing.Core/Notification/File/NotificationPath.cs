using System;
using System.IO;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Notification
{
    /// <summary>
    /// Relative path at which a file notification is stored.
    /// </summary>
    public sealed class NotificationFilename : TextEnvelope
    {
        /// <summary>
        /// Relative path at which a file notification is stored.
        /// </summary>
        public NotificationFilename(INotification notification) : base(() =>
            $"{DateTime.Now.ToString("yyyymmdd_hhMMss")}_{string.Join("_", notification.Title().Split(Path.GetInvalidFileNameChars()))}.txt",
            false
        )
        { }
    }
}
