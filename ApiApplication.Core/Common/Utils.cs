using ApiApplication.Core.Entities;
using ApiApplication.Core.ValueObjects;

namespace ApiApplication.Core.Common;

public static class Utils
{
    public static IEnumerable<Seat> Generate(ushort rowsCount, ushort seatsPerRowCount)
    {
        List<Seat> seats = new List<Seat>();
        
        for (ushort row = 1; row <= rowsCount; row++)
        {
            for (ushort col = 1; col <= seatsPerRowCount; col++)
            {
                seats.Add(Seat.Create(Position.Create(row,col)));
            }
        }

        return seats;
    }
}