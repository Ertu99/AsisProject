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
    public class IrsaliyeController : ControllerBase
    {
        private readonly IIrsaliyeRepository _irsaliyeRepository;
        private readonly Context _context;

        public IrsaliyeController(IIrsaliyeRepository irsaliyeRepository, Context context)
        {
            _irsaliyeRepository = irsaliyeRepository;
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateIrsaliye([FromBody] Irsaliye irsaliye)
        {
            _irsaliyeRepository.CreateIrsaliye(irsaliye);
            return Ok(new Response { Status = "Success", Message = "Irsaliye Basariyla olusturuldu" });
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateIrsaliye(int id, [FromBody] Irsaliye irsaliye)
        {
            var existingIrsaliye = _context.IrsaliyeNumaralari.Find(id);

            if (existingIrsaliye == null)
                return NotFound(new Response { Status = "Error", Message = "Irsaliye bulunamadi" });

            existingIrsaliye.IrsaliyeNo = irsaliye.IrsaliyeNo;

            _irsaliyeRepository.UpdateIrsaliye(existingIrsaliye);
            return Ok(new Response { Status = "Success", Message = "Irsaliye basariyla guncellendi" }); 
        }

        [HttpGet]
        public async Task<IActionResult> GetIrsaliyeler()
        {
            List<Irsaliye> irsaliyeler = _irsaliyeRepository.GetIrsaliyeler();
            return Ok(irsaliyeler);

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteIrsaliye(int id )
        {
            _irsaliyeRepository.DeleteIrsaliye(id);
            return Ok(new Response { Status = "Success", Message = "Irsaliye Basariyla Silindi" });
        }


    }
}
