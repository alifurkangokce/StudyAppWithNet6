using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study.Core.DTOs;

namespace Study.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> customResponseDto)
        {
            if (customResponseDto.StatusCode==204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = customResponseDto.StatusCode
                };
            }

            return new ObjectResult(customResponseDto)
            {
                StatusCode = customResponseDto.StatusCode
            };
        }
    }
}
