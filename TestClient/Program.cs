using System;
using System.Buffers;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Net.Sockets.Kcp;
using System.Net.Sockets.Kcp.Simple;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press F1 send word.......");

            SimpleKcpClient kcpClient = new SimpleKcpClient(50001, end);
            //kcpClient.kcp.TraceListener = new ConsoleTraceListener();		//日志
            Task.Run(async () =>
            {
                while (true)
                {
                    kcpClient.kcp.Update(DateTimeOffset.UtcNow);
                    await Task.Delay(10);
                }
            });

            while (true)
            {
                var k = Console.ReadKey();
                if (k.Key == ConsoleKey.F1)
                {
                    //Send(kcpClient, System.Text.Encoding.UTF8.GetBytes("发送一条消息"));

                    Byte[] bigBytes = new Byte[128 * 1376];		//默认极限
                    for (int i = 0; i < bigBytes.Length; i++)
                        bigBytes[i] = 0xFF;
                    Send(kcpClient, bigBytes);
                }
            }

            Console.ReadLine();
        }

        static IPEndPoint end = new System.Net.IPEndPoint(System.Net.IPAddress.Loopback, 40001);
        static async void Send(SimpleKcpClient client, byte[] bytes)
        {
            client.SendAsync(bytes, bytes.Length);
            var resp = await client.ReceiveAsync();
            var respstr = System.Text.Encoding.UTF8.GetString(resp);
            Console.WriteLine($"收到服务器回复:    {respstr}");
        }
    }
}
