using Microsoft.AspNetCore.SignalR;

namespace SampelToDo.Services
{
    public class UpdateHub:Hub
    {
        public async Task Update() =>
            await Clients.All.SendAsync("Update");
    }
}
