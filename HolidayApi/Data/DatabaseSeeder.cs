namespace HolidayApi.Data
{
    public class DatabaseSeeder
    {
        public static async Task EnvioAsync(HolidayDbContext db, IWebHostEnvironment env, CancellationToken cancellationToken = default)
        {
            await db.Database.EnsureCreatedAsync(cancellationToken);

            if (db.Feriados.Any())
                return;

            var icsCaminho = Path.Combine(env.ContentRootPath, "Data", "feriados.ics");

            if (!File.Exists(icsCaminho))
                throw new FileNotFoundException($"O arquivo não foi encontrado.", icsCaminho);


             var icsConteudo = await File.ReadAllTextAsync(icsCaminho, cancellationToken);
             var feriados = IcsParser.Parse(icsConteudo).ToList();

            await db.Feriados.AddRangeAsync(feriados, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }
    } 
}
