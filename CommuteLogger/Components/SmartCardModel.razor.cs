using System;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components;
using PCSC;
using PCSC.Iso7816;
using PCSC.Exceptions;

namespace CommuteLogger.Components;

public partial class SmartCardModel
{
    public string[] readerNames { get; set; } = Array.Empty<string>();

    [Parameter]
    public EventCallback<string[]> ChangeReaderNames { get; set; }

    public void ListupReaders()
    {
        try
        {
            var contextFactory = ContextFactory.Instance;
            using var context = contextFactory.Establish(SCardScope.User);

            Console.WriteLine("Currently connected readers: ");
            readerNames = context.GetReaders();
            foreach (var readerName in readerNames)
            {
                Console.WriteLine("\t" + readerName);
            }
            ChangeReaderNames.InvokeAsync(this.readerNames);
        }
        catch (PCSCException ex)
        {
            Console.WriteLine("PCSCException: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
        
    }
}

