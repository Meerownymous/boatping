using System;
using System.IO;
using BoatPing.Core.Notification;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Text;

namespace BoatPing.Core
{
    /// <summary>
    /// Notifications to files.
    /// </summary>
    public sealed class FileNotifications : INotifications
    {
        private FileHive xive;

        /// <summary>
        /// Notifications to files.
        /// </summary>
        public FileNotifications(string path)
        {
            this.xive = new FileHive(path, "notifications");
        }

        public void Post(INotification notification)
        {
            this.xive
                .Comb(string.Join("_", notification.Ad().Source().Split(Path.GetInvalidFileNameChars())))
                .Cell(Filename(notification))
                .Update(new InputOf(Text(notification)));
        }

        private string Text(INotification notification)
        {
            return new NewBoatText(notification.Ad()).AsString();
        }

        private string Filename(INotification notification)
        {
            return new NotificationFilename(notification).AsString();
        }
    }
}
