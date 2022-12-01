using Microsoft.AspNetCore.Mvc;
using WalletWasabi.WabiSabiClientLibrary.Filters;

namespace WalletWasabi.WabiSabiClientLibrary.Controllers;

[ApiController]
[ExceptionTranslateFilter]
[Produces("application/json")]
public class Controller : ControllerBase
{
}
