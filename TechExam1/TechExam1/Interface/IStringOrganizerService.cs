using TechExam1.Models;

namespace TechExam1.Interface
{
    public interface IStringOrganizerService
    {
        Task<UniqueCharacterResponse> GetNumberOfUniqueCharacterFromString(string input);
    }
}
