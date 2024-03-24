using ApiApplication.Core.Common;
using ApiApplication.Core.Entities;
using ApiApplication.Core.ValueObjects;
using FluentAssertions;

namespace ApiApplication.Tests;

public class AuditoriumTests
{
    [Fact]
    public void Check_If_Auditorium_Can_Get_Seats_Filtered_By_Position()
    {
        Auditorium auditorium = Auditorium.Create("Atlas", Utils.Generate(7,9));

        
        
        var seatsResult = auditorium.HasSeatsOn([Position.Create(7, 5),Position.Create(7, 10)]);

        seatsResult.AllSeatsFound.Should().BeFalse();
    }
}