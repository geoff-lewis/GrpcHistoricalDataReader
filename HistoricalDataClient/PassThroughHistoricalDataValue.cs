using HistoricalDataDomain;
using Microsoft.FSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalDataClient
{
  public class PassThroughHistoricalDataValue<OutputType> : HistoricalDataValue<OutputType, OutputType>
  {
    public PassThroughHistoricalDataValue(OutputType value, Func<DataValue, FSharpOption<OutputType>> dataValueProcessor,
        string savedItemId, string propertyName) 
      : base(dataValueProcessor, x => x, savedItemId, propertyName)
    {
      Value = value;
    }
  }
}
