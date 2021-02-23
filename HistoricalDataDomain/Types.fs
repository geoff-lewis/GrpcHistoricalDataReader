namespace HistoricalDataDomain

open System

type DoubleHistoricalDataValue =
  {
    Value : double
  }

type StringHistoricalDataValue =
  {
    Value : string
  }

type HistoricalDataValue = 
  | DoubleValue of DoubleHistoricalDataValue
  | StringValue of StringHistoricalDataValue

type HistoricalValue = 
  {
    TimeStamp : DateTime
    Value : HistoricalDataValue
  }

type HistoricalData =
  {
    SavedItemId : string
    PropertyName : string
    HistoricalValues : HistoricalValue[]
  }
