module HistoricalDataDomain.DomainToMessage

open Grpc.HistoricalData
open Google.Protobuf.WellKnownTypes

let CreateHistoricalDataRequestMessage savedItemId propertyName = HistoricalDataRequest(SavedItemId = savedItemId, PropertyName = propertyName)

let CreateHistoricalDataValue timestamp historicalValue = 
  let historicalDataValue = HistoricalDataValue(Timestamp = Timestamp.FromDateTime(timestamp) )
  match historicalValue with
    | DoubleValue v -> historicalDataValue.DoubleValue <- v.Value
    | StringValue v -> historicalDataValue.StringValue <- v.Value
  historicalDataValue

let CreateHistoricalDataResponseMessage savedItemId propertyName (historicalValues:HistoricalValue[]) = 
  let response = Grpc.HistoricalData.HistoricalDataResponse(SavedItemId =  savedItemId, PropertyName = propertyName)
  response.Values.AddRange(historicalValues |> Seq.map (fun historicalValue -> CreateHistoricalDataValue (historicalValue.TimeStamp.ToUniversalTime()) historicalValue.Value ))
  response

