using HolidayApi.Models;

namespace HolidayApi.Services.Interfaces
{
    public interface  IHolidayService
    {
        Task<IEnumerable<HolidayModel>> GetFeriadosPorAno(int ano);
        DateOnly? RecalcularFeriadosMovel(string nomeFeriado, int ano);
    }
}
