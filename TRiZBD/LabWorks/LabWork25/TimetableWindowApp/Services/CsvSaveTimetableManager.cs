using System.IO;
using TimetableWindowApp.Dtos;

namespace TimetableWindowApp.Services;

public class CsvSaveTimetableManager : ISaveTimetableManager
{
    public async Task<bool> SaveTimetableAsync(IEnumerable<SessionDto> sessions, string path)
    {
        try
        {
            using StreamWriter writer = new(path, false, System.Text.Encoding.UTF8);

            await writer.WriteLineAsync("Title;SessionTime;HallNumber;Price");
            foreach (var session in sessions)
                await writer.WriteLineAsync($"{session.Title};{session.SessionTime.ToString(@"hh\:mm")};{session.HallNumber};{session.Price}");

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
