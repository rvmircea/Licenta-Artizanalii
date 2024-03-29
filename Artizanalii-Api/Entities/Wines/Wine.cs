﻿using Artizanalii_Api.Entities.Producers;
using Newtonsoft.Json;

namespace Artizanalii_Api.Entities.Wines;

public class Wine
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int YearCreated { get; set; }
    public double Abv { get; set; }
    public int ProducerId { get; set; } 
    [JsonIgnore]
    public virtual Producer Producer { get; set; }
}