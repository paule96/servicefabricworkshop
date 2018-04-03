using GetNumber.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using RenderHtml.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            GetResult().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        static async Task GetResult()
        {
            try
            {
                var getnumber = ActorProxy.Create<IGetNumber>(ActorId.CreateRandom(), new Uri("fabric:/_02__Actor/GetNumberActorService"));
                await getnumber.SetCountAsync(5, new System.Threading.CancellationToken());
                Task<int> number = getnumber.GetCountAsync(new System.Threading.CancellationToken());
                var renderHtmlActor = ActorProxy.Create<IRenderHtml>(ActorId.CreateRandom(), new Uri("fabric:/_02__Actor/RenderHtmlActorService"));
                await renderHtmlActor.SetContent(5, new System.Threading.CancellationToken());
                Task<string> retval = renderHtmlActor.GetHtmlAsync(new System.Threading.CancellationToken());
                var html = retval.GetAwaiter().GetResult();
                Console.WriteLine(html);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
