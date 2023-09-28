using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MZWalksApi_6.Models.DTO;
using MZWalksApi_6.Repositories;

namespace MZWalksApi_6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //fetch data from database
            var walksDomain = await walkRepository.GetAllAsync();

            //convert domain walks into DTO walks
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);

            //return response
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            //Get walk domain object from database
            var walkDomain = await walkRepository.GetAsync(id);

            //Convert domain object to DTO
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            //return Response
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest request)
        {

            //check validations
            if(!(await ValidateAddWalkAsync(request)))
            {
                return BadRequest(ModelState);
            }
          
            //convert dto to domain obj
            var walkDomain = new Models.Domain.Walk()
            {
                Length = request.Length,
                Name = request.Name,
                RegionId = request.RegionId,
                WalkDifficultyId = request.WalkDifficultyId,
            };

            //pass domain object to repository
            var response = await walkRepository.AddAsync(walkDomain);

            //convert domain object back to dto
            var walkDTO = new Models.DTO.Walk()
            {
                Id = response.Id,
                Length = response.Length,
                Name = response.Name,
                RegionId = response.RegionId,
                WalkDifficultyId = response.WalkDifficultyId
            };

            //send dto resposne to client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = response.Id }, response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            if (!(await ValidateUpdateWalkAsync(updateWalkRequest)))
            {
                return BadRequest(ModelState);
            }
            //convert dto to domain object
            var walkDomain = new Models.Domain.Walk()
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };


            //pass details to Repository - Get domain object in response (or null)
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            //handle null not found
            if (walkDomain == null)
            {
                return NotFound();
            }

            //convert back to dto
            var walkDTO = new Models.DTO.Walk()
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            //return response
            return Ok(walkDTO);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //call repository to delete walk
            var walkDomain = await walkRepository.DeleteAsync(id);

            //handle null not found
            if (walkDomain == null)
            {
                return NotFound();
            }

            //convert back to dto
            /*
            var walkDTO = new Models.DTO.Walk()
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };
            */

            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            //return response
            return Ok(walkDTO);


        }


        //Region
        private async Task<bool> ValidateAddWalkAsync(AddWalkRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), "Adding Walk Request cannot be Empty or whiteSpaces");
                return false;
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} cannot be null or whiteSpaces");
            }

            if (request.Length <= 0)
            {
                ModelState.AddModelError(nameof(request.Length), $"{nameof(request.Length)} cannot be less then or equal");
            }

            var region = await regionRepository.GetAsync(request.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(request.RegionId), $"{nameof(request.RegionId)} is InValid");
            }

            var walkDifficulty = await walkDifficultyRepository.GetAsync(request.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(request.WalkDifficultyId), $"{nameof(request.WalkDifficultyId)} is InValid");
            }


            if (ModelState.ErrorCount > 0)
            {
                return false;
            }


            return true;
        }


        private async Task<bool> ValidateUpdateWalkAsync(UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest), " Walk Request cannot be Empty or whiteSpaces");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Name), $"{nameof(updateWalkRequest.Name)} cannot be null or whiteSpaces");
            }

            if (updateWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Length), $"{nameof(updateWalkRequest.Length)} cannot be less then or equal");
            }

            var region = await regionRepository.GetAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId), $"{nameof(updateWalkRequest.RegionId)} is InValid");
            }

            var walkDifficulty = await walkDifficultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId), $"{nameof(updateWalkRequest.WalkDifficultyId)} is InValid");
            }


            if (ModelState.ErrorCount > 0)
            {
                return false;
            }


            return true;
        }
    }
}