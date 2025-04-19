using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using Shared;

namespace Persentation
{
    [ApiController]
    [Route("/api/[controller]")]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProductResultDto), (int)HttpStatusCode.OK)]
    public class ApiController:ControllerBase
    {
    }
}
