using HolidayApi.Enums;
using HolidayApi.Models;

namespace HolidayApi.Data
{
    public class IcsParser
    {
        private static readonly HashSet<string> _feriadosComDataMovel = new(StringComparer.OrdinalIgnoreCase)
        {
           "Sexta-feira Santa",
            "Quinta-feira Santa",
            "Domingo de Páscoa",
            "Corpus Christi",
            "Carnaval",
            "Segunda de Carnaval",
            "Quarta-feira de Cinzas"   
        };

        public static IEnumerable<HolidayModel> Parse(string icsContent)
        {
            var todosFeriados = new List<HolidayModel>();
            var linhas = icsContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            HolidayModel? feriadoAtual = null;

            foreach (var linhaBruta in linhas)
            {
                var linha = linhaBruta.Trim();

                if (linha == "BEGIN:VEVENT") 
                {
                    feriadoAtual = new HolidayModel();
                    continue;
                }

                if (linha == "END:VEVENT")
                {
                    if (feriadoAtual is not null)
                    {
                        // AGORA: Qualquer feriado que NÃO esteja na lista de móveis será considerado FIXO
                        feriadoAtual.EFixo = !_feriadosComDataMovel.Contains(feriadoAtual.Name);

                        todosFeriados.Add(feriadoAtual);
                        feriadoAtual = null;
                    }
                    continue;
                }

                if (feriadoAtual is null) continue;


                if (linha.StartsWith("DTSTART;VALUE=DATE:"))
                {
                    var dataStr = linha["DTSTART;VALUE=DATE:".Length..].Trim();
                    if (DateOnly.TryParseExact(dataStr, "yyyyMMdd", out var data))
                        feriadoAtual.Date = data;
                    continue;
                }

                if (linha.StartsWith("SUMMARY:"))
                {
                    feriadoAtual.Name = linha["SUMMARY:".Length..].Trim();
                    continue;
                }

                if (linha.StartsWith("DESCRIPTION:"))
                {
                    var descricao = linha["DESCRIPTION:".Length..].Trim();
                    feriadoAtual.Descricao = descricao;
                    feriadoAtual.Tipo = DetectarTipo(descricao);
                }

            }

            return todosFeriados.DistinctBy(h => new { h.Date, h.Name });
        }

        private static HolidayType DetectarTipo(string descricao)
        {
            if (descricao.Contains("Nacional", StringComparison.OrdinalIgnoreCase))
                return HolidayType.Nacional;
            if (descricao.Contains("Estadual", StringComparison.OrdinalIgnoreCase))
                return HolidayType.Estadual;
            if (descricao.Contains("Municipal", StringComparison.OrdinalIgnoreCase))
                return HolidayType.Municipal;
            return HolidayType.Desconhecido;
        }
    }
}
