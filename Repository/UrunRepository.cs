using AsisProject.Data;
using AsisProject.Interface;
using AsisProject.UrunModels;

namespace AsisProject.Repository
{
    public class UrunRepository : IUrunRepository
    {
        private readonly Context _context;

        public UrunRepository(Context context)
        {
            _context = context;
        }

        public void CreateUrun(UrunModel urunModel)
        {
            urunModel.MalHizmetTutari = urunModel.Tonaj * urunModel.Fiyat;
            urunModel.OdenecekTutar = urunModel.MalHizmetTutari + urunModel.KdvOran - urunModel.DigerVergiler;

            _context.Urunler.Add(urunModel);
            _context.SaveChanges();
        }

        public void DeleteUrun(int id)
        {
            var urunModel = _context.Urunler.Find(id);
            if (urunModel != null)
            {
                _context.Urunler.Remove(urunModel);
                _context.SaveChanges();
            }
        }

        public UrunModel GetUrunById(int id)
        {
            return _context.Urunler.Find(id);
        }

        public List<UrunModel> GetUrunler()
        {
            return _context.Urunler.ToList();
        }

        public UrunModel SearchByFirmaIsim(string firmaIsim)
        {
            return _context.Urunler.FirstOrDefault(u => u.FirmaIsim == firmaIsim);
        }

        public List<UrunModel> SearchByIrsaliyeNo(int irsaliyeNo)
        {
            return _context.Urunler.Where(u => u.IrsaliyeNo == irsaliyeNo).ToList();
        }
    

        public List<UrunModel> SearchByMalinCinsi(string malinCinsi)
        {
            return _context.Urunler.Where(u => u.MalinCinsi.Contains(malinCinsi)).ToList();
        }

        public List<UrunModel> SearchByTarih(DateTime tarih)
        {
            return _context.Urunler.Where(u => u.Tarih.Date == tarih.Date).ToList();
        }

        public void UptadeUrun(UrunModel urunModel)
        {
            urunModel.MalHizmetTutari = urunModel.Tonaj * urunModel.Fiyat;
            urunModel.OdenecekTutar = urunModel.MalHizmetTutari + urunModel.KdvOran - urunModel.DigerVergiler;

            _context.Urunler.Update(urunModel);
            _context.SaveChanges();
        }
    }
}

       