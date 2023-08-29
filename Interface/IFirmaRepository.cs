using AsisProject.UrunModels;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace AsisProject.Interface
{
    public interface IFirmaRepository
    {
        void CreateFirma(Firma firma);
        void UpdateFirma(Firma firma);
        List<Firma> GetFirmalar();
        void DeleteFirma(int id);

    }
}
