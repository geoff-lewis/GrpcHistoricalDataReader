namespace HistoricalDataDomain

open System

type DoubleDataValue =
  {
    Value : double
  }

type StringDataValue =
  {
    Value : string
  }

type DataValue = 
  | DoubleValue of DoubleDataValue
  | StringValue of StringDataValue

type HistoricalValue = 
  {
    TimeStamp : DateTime
    Value : DataValue
  }

type HistoricalData =
  {
    SavedItemId : string
    PropertyName : string
    HistoricalValues : HistoricalValue[]
  }
