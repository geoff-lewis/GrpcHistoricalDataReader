using HistoricalDataDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalDataClient
{
    public class DoubleHistoricalValue : HistoricalDataValue<double>
    {
        private DoubleHistoricalValue(DataValue value, Func<DataValue, double> valueProcessor, string savedItemId, string propertyName) 
            : base(value, valueProcessor, savedItemId, propertyName)
        {
        }

        public static DoubleHistoricalValue Create(double doubleValue, string savedItemId, string propertyName) =>
             new DoubleHistoricalValue(doubleValue.ToDataValue(), HistoricalDataValues.GetDoubleValue, savedItemId, propertyName); 
    }
}
