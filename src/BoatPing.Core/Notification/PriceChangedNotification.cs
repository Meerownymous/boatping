using System;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.Notification
{
    /// <summary>
    /// Notification about a price change.
    /// </summary>
    public class PriceChangedNotification : INotification
    {
        private readonly IAd newAd;
        private readonly IAd oldAd;

        /// <summary>
        /// Notification about a price change.
        /// </summary>
        public PriceChangedNotification(IAd newAd, IAd oldAd)
        {
            this.newAd = newAd;
            this.oldAd = oldAd;
        }

        public IAd Ad()
        {
            return newAd;
        }

        public string Event()
        {
            return "price-change";
        }

        public string Title()
        {
            return
                new FallbackMap(
                    newAd.Content(),
                    notFound => newAd.ID()
                )["title"];
        }

        public string Content()
        {
            return new PriceChangedText(this.newAd, this.oldAd).AsString();
        }
    }
}
