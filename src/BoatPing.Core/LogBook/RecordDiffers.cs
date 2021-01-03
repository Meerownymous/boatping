using System;
using BoatPing.Core.Model;

namespace BoatPing.Core.LogBook
{
    /// <summary>
    /// Checks if two ads are identical besides their record date (which only log records have)
    /// </summary>
    public sealed class AdsEqual : AssumptionEnvelope
    {
        /// <summary>
        /// Checks if two ads are identical besides their record date (which only log records have)
        /// </summary>
        public AdsEqual(IAd left, IAd right) : base(() =>
        {
            var contentEqual = true;
            var leftContent = left.Content();
            var rightContent = right.Content();
            foreach(var key in leftContent.Keys)
            {
                if (key != "timestamp")
                {
                    if (rightContent[key] != leftContent[key])
                    {
                        contentEqual = false;
                    }
                }
            }

            return
                left.ID() == right.ID()
                && left.Price() == right.Price()
                && left.Source() == right.Source()
                && left.Url() == right.Url()
                && contentEqual;
        })
        { }
    }
}
