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
    }
}