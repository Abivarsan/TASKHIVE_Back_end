using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TASKHIVE.DTO.Work;
using TASKHIVE.IRepository;
using TASKHIVE.Model;

namespace TASKHIVE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IWorkRepository _workRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WorkController> _logger;
        public WorkController(IWorkRepository workRepository, IMapper mapper, ILogger<WorkController> logger)
        {
            _workRepository = workRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<ActionResult<WorkDto>> Create([FromBody] WorkDto workDto)
        {
            var result = _workRepository.IsRecordExists(x => x.workName == workDto.workName);

            if (result)
            {
                return Conflict("Work Name already exists");
            }
            var work = _mapper.Map<Work>(workDto);

            await _workRepository.create(work);

            return CreatedAtAction("GetById", new { id = work.workId }, work);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<GetAllWorkDto>>> GetAll()
        {
            var works = await _workRepository.GetAll();

            var worksDto = _mapper.Map<List<GetAllWorkDto>>(works);

            if (works == null)
            {
                return NoContent();
            }

            return Ok(worksDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<GetWorkByIdDto>> GetById(int id)
        {
            var work = await _workRepository.Get(id);



            if (work == null)
            {
                _logger.LogError($"Error while try to get record id: {id}");
                return NoContent();
            }
            var workDto = _mapper.Map<GetWorkByIdDto>(work);

            return Ok(workDto);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Work>> Update(int id, [FromBody] WorkDto workDto)
        {
            if (workDto == null || id != workDto.workId)
            {
                return BadRequest();
            }

            var work = _mapper.Map<Work>(workDto);

            await _workRepository.update(work);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Work>> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var work = await _workRepository.Get(id);

            if (work == null)
            {
                return NotFound();
            }

            await _workRepository.delete(work);
            return NoContent();
        }
    }
}

