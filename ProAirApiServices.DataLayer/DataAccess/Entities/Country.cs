using System;
namespace ProAirApiServices.DataLayer.DataAccess.Entities
{
	public class Country
	{
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public int Phone { get; set; }
        public string Symbol { get; set; } = null!;
        public string Currency { get; set; } = null!;
    }
}

