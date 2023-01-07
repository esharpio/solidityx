Console.WriteLine("Transpiling contract...");

Contract contract = new Contract();
contract.Load("../examples/EIP2.solx");

Console.WriteLine($"Contract has {contract.LineCount} lines");
Console.WriteLine($"Contract license is {contract.License}");
Console.WriteLine($"Contract version is {contract.SolidityVersion}");
Console.WriteLine($"Contract has {contract.Functions.Count} functions");
