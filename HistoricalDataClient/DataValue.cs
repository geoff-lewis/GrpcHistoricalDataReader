using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalDataClient
{
  public class DataValue
  {
    public DataValue(object value, DateTime timeStamp)
    {
      Value = value;
      TimeStamp = timeStamp;
    }

    public object Value { get; }
    public DateTime TimeStamp { get; }


  }
}
