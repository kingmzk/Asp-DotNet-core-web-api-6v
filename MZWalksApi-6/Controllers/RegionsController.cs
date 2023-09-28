using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MZWalksApi_6.Models.DTO;
using MZWalksApi_6.Repositories;

namespace MZWalksApi_6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            //return DTO regions
            /*
            var regionsDTO = new List<Models.DTO.Region>();

            foreach (var region in regions)
            {
                regionsDTO.Add(new Models.DTO.Region()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Population = region.Population
                });
            }
            */

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {

            //validate the incoming request
            if (!ValidateRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }

            //request to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            //pass detail to Repository
            var response = await regionRepository.AddAsync(region);

            //convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = response.Id,
                Code = response.Code,
                Area = response.Area,
                Lat = response.Lat,
                Long = response.Long,
                Name = response.Name,
                Population = response.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //get region from database
            var region = await regionRepository.DeleteAsync(id);

            //if null not found
            if (region == null)
            {
                return NotFound();
            }

            //convert to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            //return Ok response

            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            //Check validations
            if (!ValidateUpdateRegionAsync(updateRegionRequest))
            {
                return BadRequest(ModelState);
            }

            //convert DTO to domain model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };

            // update Region using repository

            region = await regionRepository.UpdateAsync(id, region);

            //If null then not found

            if (region == null)
            {
                return NotFound();
            }

            //convert back to DTO
            var regionDto = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            //return Ok

            return Ok(regionDto);
        }

        [HttpPatch]
        [Route("{id:guid}")]
        public async Task<IActionResult> PartialUpdateRegionAsync(Guid id, [FromBody] PatchRegionRequest patchRegionRequest)
        {
            if (patchRegionRequest == null)
            {
                return BadRequest();
            }

            var region = new Models.Domain.Region()
            {
                Code = patchRegionRequest.Code,
                Area = patchRegionRequest.Area,
                Lat = patchRegionRequest.Lat,
                Long = patchRegionRequest.Long,
                Name = patchRegionRequest.Name,
                Population = patchRegionRequest.Population
            };

            // Update the region using the repository
            var updatedRegion = await regionRepository.UpdatesAsync(id, region);

            if (updatedRegion == null)
            {
                return NotFound();
            }

            // Convert the updated region back to a DTO
            var updatedRegionDto = new Models.DTO.Region
            {
                Id = updatedRegion.Id,
                Code = updatedRegion.Code,
                Area = updatedRegion.Area,
                Lat = updatedRegion.Lat,
                Long = updatedRegion.Long,
                Name = updatedRegion.Name,
                Population = updatedRegion.Population
            };

            return Ok(updatedRegionDto);
        }

        //REGION private method

        private bool ValidateRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), "Adding Region Request cannot be Empty or whiteSpaces");
                return false;
            }


            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code), "Code cannot be null or whiteSpaces");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name), "Name cannot be null or whiteSpaces");
            }

            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), "Areaa cannot be less then or equal");
            }

            //if (addRegionRequest.Lat <= 0)
            //{
            //    ModelState.AddModelError(nameof(addRegionRequest.Lat), "Lat cannot be less then or equal");
            //}

            //if (addRegionRequest.Long <= 0)
            //{
            //    ModelState.AddModelError(nameof(addRegionRequest.Long), "Long cannot be less then or equal");
            //}

            if (addRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population), "Population cannot be less then or equal");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }


        private bool ValidateUpdateRegionAsync(UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest), "Adding Region Request cannot be Empty or whiteSpaces");
                return false;
            }


            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code), "Code cannot be null or whiteSpaces");
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name), "Name cannot be null or whiteSpaces");
            }

            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area), "Areaa cannot be less then or equal");
            }

            //if (updateregionrequest.lat <= 0)
            //{
            //    modelstate.addmodelerror(nameof(updateregionrequest.lat), "lat cannot be less then or equal");
            //}

            //if (updateregionrequest.long <= 0)
            //{
            //    modelstate.addmodelerror(nameof(updateregionrequest.long), "long cannot be less then or equal");
            //}

            if (updateRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population), "Population cannot be less then or equal");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }


    }
}