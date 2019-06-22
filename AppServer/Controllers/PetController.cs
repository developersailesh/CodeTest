
namespace AppServer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AppServer.Dtos;
    using AppServer.Models;
    using AppServer.Query;
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Pet details
    /// </summary>
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(PetDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Pet Controller handles the Apis related to Pet Details
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        /// <param name="log"></param>
        public PetController(IPeopleClientService _peopleClientService,IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET api/values
        /// <summary>
        /// Purpouse of this Api is to return Pet names based on Owner Gender
        /// </summary>
        /// <returns>Returns List of PetDetails with owner Gender and PetCollection details</returns>
        [HttpGet("gender")]
        [ProducesResponseType(typeof(PetDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetPetNamesByOwnerGender()
        {

            if (ModelState.IsValid)
            {
                IEnumerable<People> model = await _mediator.Send(new PeopleQuery {});
                var petDetails = _mapper.Map<List<People>, List<PetDetailsDto>>(model.ToList());
                if (model == null)
                {
                    return NotFound($"Pet Details not found.");
                }

                return Ok(petDetails);
            }
            else
            {
                return BadRequest(new { error = ModelState.Values.SelectMany(x => x.Errors).ToList() });
            }
        }

    }
}
