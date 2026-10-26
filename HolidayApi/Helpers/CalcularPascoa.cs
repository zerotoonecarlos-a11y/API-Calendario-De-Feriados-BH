namespace HolidayApi.Helpers
{
    public class CalcularPascoa
    {
        public static DateOnly GetPascoa(int ano)
        {
            int a = ano % 19;
            int b = ano / 100;
            int c = ano % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int mes = (h + l - 7 * m + 114) / 31;
            int dia = ((h + l - 7 * m + 114) % 31) + 1;

            return new DateOnly(ano, mes, dia);
        }

        public static DateOnly GetSextaFeiraSanta(int ano) => GetPascoa(ano).AddDays(-2);
        public static DateOnly GetQuintaFeiraSanta(int ano) => GetPascoa(ano).AddDays(-3);
        public static DateOnly GetCarnaval(int ano) => GetPascoa(ano).AddDays(-47);
        public static DateOnly GetSegundaCarnaval(int ano) => GetPascoa(ano).AddDays(-48);
        public static DateOnly GetQuartaDeCinzas(int ano) => GetPascoa(ano).AddDays(-46);
        public static DateOnly GetCorpusChristi(int ano) => GetPascoa(ano).AddDays(60);
    }
}
