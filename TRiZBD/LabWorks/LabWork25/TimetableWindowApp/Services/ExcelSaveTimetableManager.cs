using TimetableWindowApp.Dtos;
using Excel = Microsoft.Office.Interop.Excel;

namespace TimetableWindowApp.Services;

public class ExcelSaveTimetableManager : ISaveTimetableManager
{
    public async Task<bool> SaveTimetableAsync(IEnumerable<SessionDto> sessions, string path)
    {
        var app = new Excel.Application();

        var movieTheaters = sessions.Select(s => s.Cinema).Distinct().ToList();
        app.SheetsInNewWorkbook = movieTheaters.Count;
        var workbook = app.Workbooks.Add();

        Excel.Sheets worksheet;
        IEnumerable<SessionDto> sessions;

        for (int i = 0;  i < movieTheaters.Count; i++)
        {
            worksheet = workbook.Worksheets[i];


        }
    }
}
