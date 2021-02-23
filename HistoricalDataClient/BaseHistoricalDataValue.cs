using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HistoricalDataDomain;
using Microsoft.FSharp.Core;

namespace HistoricalDataClient
{
  public abstract class BaseHistoricalDataValue<InputType, OutputType> : IHistoricalDataValue<OutputType>
  {
    private Func<FSharpOption<HistoricalDataDomain.HistoricalDataValue>, InputType> m_HistoricalValueProcessor;
    private Func<InputType, OutputType> m_ValueProcessor;

    protected BaseHistoricalDataValue(Func<FSharpOption<HistoricalDataValue>, InputType> historicalValueProcessor,
      Func<InputType, OutputType> valueProcessor,
      string savedItemId, string propertyName)
    {
      m_HistoricalValueProcessor = historicalValueProcessor;
      m_ValueProcessor = valueProcessor;
      SavedItemId = savedItemId;
      PropertyName = propertyName;
    }

    public OutputType Value { get; protected set; }

    public string SavedItemId { get; }

    public string PropertyName { get; }

    public IEnumerable<DataValue> GetHistoricalValues()
    {
      return HistoricalValueReader.GetHistoricalData(SavedItemId, PropertyName).HistoricalValues.Select(hv => CreateDataValue(hv));
    }

    private DataValue CreateDataValue(HistoricalValue hv)
    {
      var processedValue = m_ValueProcessor(m_HistoricalValueProcessor(hv.Value));
      return new DataValue(processedValue, hv.TimeStamp);
    }

  }
}
