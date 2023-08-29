using AsisProject.Data;
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
    public class FirmaController : ControllerBase
    {
        private readonly IFirmaRepository _firmaRepository;
        private readonly Context _context;

        public FirmaController(IFirmaRepository firmaRepository,Context context)
        {
            _firmaRepository = firmaRepository;
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateFirma([FromBody] Firma firma)
        {
            _firmaRepository.CreateFirma(firma);
            return Ok(new Response { Status = "Success", Message = "Firma Basariyla Olusturuldu" });
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateFirma(int id, [FromBody] Firma firma)
        {
            var existingFirma = _context.Firmalar.Find(id);
            if (existingFirma == null)
                return NotFound(new Response { Status = "Error", Message = "Firma Bulunamadi" });

            existingFirma.FirmaIsim = firma.FirmaIsim;
            _firmaRepository.UpdateFirma(existingFirma);
            return Ok(new Response { Status = "Success", Message = "Firma Basariyla Guncellendi" });
        }

        [HttpGet]
        public async Task<IActionResult> GetFirmalar()
        {
            List<Firma> firmalar = _firmaRepository.GetFirmalar();
            return Ok(firmalar);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult>DeleteFirma(int id)
        {
            _firmaRepository.DeleteFirma(id);
            return Ok(new Response { Status = "Success", Message = "Firma Basariyla Silindi" });
        }
       
    }
}
