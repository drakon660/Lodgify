﻿using ApiApplication.Core.Common;
using ApiApplication.Core.Entities;
using ApiApplication.Core.ValueObjects;
using FluentAssertions;

namespace ApiApplication.Tests;

public class AuditoriumTests
{
    [Fact]
    public void Check_If_Auditorium_Can_Get_Seats_Filtered_By_Position()
    {
        Auditorium auditorium = Auditorium.Create("Atlas");
        auditorium.SetSeats(Utils.Generate(7,11));

        var seatsResult = auditorium.HasSeatsOn([Position.Create(7, 5),Position.Create(7, 10)]);

        auditorium.Seats.Should().HaveCount(77);
        seatsResult.AllSeatsFound.Should().BeTrue();
    }
}