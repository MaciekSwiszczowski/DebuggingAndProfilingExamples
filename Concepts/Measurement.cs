using System.Diagnostics;

namespace Concepts
{
    [DebuggerDisplay("Time: {Time}, Value: {Value}")]
    public class Measurement
    {
        public double Time { get; set; }
        public double Value { get; set; }
    }
}