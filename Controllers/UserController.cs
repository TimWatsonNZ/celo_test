
using System.Collections.Generic;
using System.Threading.Tasks;
using celo_test.Filters;
using celo_test.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace celo_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] UserFilter filter) =>
            Ok(_userService.Get(filter));

        [HttpPost]
        public IActionResult Post([FromBody]User user) => Ok(_userService.Insert(user));

        [HttpPut]
        public async Task<IActionResult> Update(User user) {
            var result = await _userService.Update(user);

            return Ok(user);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id) => Ok(_userService.Get(id));
    }
}
