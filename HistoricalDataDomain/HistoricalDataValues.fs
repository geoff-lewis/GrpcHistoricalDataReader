module HistoricalDataDomain.HistoricalDataValues

open HistoricalDataDomain

let CreateDoubleHistoricalDataValue doubleValue = HistoricalDataDomain.DoubleValue {Value = doubleValue}
let CreateStringHistoricalDataValue stringValue = HistoricalDataDomain.StringValue {Value = stringValue}

let CreateHistoricalValue timestamp historicalValue =
  {
    TimeStamp = timestamp;
    Value = historicalValue;
  }

let CreateHistoricalDataValue savedItemId proptertyName historicalValues  = 
  {
    SavedItemId = savedItemId;
    PropertyName = proptertyName;
    HistoricalValues = historicalValues;
  }
 
 

  

