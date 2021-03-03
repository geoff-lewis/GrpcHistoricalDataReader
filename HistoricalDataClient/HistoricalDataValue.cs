using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HistoricalDataDomain;
using Microsoft.FSharp.Core;

namespace HistoricalDataClient
{
    public abstract class HistoricalDataValue<InputType, OutputType> : IHistoricalDataValue<OutputType>
    {
        private readonly Func<DataValue, FSharpOption<InputType>> m_DataValueProcessor;
        private readonly Func<InputType, OutputType> m_ValueProcessor;

        protected HistoricalDataValue(Func<DataValue, FSharpOption<InputType>> dataValueProcessor,
            Func<InputType, OutputType> valueProcessor,
            string savedItemId, string propertyName)
        {
            m_DataValueProcessor = dataValueProcessor;
            m_ValueProcessor = valueProcessor;
            SavedItemId = savedItemId;
            PropertyName = propertyName;
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
            var processedValue = m_ValueProcessor(m_DataValueProcessor(hv.Value).Value);
            return new OpcDataValue(processedValue, hv.TimeStamp);
        }

    }
}
