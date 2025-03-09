using MagicVilla_VillaApi.Models;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaApi.Models.Dto;
using MagicVilla_VillaApi.Data;
using System.Xml;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult <IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }
    
        [HttpGet("{id:int}")]
        public ActionResult <VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
    }
}
