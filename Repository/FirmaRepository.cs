using AsisProject.Data;
using AsisProject.Interface;
using AsisProject.UrunModels;

namespace AsisProject.Repository
{
    public class FirmaRepository : IFirmaRepository
    {

        private readonly Context _context;

        public FirmaRepository(Context context)
        {
            _context = context;
        }

        public void CreateFirma(Firma firma)
        {
            _context.Firmalar.Add(firma);
            _context.SaveChanges();
        }

        public void DeleteFirma(int id)
        {
            var firma = _context.Firmalar.Find(id);
            if(firma != null)
            {
                _context.Firmalar.Remove(firma);
                _context.SaveChanges();
            }    
        }

        public List<Firma> GetFirmalar()
        {
            return _context.Firmalar.ToList();
        }

        public void UpdateFirma(Firma firma)
        {
            _context.Firmalar.Update(firma);
            _context.SaveChanges();
        }
    }
}
