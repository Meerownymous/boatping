using System;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Notification
{
    /// <summary>
    /// Text for a notification about a prive change.
    /// </summary>
    public class PriceChangedText : TextEnvelope
    {
        /// <summary>
        /// Text for a notification about a prive change.
        /// </summary>
        public PriceChangedText(IAd newAd, IAd oldAd) : base(() =>
        {
            var emoji = newAd.Price() < oldAd.Price() ? "📉💰👀" : "📈💰👀";
            var content =
                new FallbackMap(
                    newAd.Content(),
                    notFound => $"?{notFound}?"
                );
            var text =
                new Paragraph(
                    $"{emoji} {content["title"]}",
                    $"💶 {oldAd.Price()}€ → {newAd.Price()}€",
                    $"🗺 {content["country"]}",
                    $"{newAd.Url()}"
                ).AsString();

            return text;
        },
            false
        )
        { }
    }
}
