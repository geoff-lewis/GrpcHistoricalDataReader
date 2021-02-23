using Grpc.Core;
using Grpc.HistoricalData;
using HistoricalDataDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalDataClient
{
  public class HistoricalValueReader
  {
    private static string m_DataSource = "127.0.0.1:30051";

    public static HistoricalDataDomain.HistoricalData GetHistoricalData(string savedItemId, string propertName)
    {
      Channel channel = new Channel(m_DataSource, ChannelCredentials.Insecure);

      var client = new HistoricalDataService.HistoricalDataServiceClient(channel);

      var request = DomainToMessage.CreateHistoricalDataRequestMessage(savedItemId, propertName);

      var historicalDataResponseMessage = client.GetHistoricalData(request);

      return MessageToDomain.CreateHistoricalDataResult(historicalDataResponseMessage);
    }

  }
}
