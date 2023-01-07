public enum Mutability
{
    Nonpayable,
    Payable,
    View,
    Pure
}

public class ContractFunction
{
    public string Name { get; set; }

    public string? ReturnType { get; set; }

    public Mutability Mutability { get; set; }

    public List<ContractFunctionParameter> Parameters { get; set; }

    public void Parse(IList<String> functionLines)
    {
        // Parse the function name
        ParseName(functionLines[0]);

        // Parse the function parameters
        ParseParameters(functionLines[0]);

        // Parse the function return type
        ParseReturnType(functionLines[0]);

        // Parse the function mutability
        ParseMutability(functionLines[0]);
    }

    private void ParseName(string line)
    {
        // Parse the function name
        var name = line.Substring(0, line.IndexOf("(")).Trim();
        Name = name.Substring(name.LastIndexOf(" ") + 1);
    }

    private void ParseParameters(string line)
    {
        // Parse the function parameters
        var parameters = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")") - line.IndexOf("(") - 1);
        Parameters = new List<ContractFunctionParameter>();
        if (parameters.Length > 0)
        {
            foreach (var parameter in parameters.Split(","))
            {
                var parts = parameter.Trim().Split(" ");
                Parameters.Add(new ContractFunctionParameter
                {
                    Name = parts[1],
                    Type = parts[0]
                });
            }
        }
    }

    private void ParseReturnType(string line)
    {
        // Parse the function return type
        var returnType = line.Substring(line.IndexOf(")") + 1).Trim();
        if (returnType.Length > 0)
        {
            ReturnType = returnType.Substring(returnType.LastIndexOf(" ") + 1);
        }
    }

    private void ParseMutability(string line)
    {
        // Parse the function mutability
        if (line.Contains("payable"))
        {
            Mutability = Mutability.Payable;
        }
        else if (line.Contains("view"))
        {
            Mutability = Mutability.View;
        }
        else if (line.Contains("pure"))
        {
            Mutability = Mutability.Pure;
        }
        else
        {
            Mutability = Mutability.Nonpayable;
        }
    }
}