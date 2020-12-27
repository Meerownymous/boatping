using System;
using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;

namespace BoatPing.Core
{
    /// <summary>
    /// Envelope for assumptions.
    /// </summary>
    public abstract class AssumptionEnvelope : IAssumption
    {
        private readonly IScalar<bool> assumption;

        /// <summary>
        /// Envelope for assumptions.
        /// </summary>
        public AssumptionEnvelope(Func<bool> assumption)
        {
            this.assumption = new ScalarOf<bool>(assumption);
        }

        public bool IsFalse()
        {
            return !this.assumption.Value();
        }

        public bool IsTrue()
        {
            return this.assumption.Value();
        }

        public static implicit operator bool(AssumptionEnvelope assumption)
        {
            return assumption.IsTrue();
        }
    }
}
