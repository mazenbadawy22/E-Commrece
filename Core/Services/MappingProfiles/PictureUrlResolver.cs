using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Shared;

namespace Services.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return string.Empty;
            }

            //return $"https://localhost:7059/{source.PictureUrl}";
            return $"{_configuration["baseUrl"]}{source.PictureUrl}";
        }
    }
}
///images/products/ItalianChickenMarinade.png