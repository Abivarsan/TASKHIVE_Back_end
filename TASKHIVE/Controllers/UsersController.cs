using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TASKHIVE.DTO.Role;
using TASKHIVE.DTO.Users;
using TASKHIVE.IRepository;
using TASKHIVE.Model;

namespace TASKHIVE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUsersRepository usersRepository, IMapper mapper, ILogger<UsersController> logger)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<ActionResult<CreateUserDto>> Create([FromBody] CreateUserDto usersDto)
        {
            var result = _usersRepository.IsRecordExists(x => x.email == usersDto.email);

            if (result)
            {
                return Conflict("Email already exists");
            }
            var users = _mapper.Map<User>(usersDto);

            await _usersRepository.create(users);

            return CreatedAtAction("GetById", new { id = users.roleId }, users);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<GetAllUserDto>>> GetAll()
        {
            var users = await _usersRepository.GetAll();

            var usersDto = _mapper.Map<List<GetAllUserDto>>(users);

            if (users == null)
            {
                return NoContent();
            }

            return Ok(usersDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<GetUserByIdDto>> GetById(int id)
        {
            var users = await _usersRepository.Get(id);



            if (users == null)
            {
                _logger.LogError($"Error while try to get record id: {id}");
                return NoContent();
            }
            var usersDto = _mapper.Map<GetUserByIdDto>(users);

            return Ok(usersDto);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Update(int id, [FromBody] UpdateUserDto usersDto)
        {
            if (usersDto == null || id != usersDto.userId)
            {
                return BadRequest();
            }

            var users = _mapper.Map<User>(usersDto);

            await _usersRepository.update(users);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<User>> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var role = await _usersRepository.Get(id);

            if (role == null)
            {
                return NotFound();
            }

            await _usersRepository.delete(role);
            return NoContent();
        }
    }
}

