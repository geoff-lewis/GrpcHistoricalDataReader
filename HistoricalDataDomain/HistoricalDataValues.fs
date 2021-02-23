module HistoricalDataDomain.HistoricalDataValues

open HistoricalDataDomain
open System

let CreateDoubleDataValue doubleValue = HistoricalDataDomain.DoubleValue {Value = doubleValue}
let CreateStringDataValue stringValue = HistoricalDataDomain.StringValue {Value = stringValue}

let CreateHistoricalValue timestamp dataValue =
  {
    TimeStamp = timestamp;
    Value = dataValue;
  }

let CreateHistoricalData savedItemId proptertyName historicalValues  = 
  {
    SavedItemId = savedItemId;
    PropertyName = proptertyName;
    HistoricalValues = historicalValues;
  }
 
let GetDoubleValue dataValue =
  match dataValue with  
    |  DoubleValue v -> v.Value
    |  _ -> double 0

let GetStringValue dataValue =
  match dataValue with  
    |  StringValue v -> v.Value
    |  _ -> String.Empty

let fold (dataValue:DataValue) (doubleFunction:Func<double,'b>) (stringFunction:Func<string,'b>) =
  match dataValue with  
   |  DoubleValue v -> doubleFunction.Invoke(v.Value)
   |  StringValue v -> stringFunction.Invoke(v.Value)

  

