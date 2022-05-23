using Application;
using Microsoft.AspNetCore.Mvc;

namespace Api
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IScoreProvider _scoreProvider;

        public UserController(IScoreProvider scoreProvider)
        {
            _scoreProvider = scoreProvider ?? throw new ArgumentNullException(nameof(scoreProvider));
        }

        [HttpGet]
        [Route("score/{userLogin}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetScore([FromRoute] string userLogin)
        {
            var score = await _scoreProvider.GetScore(userLogin);

            return Ok(score);
        }
    }
}
