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
 
let TryGetDoubleValue dataValue  =
  match dataValue with  
    |  DoubleValue v -> v.Value |> Some
    |  _ -> None

let TryGetStringValue dataValue =
  match dataValue with  
    |  StringValue v -> v.Value |> Some
    |  _ -> None

let fold (dataValue:DataValue) (doubleFunction:Func<double,'b>) (stringFunction:Func<string,'b>) =
  match dataValue with  
   |  DoubleValue v -> doubleFunction.Invoke(v.Value)
   |  StringValue v -> stringFunction.Invoke(v.Value)

let foldF doubleFunction stringFunction (dataValue:DataValue) =
  match dataValue with  
   |  DoubleValue v -> doubleFunction v
   |  StringValue v -> stringFunction v

//let GetDoubleValue defaultValue dataValue =
//  foldF (fun dV -> dV.Value |> Some) (fun dV -> None) dataValue

//let TryCast (input:'b) : 'a option =  if (input :? 'a) then input :?> 'a |> Some else None

let TryGetOf dataValue : 'a option =
    let objectValue = match dataValue with
                        |DoubleValue v -> v.Value :> Object 
                        |StringValue v -> v.Value :> Object 
    if (objectValue :? 'a) then objectValue :?> 'a |> Some else None

//let TryGetT dataValue : 'a option =
//    match dataValue with
//    |DoubleValue v when (v.Value :? 'a) -> Option.Some(v.Value :? 'a) 
//    |StringValue v when (v.Value :? 'a) -> Option.Some(v.Value :? 'a)
//    | _ -> None