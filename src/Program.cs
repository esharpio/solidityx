// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Contract contract = new Contract();
contract.Load("../examples/EIP2.solx");

Console.WriteLine($"Contract has {contract.LineCount} lines");