using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microservices.Services.Core.Entities;
using Microservices.Services.Users.Infrastructure;

public class CounterEventHandler : IConsumer<Counter>
{
    public Task Consume(ConsumeContext<Counter> context)
    {
        Console.Out.WriteLine("Yo");
        return Task.CompletedTask;
    }
}