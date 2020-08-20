namespace Demo.PASS.Faring.Web.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Demo.Core.DataAccess.Abstraction.Paging;
    using Demo.Core.DataAccess.Utilities.Criteria;
    using Demo.PASS.Faring.Abstractions.Services;
    using Demo.PASS.Faring.DataTransferModels.EWalletPass;
    using Demo.PASS.Faring.Model;

    /// <summary>
    /// The controller for the EZWalletPass.
    /// </summary>
    [ApiVersion(ApiConstants.ApiVersion)]
    [ApiController]
    [Route("api/pass/v{version:apiVersion}/ez-wallet/pass")]
    public class EZWalletPassController : ControllerBase
    {
        /// <summary>
        /// Defines the _eWalletPassService.
        /// </summary>
        private readonly IEWalletPassService _eWalletPassService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EZWalletPassController"/> class.
        /// </summary>
        /// <param name="service">The EZWalletPassService service to call.</param>
        public EZWalletPassController(IEWalletPassService service)
        {
            _eWalletPassService = service;
        }

        /// <summary>
        /// Gets a list of ezWalletPasses.
        /// </summary>
        /// <param name="pagingInfo">An <see cref="IPagingStrategy"/> with the pagination information.</param>
        /// <returns>A <see cref="Task{TResult}"/> whose result contains the status object as a <see cref="JsonResult"/>.</returns>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IPaginatedList<EZWalletPassReadDto>>> GetEWalletPass([FromQuery] PagingStrategy pagingInfo)
        {
            return Ok(await _eWalletPassService.GetEWalletPassesAsync(pagingInfo).ConfigureAwait(false));
        }

        /// <summary>
        /// Gets a ezWalletPasss.
        /// </summary>
        /// <param name="ezWalletPassId">The id of the ezWalletPass to return.</param>
        /// <returns>A <see cref="Task{IResult}"/> whose result contains the status object as a <see cref="JsonResult"/>.</returns>
        [HttpGet]
        [Route("{ezWalletPassId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EZWalletPassReadDto>> GetEWalletPassById([FromRoute][Required]int ezWalletPassId)
        {
            return Ok(await _eWalletPassService.GetEWalletPassByIdAsync(ezWalletPassId).ConfigureAwait(false));
        }

        /// <summary>
        /// Inserts a ezWalletPass.
        /// </summary>
        /// <param name="ezWalletPassInsertDto">A <see cref="EZWalletPassInsertDto"/> object to create a new ezWalletPass.</param>
        /// <returns>A <see cref="Task{IResult}"/> whose result contains the status object as a <see cref="JsonResult"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
        public async Task<ActionResult<int>> InsertEWalletPass([FromBody][Required]EZWalletPassInsertDto ezWalletPassInsertDto)
        {
            var ezWalletPassId = await _eWalletPassService.AddEWalletPassAsync(ezWalletPassInsertDto).ConfigureAwait(false);

            if (ezWalletPassId > 0)
            {
                return Created(new Uri($"{HttpContext.Request.Host}{HttpContext.Request.Path}/{ezWalletPassId}", UriKind.RelativeOrAbsolute), ezWalletPassId);
            }
            else
            {
                var error = new ProblemDetails
                {
                    Title = "EZWalletPass Not Added",
                    Detail = "The EZWalletPass could not be added to the database",
                    Status = StatusCodes.Status500InternalServerError,
                    Instance = HttpContext.Request.Path,
                };

                return new ObjectResult(error)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }

        /// <summary>
        /// Updates a ezWalletPass.
        /// </summary>
        /// <param name="ezWalletPassId">The id of the ezWalletPass to update.</param>
        /// <param name="ezWalletPassUpdateDto">A <see cref="EZWalletPassUpdateDto"/> to update the existing ezWalletPass with.</param>
        /// <returns>A <see cref="Task{IResult}"/> whose result contains the status object as a <see cref="JsonResult"/>.</returns>
        [HttpPut]
        [Route("{ezWalletPassId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEWalletPass([FromRoute][Required]int ezWalletPassId, [FromBody][Required]EZWalletPassUpdateDto ezWalletPassUpdateDto)
        {
            var success = await _eWalletPassService.UpdateEWalletPassAsync(ezWalletPassId, ezWalletPassUpdateDto).ConfigureAwait(false);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return Problem(
                    "The EZWalletPass could not be saved.",
                    HttpContext.Request.Path,
                    StatusCodes.Status500InternalServerError,
                    "EZWalletPass Not Updated.");
            }
        }

        /// <summary>
        /// Deletes a ezWalletPass.
        /// </summary>
        /// <param name="ezWalletPassId">The id of the ezWalletPass to delete.</param>
        /// <returns>A <see cref="Task{IResult}"/> whose result contains the status object as a <see cref="JsonResult"/>.</returns>
        [HttpDelete]
        [Route("{ezWalletPassId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEWalletPass([FromRoute][Required]int ezWalletPassId)
        {
            var success = await _eWalletPassService.DeleteEWalletPassAsync(ezWalletPassId).ConfigureAwait(false);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return Problem(
                    "The EZWalletPass could not be deleted.",
                    HttpContext.Request.Path,
                    StatusCodes.Status500InternalServerError,
                    "EZWalletPass Not Deleted.");
            }
        }
    }
}
