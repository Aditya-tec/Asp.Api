using MagicVilla_VillaApi.Models;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaApi.Models.Dto;
using MagicVilla_VillaApi.Data;
using System.Xml;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.JsonPatch;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public string Amenity { get; private set; }
        public string Details { get; private set; }
        public int Id { get; private set; }
        public string ImageUrl { get; private set; }
        public string Name { get; private set; }
        public int Occupancy { get; private set; }
        public double Rate { get; private set; }
        public int Sqft { get; private set; }

        public VillaAPIController (ApplicationDbContext db)
        {
            _db = db;
            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult <IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(_db.Villas);
        }
    
        [HttpGet("{id:int}", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState); }

            if(_db.Villas.FirstOrDefault(u=> u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "name is not valid!!");
                return BadRequest(ModelState);
            }
           
            if(villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Villa model = new();
            {
                Amenity = villaDTO.Amenity;
                Details = villaDTO.Details;
                Id = villaDTO.Id;
                ImageUrl = villaDTO.ImageUrl;
                Name = villaDTO.Name;
                Occupancy = villaDTO.Occupancy;
                Rate = villaDTO.Rate;
                Sqft = villaDTO.Sqft;
            };


            _db.Villas.Add(model);

            _db.SaveChanges();

            return CreatedAtRoute("GetVilla",new { id = villaDTO.Id }, villaDTO);
            
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name ="DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}", Name ="UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if(villaDTO==null || id!= villaDTO.Id)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            //villa.Name = villaDTO.Name;
            //villa.Occupancy = villaDTO.Occupancy;
            //villa.Sqft = villaDTO.Sqft;

            Villa model = new();
            {
                Amenity = villaDTO.Amenity;
                Details = villaDTO.Details;
                Id = villaDTO.Id;
                ImageUrl = villaDTO.ImageUrl;
                Name = villaDTO.Name;
                Occupancy = villaDTO.Occupancy;
                Rate = villaDTO.Rate;
                Sqft = villaDTO.Sqft;
            };
            _db.Villas.Update(model);
            _db.SaveChanges();


            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        

         public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            VillaDTO villaDTO = new();
            {
                Amenity = villa.Amenity;
                Details = villa.Details;
                Id = villa.Id;
                ImageUrl = villa.ImageUrl;
                Name = villa.Name;
                Occupancy = villa.Occupancy;
                Rate = villa.Rate;
                Sqft = villa.Sqft;
            };
            
            if (villa == null)
            {
                return NotFound();
            }
            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa model = new();
            {
                Amenity = villaDTO.Amenity;
                Details = villaDTO.Details;
                Id = villaDTO.Id;
                ImageUrl = villaDTO.ImageUrl;
                Name = villaDTO.Name;
                Occupancy = villaDTO.Occupancy;
                Rate = villaDTO.Rate;
                Sqft = villaDTO.Sqft;
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
