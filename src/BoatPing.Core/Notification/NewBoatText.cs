using System;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Notification
{
    /// <summary>
    /// Text for a notification about a new boat.
    /// </summary>
    public class NewBoatText : TextEnvelope
    {
        /// <summary>
        /// Text for a notification about a new boat.
        /// </summary>
        public NewBoatText(IAd ad) : base(() =>
            {
                var emoji = "🚨⛵️👀";
                var content =
                    new FallbackMap(
                        ad.Content(),
                        notFound => $"?{notFound}?"
                    );
                var text =
                    new Paragraph(
                        $"{emoji} {content["title"]}",
                        $"💶{ad.Price()}€",
                        $"🗺{content["country"]}",
                        $"🔗{ad.Url()}"
                    ).AsString();

                return text;
            },
            false
        )
        { }
    }
}
