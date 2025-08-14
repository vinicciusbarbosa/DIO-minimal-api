using System;
using minimal_api.Api.Domain.Enums;

namespace minimal_api.Domain.DTO
{
    public record VehicleOutDTO
    {
        public int Id { get; init; }
        public string Plate { get; set; } = default!;
        public string Name { get; init; } = default!;
        public string Brand { get; init; } = default!;
        public int Year { get; init; }
    
    }
}
