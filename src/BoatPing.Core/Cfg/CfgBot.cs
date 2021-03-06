using System;
using System.IO;
using System.Linq;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Number;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace BoatPing.Core
{
    /// <summary>
    /// Search interval in minutes.
    /// </summary>
    public sealed class CfgInterval : ScalarEnvelope<TimeSpan>
    {
        /// <summary>
        /// Search interval in minutes.
        /// </summary>
        public CfgInterval(Uri file, Action<string> onError) : base(new ScalarOf<TimeSpan>(() =>
            {
                var time = 30;
                var lines = File.ReadAllLines(file.AbsolutePath);
                try
                {
                    if(new LengthOf(lines).Value() != 2)
                    {
                        throw new ArgumentException(
                            new Paragraph(
                                $"Invalid config: {file.AbsolutePath}",
                                "There must be 2 lines in this config.",
                                "1. token for the Telegram bot",
                                "2. time interval to search in minutes (set to 0 to exit after first search)"
                            ).AsString()
                        );  
                    }
                    int.TryParse(lines[1], out time);
                }
                catch(Exception ex)
                {
                    onError(ex.Message);
                }
                return new TimeSpan(0, time, 0);
            })
        )
        { }
    }
}
