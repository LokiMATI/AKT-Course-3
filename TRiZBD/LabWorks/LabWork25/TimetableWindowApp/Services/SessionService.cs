using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using TimetableWindowApp.Contexts;
using TimetableWindowApp.Dtos;

namespace TimetableWindowApp.Services;

public class SessionService(TimetableDbContext context)
{
    private readonly TimetableDbContext _context = context;

    public async Task<IEnumerable<SessionDto>> GetSessionDtosAsync(DateTime date, string cinema)
        =>  await _context.Sessions
            .Include(s => s.Film)
            .Include(s => s.Hall)
            .Where(s => s.SessionTime.Date == date.Date && s.Hall.Cinema == cinema)
            .Select(s => new SessionDto()
            {
                Title = s.Film.Title,
                SessionTime = s.SessionTime.TimeOfDay,
                HallNumber = s.Hall.HallNumber,
                Cinema = s.Hall.Cinema,
                Price = s.Price
            })
            .OrderBy(s => s.Title)
            .ThenBy(s => s.SessionTime)
            .ToListAsync();

    public async Task<IEnumerable<SessionDto>> GetSessionDtosAsync(DateTime date)
        => await _context.Sessions
            .Include(s => s.Film)
            .Include(s => s.Hall)
            .Where(s => s.SessionTime.Date == date.Date)
            .Select(s => new SessionDto()
            {
                Title = s.Film.Title,
                SessionTime = s.SessionTime.TimeOfDay,
                HallNumber = s.Hall.HallNumber,
                Cinema = s.Hall.Cinema,
                Price = s.Price
            })
            .OrderBy(s => s.Title)
            .ThenBy(s => s.SessionTime)
            .ToListAsync();

    public async Task<bool> SaveTimetableAsync(IEnumerable<SessionDto> sessions)
    {
        SaveFileDialog dialog = new();

        ISaveTimetableManager manager;

        dialog.FileName = "Time table";
        dialog.Filter = "CSV (*.csv)|*.csv";

        if (dialog.ShowDialog() == true)
        {
            switch (dialog.FilterIndex)
            {
                case 0:
                    manager = new CsvSaveTimetableManager();
                    break;
                default:
                    manager = new CsvSaveTimetableManager();
                    break;
            }

            return await manager.SaveTimetableAsync(sessions, dialog.FileName);
        }

        return false;
    }
}
