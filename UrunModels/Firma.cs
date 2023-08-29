using System.ComponentModel.DataAnnotations;

namespace AsisProject.UrunModels
{
    public class Firma
    {
        [Key]
        public int FirmaId { get; set; }
        public string FirmaIsim { get; set; }
    }
}
