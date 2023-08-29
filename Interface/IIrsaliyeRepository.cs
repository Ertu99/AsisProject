using AsisProject.UrunModels;

namespace AsisProject.Interface
{
    public interface IIrsaliyeRepository
    {
        void CreateIrsaliye(Irsaliye irsaliye);
        void UpdateIrsaliye(Irsaliye irsaliye);
        List<Irsaliye> GetIrsaliyeler();
        void DeleteIrsaliye(int id);
    }
}
