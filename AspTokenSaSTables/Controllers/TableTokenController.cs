using AspTokenSaSTables.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace AspTokenSaSTables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableTokenController : ControllerBase
    {
        private ServiceSaSToken service;
        public TableTokenController(ServiceSaSToken service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<string> GenerateToken(string curso)
        {
            string token = this.service.GenerateSasToken(curso);
            //NO SE DEVUELVE STRING EN LAS APIs. Se devuelve un JSON.
            return Ok(
              new { 
                token = token,                   
            });
        }
    }
}
