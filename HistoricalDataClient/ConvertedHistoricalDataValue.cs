using HistoricalDataDomain;
using Microsoft.FSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalDataClient
{
  public class ConvertedHistoricalDataValue<InputType,OutputType> : HistoricalDataValue<InputType, OutputType>
  {
    public ConvertedHistoricalDataValue(InputType value, Func<FSharpOption<DataValue>, InputType> dataValueProcessor, Func<InputType, OutputType> valueProcessor,
        string savedItemId, string propertyName) 
      : base(dataValueProcessor, valueProcessor, savedItemId, propertyName)
    {
      Value = valueProcessor(value);
    }
  }
}
