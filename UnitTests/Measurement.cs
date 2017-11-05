using System.Diagnostics;

namespace UnitTests
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Measurement
    {
        public double Time { get; set; }
        public double Value { get; set; }

        private string DebuggerDisplay => string.Format("Time: {0:000000}, Value: {1}", Time, Value > 1000 ? "big" : "small");
    }


}