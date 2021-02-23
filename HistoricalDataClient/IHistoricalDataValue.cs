using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalDataClient
{
  public interface IHistoricalDataValue<T>
  {
    T Value { get; }

    string SavedItemId { get; }

    string PropertyName { get; }

    IEnumerable<DataValue> GetHistoricalValues();

  }
}
