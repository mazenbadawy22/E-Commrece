﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record BasketDto
    {
        public string Id { get; init; }
        public IEnumerable<BasketIemDto> Items { get; init; }


    }
}
