namespace lab25;

public interface IDataProcessorStrategy
{
    string Name { get; }
    string Process(string data);
}