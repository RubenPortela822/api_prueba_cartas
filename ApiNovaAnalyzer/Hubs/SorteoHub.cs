using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace ApiNovaAnalyzer.Hubs
{
    public class SorteoHub : Hub
    {        
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Cliente conectado: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }
        
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Cliente desconectado: {Context.ConnectionId}. Reason: {exception?.Message}");
            return base.OnDisconnectedAsync(exception);
        }     
    }
}
