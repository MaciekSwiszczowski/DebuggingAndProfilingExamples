using System;
using System.Collections.Generic;

namespace Concepts
{
    public interface IDataSource
    {
        IEnumerable<Measurement> Get(double start, double end);
    }
}
