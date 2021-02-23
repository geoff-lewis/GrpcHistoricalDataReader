module HistoricalDataDomain.MessageToDomain

open System
open Grpc.HistoricalData
open HistoricalDataDomain
open HistoricalDataDomain.HistoricalDataValues
open Google.Protobuf.Collections

let CreateHistoricalDataValue (historicalValue:Grpc.HistoricalData.HistoricalDataValue) =
  match historicalValue.ValueCase with  
    |Grpc.HistoricalData.HistoricalDataValue.ValueOneofCase.DoubleValue ->  CreateDoubleHistoricalDataValue historicalValue.DoubleValue |> Option.Some
    |Grpc.HistoricalData.HistoricalDataValue.ValueOneofCase.StringValue  -> CreateStringHistoricalDataValue historicalValue.StringValue |> Option.Some
    |_ -> Option.None

let CreateHistoricalValue (timestamp:Google.Protobuf.WellKnownTypes.Timestamp) (historicalDataValue:HistoricalDataValue option) =
    match historicalDataValue with
      |Some x -> {
                  TimeStamp = timestamp.ToDateTime()
                  Value = historicalDataValue.Value 
                  } |> Option.Some
      | _ -> Option.None


let CreateHistoricalValues (historicalValues:RepeatedField<Grpc.HistoricalData.HistoricalDataValue>) =
   historicalValues |> Seq.choose (fun historicalValue  -> CreateHistoricalDataValue historicalValue |> CreateHistoricalValue historicalValue.Timestamp) |> Seq.toArray

let CreateHistoricalDataResult (historicalDataResult:Grpc.HistoricalData.HistoricalDataResponse) =
  {
    SavedItemId = historicalDataResult.SavedItemId
    PropertyName = historicalDataResult.PropertyName
    HistoricalValues = CreateHistoricalValues historicalDataResult.Values
  }


let GetDoubleValue (optionHistoricalDataValue:HistoricalDataValue option) =
  match optionHistoricalDataValue with  
  | Some x -> 
    match x with  
      |  DoubleValue v -> v.Value
      |  _ -> double 0
  | None -> double 0


let GetStringValue (optionHistoricalDataValue:HistoricalDataValue option) =
  match optionHistoricalDataValue with  
  | Some x -> 
    match x with  
      |  StringValue v -> v.Value
      |  _ -> String.Empty
  | None -> String.Empty


let fold (optionHistoricalDataValue:HistoricalDataValue) (doubleFunction:Func<double,'b>) (stringFunction:Func<string,'b>) =
  match optionHistoricalDataValue with  
   |  DoubleValue v -> doubleFunction.Invoke(v.Value)
   |  StringValue v -> stringFunction.Invoke(v.Value)

