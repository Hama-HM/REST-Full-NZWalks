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
    [Route("api/difficulty")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;
        public DifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this._walkDifficultyRepository = walkDifficultyRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var difficultyDomain = await _walkDifficultyRepository.GetAllAsync();

            return Ok(_mapper.Map<List<DifficultyDto>>(difficultyDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName(nameof(GetById))]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var difficultyDomain = await _walkDifficultyRepository.GetByIdAsync(id);
            if (difficultyDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DifficultyDto>(difficultyDomain));
        }

        [HttpPost]
        //[Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] DifficultyRequestDto addDifficultyRequestDto)
        {
            var difficultyDomainModel = _mapper.Map<Difficulty>(addDifficultyRequestDto);
            difficultyDomainModel = await _walkDifficultyRepository.CreateAsync(difficultyDomainModel);
            var difficultyDto = _mapper.Map<DifficultyDto>(difficultyDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = difficultyDto.Id }, difficultyDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] DifficultyRequestDto updateDiffcultyDto)
        {
           
            var difficultyDomainModel = _mapper.Map<Difficulty>(updateDiffcultyDto);
            difficultyDomainModel = await _walkDifficultyRepository.UpdateAsync(id, difficultyDomainModel);

            if (difficultyDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DifficultyDto>(difficultyDomainModel));
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var difficultyDomainModel = await _walkDifficultyRepository.DeleteAsync(id);

            if (difficultyDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DifficultyDto>(difficultyDomainModel));
        }
    }
}
