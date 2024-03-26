// See https://aka.ms/new-console-template for more information
// The port number must match the port of the gRPC server.

using Grpc;
using Grpc.Net.Client;
using var channel = GrpcChannel.ForAddress("https://localhost:8443");

var client = new ShowtimeService.ShowtimeServiceClient(channel);
var reservationsResponse = await client.GetAllReservationsAsync(new GetAllReservationsRequest());

foreach (var reservation in reservationsResponse.Reservations)
{
    Console.WriteLine(reservation.ReservationId);    
}

Console.WriteLine("Hello, World!");