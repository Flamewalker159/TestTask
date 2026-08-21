using TestTask.Entities;

namespace TestTask.Calculators;

public interface IResultCalculator
{
    public Result Calculate(List<Values> records);
}