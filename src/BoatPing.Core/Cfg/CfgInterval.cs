using System;
using System.IO;
using System.Linq;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace BoatPing.Core
{
    /// <summary>
    /// Bot auth token from file.
    /// </summary>
    public sealed class CfgBot : TextEnvelope
    {
        /// <summary>
        /// Bot auth token from file.
        /// </summary>
        public CfgBot(Uri file, Action<string> onError) : base(() =>
            {
                var result = String.Empty;
                var lines =
                    new Filtered<string>(
                        line => !String.IsNullOrEmpty(line),
                        File.ReadAllLines(file.AbsolutePath)
                    );
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
                    result = new FirstOf<string>(lines).Value().Trim();
                }
                catch(Exception ex)
                {
                    onError(ex.Message);
                }
                return result;
            },
            false
        )
        { }
    }
}
