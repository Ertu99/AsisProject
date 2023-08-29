using AsisProject.Interface;
using AsisProject.UrunModels;
using AsisProject.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AsisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunController : ControllerBase
    {
        private readonly IUrunRepository _urunRepository;

        public UrunController(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUrunler()
        {
            List<UrunModel> urunler = _urunRepository.GetUrunler();
            return Ok(urunler);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUrunById(int id)
        {
            UrunModel urun = _urunRepository.GetUrunById(id);

            if (urun == null)
                return NotFound(new Response { Status = "Error", Message = "Urun Bulunamadi" });

            return Ok(urun);
        }

        [HttpGet("malinCinsi/{malinCinsi}")]
        public async Task<IActionResult> SearchByMalinCinsi(string malinCinsi)
        {
            List<UrunModel> urunler = _urunRepository.SearchByMalinCinsi(malinCinsi);
            return Ok(urunler);
        }

        [HttpGet("tarih/{tarih}")]
        public async Task<IActionResult> SearchByTarih(DateTime tarih)
        {
            List<UrunModel> urunler = _urunRepository.SearchByTarih(tarih);
            return Ok(urunler);
        }
        [HttpGet("irsaliyeno/{irsaliyeNo}")]
        public async Task<IActionResult> SearchByIrsaliyeNo(int irsaliyeNo)
        {
            var urun = _urunRepository.SearchByIrsaliyeNo(irsaliyeNo);
            if (urun == null)
                return NotFound();
            return Ok(urun);
        }

        [HttpGet("firmaisim/{firmaIsim}")]
        public async Task<IActionResult> SearchByFirmaIsim(string firmaIsim)
        {
            var urunler = _urunRepository.SearchByFirmaIsim(firmaIsim);
            
            return Ok(urunler);
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateUrun([FromBody] UrunModel urunModel)
        {
            _urunRepository.CreateUrun(urunModel);
            return Ok(new Response { Status = "Success", Message = "Urun Basariyla Olusturuldu" });
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateUrun(int id, [FromBody] UrunModel urunModel)
        {
            var existingUrun = _urunRepository.GetUrunById(id);
            if (existingUrun == null)
                return NotFound(new Response { Status = "Error", Message = "Urun Bulunamadi" });

            existingUrun.IrsaliyeNo = urunModel.IrsaliyeNo;
            existingUrun.FirmaIsim = urunModel.FirmaIsim;
            existingUrun.MalinCinsi = urunModel.MalinCinsi;
            existingUrun.Tarih = urunModel.Tarih;
            existingUrun.Tonaj = urunModel.Tonaj;
            existingUrun.Fiyat = urunModel.Fiyat;
            existingUrun.KdvOran = urunModel.KdvOran;
            existingUrun.DigerVergiler = urunModel.DigerVergiler;
            existingUrun.Baslangic = urunModel.Baslangic;
            existingUrun.Bitis = urunModel.Bitis;
            existingUrun.Fatura = urunModel.Fatura;
            existingUrun.OdendiBilgisi = urunModel.OdendiBilgisi;

            _urunRepository.UptadeUrun(existingUrun);
            return Ok(new Response { Status = "Success", Message = "Urun Basariyla Guncellendi" });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteUrun(int id)
        {
            _urunRepository.DeleteUrun(id);
            return Ok(new Response { Status = "Success", Message = "Urun Basariyla Silindi" });
        }

    }
}
