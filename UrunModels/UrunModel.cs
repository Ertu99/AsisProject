using System.ComponentModel.DataAnnotations;

namespace AsisProject.UrunModels
{
    public class UrunModel
    {
        [Key]
        public int UrunId { get; set; }
        public DateTime Tarih { get; set; }
        public string Baslangic { get; set; }
        public string Bitis { get; set; }
        public string MalinCinsi { get; set; }
        public int IrsaliyeNo { get; set; }
        public decimal Tonaj { get; set; }
        public decimal Fiyat { get; set; }
        public decimal KdvOran { get; set; }
        public decimal MalHizmetTutari { get; set; }
        public decimal DigerVergiler { get; set; }
        public decimal OdenecekTutar { get; set; }
        public string FirmaIsim { get; set; }
        public bool Fatura { get; set; }
        public bool OdendiBilgisi { get; set; }
        public Irsaliye Irsaliye { get; set; }
        public Firma Firma { get; set; }
        

    }
}
