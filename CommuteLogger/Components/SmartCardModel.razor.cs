using System;
using Microsoft.AspNetCore.Components;
using PCSC;

namespace CommuteLogger.Components;

public partial class SmartCardModel
{
    public string[] readerNames { get; set; } = Array.Empty<string>();

    [Parameter]
    public EventCallback<string[]> ChangeReaderNames { get; set; }

    public void ListupReaders()
    {
        var contextFactory = ContextFactory.Instance;
        var context = contextFactory.Establish(SCardScope.System);
        
        Console.WriteLine("Currently connected readers: ");
        readerNames = context.GetReaders();
        foreach (var readerName in readerNames)
        {
            Console.WriteLine("\t" + readerName);
        }
        ChangeReaderNames.InvokeAsync(this.readerNames);
        
    }
}

