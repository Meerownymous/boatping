using System;
using System.IO;
using System.Linq;
using Xunit.Abstractions;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Number;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace BoatPing.Core
{
    /// <summary>
    /// Price min/max from file.
    /// </summary>
    public sealed class CfgPrice : NumberEnvelope
    {
        /// <summary>
        /// Price min/max from file.
        /// </summary>
        public CfgPrice(Uri file, bool min, Action<string> onError) : base(new ScalarOf<int>(() =>
            {
                var parameter = min ? "min-price=" : "max-price=";
                
                int result = min ? 0 : int.MaxValue;
                var lines =
                    new Filtered<string>(
                        line => line.ToLower().StartsWith(parameter),
                        File.ReadAllLines(file.AbsolutePath)
                    );
                try
                {
                    var value =
                        new FirstOf<string>(
                            lines
                        )
                        .Value()
                        .Trim()
                        .ToLower()
                        .Replace(parameter, "");

                    if(!int.TryParse(value, out result))
                    {
                        onError($"{value} is not a valid {parameter}. It must be a number.");
                    }
                }
                catch(Exception ex)
                {
                    //onError(ex.Message);
                }
                return result;
            })
        )
        { }
    }
}
