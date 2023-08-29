using AsisProject.Data;
using AsisProject.Interface;
using AsisProject.UrunModels;

namespace AsisProject.Repository
{
    public class IrsaliyeRepository : IIrsaliyeRepository
    {
        private readonly Context _context;

        public IrsaliyeRepository(Context context)
        {
            _context = context;
        }

        public void CreateIrsaliye(Irsaliye irsaliye)
        {
            _context.IrsaliyeNumaralari.Add(irsaliye);
            _context.SaveChanges();
        }

        public void DeleteIrsaliye(int id)
        {
            var irsaliye = _context.IrsaliyeNumaralari.Find(id);
            if (irsaliye != null)
            {
                _context.IrsaliyeNumaralari.Remove(irsaliye);
                _context.SaveChanges();
            }
        }

        public List<Irsaliye> GetIrsaliyeler()
        {
            return _context.IrsaliyeNumaralari.ToList();
               
        }

        public void UpdateIrsaliye(Irsaliye irsaliye)
        {
            _context.IrsaliyeNumaralari.Update(irsaliye);
            _context.SaveChanges();
        }
    }
}
