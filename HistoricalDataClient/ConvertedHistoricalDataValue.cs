using HistoricalDataDomain;
using Microsoft.FSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalDataClient
{
  public class ConvertedHistoricalDataValue<InputType,OutputType> : BaseHistoricalDataValue<InputType, OutputType>
  {
    public ConvertedHistoricalDataValue(InputType value, Func<FSharpOption<HistoricalDataValue>, InputType> historicalValueProcessor, Func<InputType, OutputType> valueProcessor,
        string savedItemId, string propertyName) 
      : base(historicalValueProcessor, valueProcessor, savedItemId, propertyName)
    {
      Value = valueProcessor(value);
    }
  }
}
