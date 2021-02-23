using HistoricalDataDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalDataClient
{
    public class StringHistoricalValue : HistoricalDataValue<string>
    {
        private StringHistoricalValue(DataValue value, Func<DataValue, string> valueProcessor, string savedItemId, string propertyName) 
            : base(value, valueProcessor, savedItemId, propertyName)
        {
        }

        public static StringHistoricalValue Create(string stringValue, string savedItemId, string propertyName) =>
             Create(stringValue, HistoricalDataValues.GetStringValue, savedItemId, propertyName);

        public static StringHistoricalValue Create(string stringValue, Func<DataValue, string> valueProcessor, string savedItemId, string propertyName) =>
            Create(stringValue.ToDataValue(), valueProcessor, savedItemId, propertyName);

        public static StringHistoricalValue Create(DataValue dataValue, Func<DataValue, string> valueProcessor, string savedItemId, string propertyName) =>
            new StringHistoricalValue(dataValue, valueProcessor, savedItemId, propertyName);    
    }
}
