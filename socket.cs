using System.Net.WebSockets;
using System.Text;

public class Socket
{
    private string url;
    private ClientWebSocket ws;

    public Socket(string gameId)
    {
        url = "ws://localhost:3000/game/join/" + gameId;
        ws = new ClientWebSocket();
    }

    public async Task Connect()
    {
        try
        {
            Console.WriteLine("Connection to socket: " + url);
            await ws.ConnectAsync(new Uri(url), CancellationToken.None);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        var receiveTask = this.ReceiveTask();
        var sendTask = SendTask();
        
        await Task.WhenAny(sendTask, receiveTask);
        if (this.ws.State != WebSocketState.Closed)
        { 
            await this.ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        }
        await Task.WhenAll(sendTask, receiveTask);
    }

    private Task ReceiveTask()
    {
        return Task.Run(async () =>
        {
            var buffer = new byte[1024];
            while (true)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }

                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine("Received: " + message);
            }
        });
    }

    private Task SendTask()
    {
        return Task.Run(async () =>
        {
            while (true)
            {
                var message = Console.ReadLine();

                if (message == "exit")
                {
                    //TODO: Add some disconnect logic
                }

                var bytes = Encoding.UTF8.GetBytes(message);
                await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true,
                                CancellationToken.None);
            }
        });
    }
}