using Microsoft.AspNetCore.Mvc;

namespace FlighStatusApi.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public abstract class BaseApiController<T> : ControllerBase where T : BaseApiController<T>
{
}