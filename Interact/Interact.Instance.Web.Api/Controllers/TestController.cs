using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.Instance.Web.Api.Controllers
{
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [Authorize("Bearer")]
        [HttpGet("get")]
        public object Get()
        {
            return new
            {
                valid = true
            };
        }
    }
}
