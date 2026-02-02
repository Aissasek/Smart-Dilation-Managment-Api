using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace Smart_Dilation_Management.Hups
{
    public class NotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext()?.Request.Headers["UserId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {

                Groups.AddToGroupAsync(Context.ConnectionId, userId);

                Console.WriteLine($"User connected: {userId} - ConnectionId: {Context.ConnectionId}");
            }

            return base.OnConnectedAsync();
        }
        public async Task SendMessageToUser(string senderId, string receiverId, string message)
        {

            await Clients.Group(receiverId).SendAsync("ReceiveMessage", message, senderId);

            await Clients.Group(senderId).SendAsync("ReceiveMessage", message, senderId);
        }
    }
}
