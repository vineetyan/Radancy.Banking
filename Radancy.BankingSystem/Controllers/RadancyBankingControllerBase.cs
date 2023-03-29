using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Radancy.BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [Produces("application/json")]
    public class RadancyBankingControllerBase: ControllerBase
    {
    }
}
