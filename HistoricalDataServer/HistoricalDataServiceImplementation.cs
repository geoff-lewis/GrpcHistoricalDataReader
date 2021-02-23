using Grpc.Core;
using Grpc.HistoricalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalDataServer
{
  public class HistoricalDataServiceImplementation : Grpc.HistoricalData.HistoricalDataService.HistoricalDataServiceBase
  {

    public override Task<HistoricalDataResponse> GetHistoricalData(HistoricalDataRequest request, ServerCallContext context)
    {
      if(request.PropertyName == "Length")
      {
        return GetDoubleResult(request.SavedItemId,request.PropertyName);
      }

      if(request.PropertyName == "Greeting")
      {
        return GetStringResult(request.SavedItemId, request.PropertyName);
      }


      return base.GetHistoricalData(request, context);
    }

    private Task<HistoricalDataResponse> GetDoubleResult(string savedItemId, string propertyName)
    {
      var random = new Random(1000);
      var startDate = DateTime.Now.AddDays(-10);

      var results = Enumerable.Range(0, 3).Select(i => HistoricalDataDomain.HistoricalDataValues.CreateHistoricalValue(
         startDate.AddDays(i),
         HistoricalDataDomain.HistoricalDataValues.CreateDoubleHistoricalDataValue(random.Next())
         )).ToArray();

      var result = HistoricalDataDomain.DomainToMessage.CreateHistoricalDataResponseMessage(savedItemId, propertyName, results);

      return Task.FromResult(result);
      
    }

    private Task<HistoricalDataResponse> GetStringResult(string savedItemId, string propertyName)
    {
      var startDate = DateTime.Now.AddDays(-10);

      var results = Enumerable.Range(0, 3).Select(i => HistoricalDataDomain.HistoricalDataValues.CreateHistoricalValue(
         startDate.AddDays(i),
         HistoricalDataDomain.HistoricalDataValues.CreateStringHistoricalDataValue("Greetings from the past, ")
         )).ToArray();

      var result = HistoricalDataDomain.DomainToMessage.CreateHistoricalDataResponseMessage(savedItemId, propertyName, results);

      return Task.FromResult(result);

    }
  }

}
