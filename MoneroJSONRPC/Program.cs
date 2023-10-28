// See https://aka.ms/new-console-template for more information
using MoneroJSONRPC;
using MoneroJSONRPC.Models;
using System;


while (true)
{
    Console.Write("Daemon RPC:");
    var method = Console.ReadLine();
    var aparams = string.Empty;
    if (method.Contains(" "))
    {
        var ps = method.Substring(method.IndexOf(" "));
        aparams = ps.Trim();
        method= method.Split(' ')[0];   
    } 
    //This creates an address and paymentid combination.
    var integratedAddress = JsonRPC.InvokeMethod<IntegratedAddress>(method, aparams);
    Console.WriteLine(integratedAddress);

}