using TechExam2.Interface;
using TechExam2.Model;

namespace TechExam2.Service
{
    public class CalculatorService : ICalculatorService
    {
        public CalculateResponse RoundingSumOf2Int(int firstNumber, int secondNumber)
        {
            int sum = firstNumber + secondNumber;
            int mod = (sum % 5);

            CalculateResponse response = new()
            {
                Answer = mod == 0 ? sum : sum + 5 - mod
            };

            return response;
        }
    }
}
