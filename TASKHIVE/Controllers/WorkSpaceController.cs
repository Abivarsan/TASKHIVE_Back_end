using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TASKHIVE.DTO.Meeting;
using TASKHIVE.DTO.WorkSpace;
using TASKHIVE.IRepository;
using TASKHIVE.Model;
using TASKHIVE.Repository;

namespace TASKHIVE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSpaceController : ControllerBase
    {
        private readonly IWorkSpaceRepository _workSpaceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WorkSpaceController> _logger;
        public WorkSpaceController(IWorkSpaceRepository workSpaceRepository, IMapper mapper, ILogger<WorkSpaceController> logger)
        {
            _workSpaceRepository = workSpaceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<ActionResult<WorkSpaceDto>> Create([FromBody] WorkSpaceDto workSpaceDto)
        {
            var result = _workSpaceRepository.IsRecordExists(x => x.workSpaceName == workSpaceDto.workSpaceName);

            if (result)
            {
                return Conflict("Schedule Date already exists");
            }
            var workSpace = _mapper.Map<WorkSpace>(workSpaceDto);

            await _workSpaceRepository.create(workSpace);

            return CreatedAtAction("GetById", new { id = workSpace.workSpaceId }, workSpace);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<GetAllWorkSpaceDto>>> GetAll()
        {
            var workSpaces = await _workSpaceRepository.GetAll();

            var workSpacesDto = _mapper.Map<List<GetAllMeetingDto>>(workSpaces);

            if (workSpaces == null)
            {
                return NoContent();
            }

            return Ok(workSpacesDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<GetWorkSpaceByIdDto>> GetById(int id)
        {
            var workSpace = await _workSpaceRepository.Get(id);



            if (workSpace == null)
            {
                _logger.LogError($"Error while try to get record id: {id}");
                return NoContent();
            }
            var meetingDto = _mapper.Map<GetMeetingByIdDto>(workSpace);

            return Ok(meetingDto);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WorkSpace>> Update(int id, [FromBody] WorkSpaceDto workSpaceDto)
        {
            if (workSpaceDto == null || id != workSpaceDto.workSpaceId)
            {
                return BadRequest();
            }

            var workSpace = _mapper.Map<WorkSpace>(workSpaceDto);

            await _workSpaceRepository.update(workSpace);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<WorkSpace>> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var meeting = await _workSpaceRepository.Get(id);

            if (meeting == null)
            {
                return NotFound();
            }

            await _workSpaceRepository.delete(meeting);
            return NoContent();
        }
    }
}
