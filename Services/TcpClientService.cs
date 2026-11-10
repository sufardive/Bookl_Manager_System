using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Bookl_Manager_System.Services
{
    public class TcpClientService
    {
        private Task _tcpTask;
        private CancellationTokenSource _cts;
        public event Action<string, double> BookReceived;
        public void Start()
        {
            if (_cts != null) return;

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            var ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);

            _tcpTask = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        using (TcpClient tcpClient = new TcpClient())
                        {
                            await tcpClient.ConnectAsync(ipEndPoint);

                            using (NetworkStream stream = tcpClient.GetStream())
                            {
                                var buffer = new byte[1024];

                                while (!token.IsCancellationRequested)
                                {
                                    var received = await stream.ReadAsync(buffer, token);

                                    if (received == 0) break;

                                    var msg = Encoding.UTF8.GetString(buffer, 0, received);
                                    string[] parts = msg.Split(",");

                                    if (parts.Length == 2 &&
                                        double.TryParse(parts[1], out double price))
                                    {
                                        var title = parts[0];

                                        BookReceived?.Invoke(title, price);
                                    }
                                }
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch
                    {
                        // 连接失败或断开，进入重连等待
                    }

                    try
                    {
                        await Task.Delay(3000, token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }
            });
        }
        public async Task StopAsync()
        {
            if (_cts == null) return;

            _cts.Cancel();

            try
            {
                if (_tcpTask != null)
                {
                    await _tcpTask;
                }
            }
            finally
            {
                _cts.Dispose();
                _cts = null;
                _tcpTask = null;
            }
        }
        
        public bool IsStarted
{
    get
    {
        return _cts != null;
    }
}
    }


}
