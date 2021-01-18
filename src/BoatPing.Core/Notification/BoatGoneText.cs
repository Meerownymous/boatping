using System;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Notification
{
    /// <summary>
    /// Text for a notification about a boat which left the market.
    /// </summary>
    public class BoatGoneText : TextEnvelope
    {
        /// <summary>
        /// Text for a notification about a boat which left the market.
        /// </summary>
        public BoatGoneText(IAd oldAd) : base(() =>
        {
            var emoji = "💨💨  👀";
            var content =
                new FallbackMap(
                    oldAd.Content(),
                    notFound => $"?{notFound}?"
                );
            var text =
                new Paragraph(
                    $"{emoji} {content["title"]}",
                    $"💶{oldAd.Price()}€",
                    $"🗺{content["country"]}",
                    $"🔗{oldAd.Url()}"
                ).AsString();

            return text;
        },
            false
        )
        { }
    }
}
