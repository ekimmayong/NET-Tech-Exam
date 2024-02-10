using TechExam2.Model;

namespace TechExam2.Interface
{
    public interface ICalculatorService
    {
        CalculateResponse RoundingSumOf2Int(int firstNumber, int secondNumber);
    }
}
