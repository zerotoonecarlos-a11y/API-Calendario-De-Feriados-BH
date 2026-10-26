using HolidayApi.Enums;
using System.Data;

namespace HolidayApi.Models
{
    public class HolidayModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public HolidayType Tipo { get; set; }
        public bool EFixo { get; set; }
    }
}
