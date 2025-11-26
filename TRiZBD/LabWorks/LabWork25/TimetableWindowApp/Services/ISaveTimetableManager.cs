using TimetableWindowApp.Dtos;

namespace TimetableWindowApp.Services;

public interface ISaveTimetableManager
{
    public Task<bool> SaveTimetableAsync(IEnumerable<SessionDto> sessions, string path);
}
