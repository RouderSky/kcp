using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net.Sockets.Kcp.Simple;
using System.Threading.Tasks;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SimpleKcpClient kcpClient = new SimpleKcpClient(40001);
            //kcpClient.kcp.TraceListener = new ConsoleTraceListener();		//日志
            Task.Run(async () =>
            {
                while (true)
                {
                    kcpClient.kcp.Update(DateTimeOffset.UtcNow);
                    await Task.Delay(10);
                }
            });

            StartRecv(kcpClient);
            Console.ReadLine();
        }

        static async void StartRecv(SimpleKcpClient client)
        {
            while (true)
            {
                var res = await client.ReceiveAsync();
                Console.WriteLine($"收到一条消息，长度为：{res.Length}");
                //Console.WriteLine($"收到一条消息，内容为：{System.Text.Encoding.UTF8.GetString(res)}");

                var str = System.Text.Encoding.UTF8.GetString(res);
                if ("发送一条消息" == str)
                {
                    var buffer = System.Text.Encoding.UTF8.GetBytes("回复一条消息");
                    client.SendAsync(buffer, buffer.Length);
                }
            }
        }
    }
}
