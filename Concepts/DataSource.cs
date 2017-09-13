using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Concepts
{
    [Export(typeof(IDataSource))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataSource : IDataSource
    {
        private readonly double SingleTickTimeSpan = 1;

        private static readonly Random Randomizer = new Random();
        private Measurement _previousMeasurement = new Measurement();


        private Measurement CalculateNext()
        {
            var nextValue = 0.0;

            for (var j = 0; j < 10; j++)
            {
                nextValue += (Randomizer.NextDouble() - 0.5) * 0.1;
            }

            var newMeasurement = new Measurement
            {
                Time = _previousMeasurement.Time + 1,
                Value = _previousMeasurement.Value + nextValue
            };

            _previousMeasurement = newMeasurement;

            return _previousMeasurement;
        }

        public IEnumerable<Measurement> Get(double start, double end)
        {
            if (end < start)
                yield break;

            _previousMeasurement = new Measurement();

            var currentDate = start;

            while (Math.Abs(currentDate - end) > 0.1)
            {
                yield return CalculateNext();
                currentDate += SingleTickTimeSpan;
            }

        }
    }
}