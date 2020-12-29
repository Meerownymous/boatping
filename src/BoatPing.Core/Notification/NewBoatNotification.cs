using System;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.Notification
{
    /// <summary>
    /// Notification about a new boat.
    /// </summary>
    public class NewBoatNotification : INotification
    {
        private readonly IAd ad;

        /// <summary>
        /// Notification about a new boat.
        /// </summary>
        public NewBoatNotification(IAd ad)
        {
            this.ad = ad;
        }

        public IAd Ad()
        {
            return ad;
        }

        public string Event()
        {
            return "new-boat";
        }

        public string Title()
        {
            return new FallbackMap(ad.Content(), notFound => ad.ID())["title"];
        }
    }
}
