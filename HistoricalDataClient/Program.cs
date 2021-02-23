using System;
using System.Linq;
using Grpc.Core;
using Grpc.HistoricalData;
using HistoricalDataDomain;
using Microsoft.FSharp.Core;

namespace HistoricalDataClient
{
  class Program
  {

    static void Main(string[] args)
    {

      var passThroughdoubleHistoricalValue = DoubleHistoricalValue.Create(123.0, "TestSavedItem1", "Length");
      var passThroughStringHistoricalValue = new HistoricalDataValue<string>("Hello".ToDataValue(), HistoricalDataValues.GetStringValue, "TestSavedItem1", "Greeting");

      var convertedDoubleScaledHistoricalValue = new HistoricalDataValue<double>(123.0.ToDataValue(), ScaleValue, "TestSavedItem1", "Length");
      var convertedDoubleToStringHistoricalValue = new HistoricalDataValue<string>(123.0.ToDataValue(), WrapValue, "TestSavedItem1", "Length");
      var convertedStringToSuffixHistoricalValue = new HistoricalDataValue< string>("Hello".ToDataValue(), AddSuffix, "TestSavedItem1", "Greeting");

      PrintInitial(nameof(passThroughdoubleHistoricalValue), passThroughdoubleHistoricalValue);
      PrintInitial(nameof(passThroughStringHistoricalValue), passThroughStringHistoricalValue);
      PrintInitial(nameof(convertedDoubleScaledHistoricalValue), convertedDoubleScaledHistoricalValue);
      PrintInitial(nameof(convertedDoubleToStringHistoricalValue), convertedDoubleToStringHistoricalValue);
      PrintInitial(nameof(convertedStringToSuffixHistoricalValue), convertedStringToSuffixHistoricalValue);


      ReadHistoricalValue(nameof(passThroughdoubleHistoricalValue), passThroughdoubleHistoricalValue);
      ReadHistoricalValue(nameof(passThroughStringHistoricalValue), passThroughStringHistoricalValue);
      ReadHistoricalValue(nameof(convertedDoubleScaledHistoricalValue), convertedDoubleScaledHistoricalValue);
      ReadHistoricalValue(nameof(convertedDoubleToStringHistoricalValue), convertedDoubleToStringHistoricalValue);
      ReadHistoricalValue(nameof(convertedStringToSuffixHistoricalValue), convertedStringToSuffixHistoricalValue);

      Console.WriteLine("Press any key to exit");
      Console.ReadLine();

      //var historicalDataResult = MessageToDomain.CreateHistoricalDataResult(historicalDataResponseMessage);

      //var parsedValues = historicalDataResult.HistoricalValues.Select(hdv => hdv.Value.Value);
      //these are all the values but they are a HistoricalDataValue
      //type HistoricalDataValue =
      // |DoubleValue of DoubleHistoricalDataValue
      // | StringValue of StringHistoricalDataValue

      //do I need a function  type GetDouble DoubleHistoricalDataValue -> Double ?

    }

    private static void PrintInitial<T>(string variableName, IHistoricalDataValue<T> historicalValue)
    {
      Console.WriteLine($"Initial value for {variableName} : {historicalValue.Value}");
    }


    private static void ReadHistoricalValue<T>(string variableName, IHistoricalDataValue<T> historicalValue)
    {
      Console.WriteLine();
      Console.WriteLine($"Reading historical values for {historicalValue.SavedItemId} - {historicalValue.PropertyName}");
      var result = historicalValue.GetHistoricalValues();
      result.ToList().ForEach(value => Console.WriteLine($"{value.Value} @ {value.TimeStamp.ToShortDateString()}"));
    }


    private static double ScaleValue(DataValue dataValue)
    {
        return HistoricalDataValues.GetDoubleValue(dataValue) * 100;
    }

    private static string WrapValue(DataValue dataValue)
    {
        return $"I was a double {HistoricalDataValues.GetDoubleValue(dataValue)}";
    }

    private static string AddSuffix(DataValue dataValue)
    {
        return $"{HistoricalDataValues.GetStringValue(dataValue)}, Geoff";
    }


    }



}
