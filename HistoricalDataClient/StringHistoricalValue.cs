using HistoricalDataDomain;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.FSharp.Core;

namespace HistoricalDataClient
{
    public class StringHistoricalValue : PassThroughHistoricalDataValue<string>
    {
        private StringHistoricalValue(string value, Func<DataValue, FSharpOption<string>> valueProcessor, string savedItemId, string propertyName) 
            : base(value, valueProcessor, savedItemId, propertyName)
        {
        }

        public static StringHistoricalValue Create(string stringValue, string savedItemId, string propertyName) =>
             new StringHistoricalValue(stringValue, HistoricalDataValues.TryGetStringValue, savedItemId, propertyName);

    }
}
