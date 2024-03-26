using CSharpFunctionalExtensions;

namespace ApiApplication.Core.ValueObjects;

public class Position : ValueObject<Position>
{
    public ushort RowNumber { get; }
    public ushort SeatNumber { get; }

    protected Position()
    {
        
    }
    
    private Position(ushort rowNumber, ushort seatNumber)
    {
        RowNumber = rowNumber > 0 ? rowNumber : throw new ArgumentOutOfRangeException(nameof(rowNumber));
        SeatNumber = seatNumber > 0 ? seatNumber : throw new ArgumentOutOfRangeException(nameof(rowNumber));
    }

    public static Position Create(ushort rowNumber, ushort seatNumber) => new (rowNumber, seatNumber);
    protected override bool EqualsCore(Position other)
    {
        return RowNumber == other.RowNumber && SeatNumber == other.SeatNumber;
    }

    protected override int GetHashCodeCore()
    {
        return HashCode.Combine(RowNumber, SeatNumber);
    }
    public override string ToString() => $"[row:{RowNumber}, seat:{SeatNumber}]";
    
}