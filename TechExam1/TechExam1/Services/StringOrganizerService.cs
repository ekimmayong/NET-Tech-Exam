using TechExam1.Interface;
using TechExam1.Models;

namespace TechExam1.Services
{
    public class StringOrganizerService : IStringOrganizerService
    {
        public async Task<UniqueCharacterResponse> GetNumberOfUniqueCharacterFromString(string input)
        {
            UniqueCharacterResponse response = new()
            {
                UniqueCharacterCount = input.Distinct().Count(c => input.Count(x => x == c) == 1)
            };

            return response;
        }
    }
}
