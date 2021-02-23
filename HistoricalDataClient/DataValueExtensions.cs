using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalDataClient
{
    public static class DataValueExtensions
    {

        public static HistoricalDataDomain.DataValue ToDataValue(this double doubleValue)
        {
            return HistoricalDataDomain.HistoricalDataValues.CreateDoubleDataValue(doubleValue);
        }

        public static HistoricalDataDomain.DataValue ToDataValue(this string stringValue)
        {
            return HistoricalDataDomain.HistoricalDataValues.CreateStringDataValue(stringValue);
        }

    }
}
