using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.IRepositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this._regionRepository = regionRepository;
            this._mapper = mapper;
            _logger = logger;
        }
        //GET ALL REGIONS
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get All Regions Action Method was invoked");
            var regionsDomain = await _regionRepository.GetAllAsync();

            _logger.LogInformation($"Finished GetAll Regions request with data : {JsonSerializer.Serialize(regionsDomain)}");
            return Ok(_mapper.Map<List<RegionDto>>(regionsDomain));
        }

        // GET SINGLE REGION (Get Region By ID)
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }

        // POST To Create New Region
        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update region
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
           
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
            
        }

        // Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
