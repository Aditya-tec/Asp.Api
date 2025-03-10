using MagicVilla_VillaApi.Models.Dto;
namespace MagicVilla_VillaApi.Data

{
    public static class VillaStore
    { 
        public static List<VillaDTO> villaList = new List<VillaDTO>
            {
                new VillaDTO { Id = 1, Name = "Pool Villa", Sqft=100, Occupancy=4 },
                new VillaDTO { Id = 2, Name = "beich Villa", Sqft=200, Occupancy=3 },
            };
    }
}
