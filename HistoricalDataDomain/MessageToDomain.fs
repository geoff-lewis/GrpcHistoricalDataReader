module HistoricalDataDomain.MessageToDomain

open System
open Grpc.HistoricalData
open HistoricalDataDomain
open HistoricalDataDomain.HistoricalDataValues
open Google.Protobuf.Collections

let CreateDataValue (historicalValue:Grpc.HistoricalData.HistoricalDataValue) =
  match historicalValue.ValueCase with  
    |Grpc.HistoricalData.HistoricalDataValue.ValueOneofCase.DoubleValue ->  CreateDoubleDataValue historicalValue.DoubleValue |> Option.Some
    |Grpc.HistoricalData.HistoricalDataValue.ValueOneofCase.StringValue  -> CreateStringDataValue historicalValue.StringValue |> Option.Some
    |Grpc.HistoricalData.HistoricalDataValue.ValueOneofCase.None -> Option.None
    | _ -> failwith "Somebody created an enum value with an illegal value."

let CreateHistoricalValue (timestamp:Google.Protobuf.WellKnownTypes.Timestamp) dataValueOption =
   dataValueOption |> Option.map( fun dataValue->{
        TimeStamp = timestamp.ToDateTime()
        Value = dataValue 
        })

let CreateHistoricalValues (historicalValues:RepeatedField<Grpc.HistoricalData.HistoricalDataValue>) =
   historicalValues |> Seq.choose (fun historicalValue  -> CreateDataValue historicalValue |> CreateHistoricalValue historicalValue.Timestamp) |> Seq.toArray

let CreateHistoricalDataResult (historicalDataResult:Grpc.HistoricalData.HistoricalDataResponse) =
  {
    SavedItemId = historicalDataResult.SavedItemId
    PropertyName = historicalDataResult.PropertyName
    HistoricalValues = CreateHistoricalValues historicalDataResult.Values
  }



