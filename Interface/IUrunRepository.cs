using AsisProject.UrunModels;

namespace AsisProject.Interface
{
    public interface IUrunRepository
    {
        List<UrunModel> GetUrunler();
        UrunModel GetUrunById(int id);
        List<UrunModel> SearchByMalinCinsi(string malinCinsi);
        List<UrunModel> SearchByTarih(DateTime tarih);
        List<UrunModel> SearchByIrsaliyeNo(int irsaliyeNo);
        UrunModel SearchByFirmaIsim(string firmaIsim);
        void CreateUrun(UrunModel urunModel);
        void UptadeUrun(UrunModel urunModel);
        void DeleteUrun(int id);
    }
}
