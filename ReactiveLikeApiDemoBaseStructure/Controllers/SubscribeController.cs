using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactiveLikeApiDemo.Services;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ReactiveLikeApiDemo.Controllers
{
    [ApiController]
    [Route("api/subscribe")]
    public class SubscribeController : ControllerBase
    {
        [HttpGet]
        public async Task Subscribe(CancellationToken token)
        {
            // Sets the response type to SSE so the browser can listen for real-time updates.
            Response.Headers.Add("Content-Type", "text/event-stream");

            // Used to keep the HTTP request alive until cancellation occurs.
            var tcs = new TaskCompletionSource();

            // Subscribes to throttled post updates and sends each update to the client via SSE format.
            var subscription = BroadcastService.Subscribe(async post =>
            {
                var json = JsonSerializer.Serialize(post);
                await Response.WriteAsync($"data: {json}\n\n");
                await Response.Body.FlushAsync(); 
            });

            // Handles client disconnection by cleaning up the subscription and completing the task.
            token.Register(() =>
            {
                subscription.Dispose();  // Dispose the subscriber
                tcs.SetResult();         // Complete the task
            });

            // Keeps the HTTP request open as long as the client is connected.
            await tcs.Task;
        }
    }
}
