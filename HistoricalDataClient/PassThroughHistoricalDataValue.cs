using HistoricalDataDomain;
using Microsoft.FSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalDataClient
{
  public class PassThroughHistoricalDataValue<OutputType> : BaseHistoricalDataValue<OutputType, OutputType>
  {
    public PassThroughHistoricalDataValue(OutputType value, Func<FSharpOption<HistoricalDataValue>, OutputType> historicalValueProcessor,
        string savedItemId, string propertyName) 
      : base(historicalValueProcessor, x => x, savedItemId, propertyName)
    {
      Value = value;
    }
  }
}
