using HolidayApi.Data;
using HolidayApi.Helpers;
using HolidayApi.Models;
using HolidayApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HolidayApi.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly HolidayDbContext _context;

        public HolidayService(HolidayDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HolidayModel>> GetFeriadosPorAno(int ano)
        {
           var feriadosDoBanco = await _context.Feriados.AsNoTracking().ToListAsync();
            var resultado = new List<HolidayModel>();

            foreach (var feriado in feriadosDoBanco)
            {
                var feriadoAtualizado = new HolidayModel
                {
                    Id = feriado.Id,
                    Name = feriado.Name,
                    Descricao = feriado.Descricao,
                    Tipo = feriado.Tipo,
                    EFixo = feriado.EFixo
                };

                if (feriado.EFixo)
                {
                    feriadoAtualizado.Date = new DateOnly(ano, feriado.Date.Month, feriado.Date.Day);
                }
                else
                {
                    var dataRecalculada = RecalcularFeriadosMovel(feriado.Name, ano);

                    if(dataRecalculada.HasValue)
                    {
                        feriadoAtualizado.Date = dataRecalculada.Value;
                    }
                    else
                    {
                        feriadoAtualizado.Date = new DateOnly(ano, feriado.Date.Month, feriado.Date.Day);
                    }
                }

                resultado.Add(feriadoAtualizado);
            }

            return resultado.OrderBy(f => f.Date);

        }

        public DateOnly? RecalcularFeriadosMovel(string nomeFeriado, int ano)
        {
            return nomeFeriado switch
            {
                "Páscoa" => CalcularPascoa.GetPascoa(ano),
                "Sexta-feira da Paixão" => CalcularPascoa.GetSextaFeiraSanta(ano),
                "Quinta-feira da Paixão" => CalcularPascoa.GetQuintaFeiraSanta(ano),
                "Carnaval" => CalcularPascoa.GetCarnaval(ano),
                "Segunda-feira do Carnaval" => CalcularPascoa.GetSegundaCarnaval(ano),
                "Quarta-feira de Cinzas" => CalcularPascoa.GetQuartaDeCinzas(ano),
                "Corpus Christi" => CalcularPascoa.GetCorpusChristi(ano),
                _ => null
            };
        }
    }
}
 