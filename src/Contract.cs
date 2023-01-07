public class Contract
{
    public string Name { get; set; }

    public string SolidityVersion { get; set; }

    // private List<ContractFunction> _functions = new List<ContractFunction>();

    private List<String> _lines;

    public Int32 LineCount
    {
        get
        {
            return _lines.Count;
        }
    }

    public void Load(string path)
    {
        // Load the contract from the file
        _lines = File.ReadAllLines(path).ToList();
    }
}