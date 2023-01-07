public class Contract
{
    public string License { get; private set; }

    public string Name { get; private set; }

    public string SolidityVersion { get; private set; }

    private List<ContractFunction> _functions = new List<ContractFunction>();

    public IList<ContractFunction> Functions
    {
        get
        {
            return _functions;
        }
    }

    private List<ContractEvent> _events = new List<ContractEvent>();

    public IList<ContractEvent> Events
    {
        get
        {
            return _events;
        }
    }


    private List<String> _lines;

    public Int32 LineCount
    {
        get
        {
            return _lines.Count;
        }
    }

    private List<String> _imports = new List<String>();

    public IList<String> Imports
    {
        get
        {
            return _imports;
        }
    }

    private Stack<String> _tokens;

    public void Load(string path)
    {
        // Load the contract from the file
        _lines = File.ReadAllLines(path).ToList();

        // Parse the license
        ParseLicense();
        ParseVersion();
        ParseFunctions();
    }

    // public void Parse(List<String> lines)
    // {
    //     // Load the contract from the file
    //     _lines = lines;

    //     // Parse the license
    //     ParseLicense();
    //     ParseVersion();
    //     ParseFunctions();
    // }

    private void ParseTokens()
    {
        if (_lines.Count == 0)
        {
            throw new Exception("Contract is empty");
        }

        _tokens = new Stack<string>();

        foreach (string line in _lines)
        {
            List<String> words = line.Trim().Split(" ").ToList();

            foreach(string word in words)
            {
                // Push the word onto the stack if not "" or Tab
                if (word != "" && word != "\t")
                {
                    _tokens.Push(word);
                }
            }
        }
    }

    private void ParseImports()
    {
        if (_lines.Count == 0)
        {
            throw new Exception("Contract is empty");
        }

        foreach (string line in _lines)
        {
            if (line.Trim().StartsWith("import"))
            {
                // Parse the import
                string[] parts = line.Split(" ");
                string importPath = parts[1].Trim();
                _imports.Add(importPath);
            }
        }
    }

    private void ParseLicense()
    {
        if (_lines.Count == 0)
        {
            throw new Exception("Contract is empty");
        }

        foreach (string line in _lines)
        {
            if (line.Contains("SPDX-License-Identifier:"))
            {
                // Parse the license
                string[] parts = line.Split(":");
                License = parts[1].Trim();
                break;
            }
        }
    }

    private void ParseVersion()
    {
        if (_lines.Count == 0)
        {
            throw new Exception("Contract is empty");
        }

        foreach (string line in _lines)
        {
            if (line.Trim().StartsWith("pragma solidity"))
            {
                // Parse the version
                string[] parts = line.Split(" ");
                SolidityVersion = parts[2];
                break;
            }
        }
    }

    private void ParseFunctions()
    {
        if (_lines.Count == 0)
        {
            throw new Exception("Contract is empty");
        }


        Boolean isFunction = false;
        int leftBraceCount = 0;
        int rightBraceCount = 0;
        List<string> functionLines = new List<string>();

        foreach (string line in _lines)
        {
            if (line.Trim().StartsWith("function"))
            {
                // Parse the function
                isFunction = true;
                // ContractFunction function = new ContractFunction();

                List<string> words = line.Trim().Split(" ").ToList();
                
                foreach (string word in words)
                {
                    if (word.Contains("{"))
                    {
                        leftBraceCount++;
                    }

                    if (word.Contains("}"))
                    {
                        rightBraceCount++;
                    }
                }

                functionLines.Add(line);

                isFunction = leftBraceCount != rightBraceCount;
                continue;
            }

            if (isFunction)
            {
                // Parse the function
                // ContractFunction function = new ContractFunction();

                List<string> words = line.Split(" ").ToList();

                foreach (string word in words)
                {
                    if (word.Contains("{"))
                    {
                        leftBraceCount++;
                    }

                    if (word.Contains("}"))
                    {
                        rightBraceCount++;
                    }
                }

                functionLines.Add(line);
                isFunction = leftBraceCount != rightBraceCount;
            }

            if (!isFunction && functionLines.Count > 0)
            {
                // Parse the function
                ContractFunction function = new ContractFunction();
                function.Parse(functionLines);

                _functions.Add(function);

                // reset
                leftBraceCount = 0;
                rightBraceCount = 0;
                isFunction = false;
                functionLines.Clear();
            }
        }
    }

    private void ParseFunctionsByTokens()
    {
        if (_tokens.Count == 0)
        {
            throw new Exception("Contract is empty");
        }

        foreach (string token in _tokens)
        {
            Boolean isFunction = false;
            int leftBraceCount = 0;
            int rightBraceCount = 0;
            List<string> functionLines = new List<string>();

            if (token.Trim() == "function")
            {
                // Parse the function
                isFunction = true;
                
                if (_tokens.Peek() == "{")
                {
                    leftBraceCount++;
                }

                if (_tokens.Peek() == "}")
                {
                    rightBraceCount++;
                }

                // ContractFunction function = new ContractFunction();

                // List<string> words = line.Trim().Split(" ").ToList();
                
                // foreach (string word in words)
                // {
                //     if (word.Contains("{"))
                //     {
                //         leftBraceCount++;
                //     }

                //     if (word.Contains("}"))
                //     {
                //         rightBraceCount++;
                //     }
                // }

                // functionLines.Add(line);

                isFunction = leftBraceCount != rightBraceCount;
            }

            if (isFunction)
            {
                // Parse the function
                // ContractFunction function = new ContractFunction();

                // List<string> words = line.Split(" ").ToList();

                // foreach (string word in words)
                // {
                //     if (word.Contains("{"))
                //     {
                //         leftBraceCount++;
                //     }

                //     if (word.Contains("}"))
                //     {
                //         rightBraceCount++;
                //     }
                // }

                // functionLines.Add(line);
                isFunction = leftBraceCount != rightBraceCount;
            }

            if (!isFunction && functionLines.Count > 0)
            {
                // Parse the function
                ContractFunction function = new ContractFunction();
                function.Parse(functionLines);

                _functions.Add(function);

                // reset
                leftBraceCount = 0;
                rightBraceCount = 0;
                functionLines.Clear();
            }
        }
    }
}