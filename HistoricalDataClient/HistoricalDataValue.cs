using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HistoricalDataDomain;
using Microsoft.FSharp.Core;

namespace HistoricalDataClient
{
  public class HistoricalDataValue<OutputType> : IHistoricalDataValue<OutputType>
  {
    private Func<HistoricalDataDomain.DataValue, OutputType> m_ValueProcessor;

    public HistoricalDataValue(DataValue value, Func<HistoricalDataDomain.DataValue, OutputType> valueProcessor,
      string savedItemId, string propertyName)
    {
      m_ValueProcessor = valueProcessor;
      SavedItemId = savedItemId;
      PropertyName = propertyName;
      Value = valueProcessor(value);
    }

    public OutputType Value { get; protected set; }

    public string SavedItemId { get; }

    public string PropertyName { get; }

    public IEnumerable<OpcDataValue> GetHistoricalValues()
    {
      return HistoricalValueReader.GetHistoricalData(SavedItemId, PropertyName).HistoricalValues.Select(hv => CreateDataValue(hv));
    }

    private OpcDataValue CreateDataValue(HistoricalValue hv)
    {
      var processedValue = m_ValueProcessor(hv.Value);
      return new OpcDataValue(processedValue, hv.TimeStamp);
    }

  }
}
