using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TASKHIVE.DTO.Role;
using TASKHIVE.DTO.User;
using TASKHIVE.IRepository;
using TASKHIVE.Model;
using TASKHIVE.Repository;
using TASKHIVE.Service;

namespace TASKHIVE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly JwtService _jwtService;
        private readonly IRoleRepository _roleRepository;
        public UserController(IUserRepository usersRepository, IMapper mapper, ILogger<UserController> logger, IRoleRepository roleRepository, JwtService jwtService)
        {
            _userRepository = usersRepository;
            _mapper = mapper;
            _logger = logger;
            _roleRepository = roleRepository;
            _jwtService = jwtService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<GetAllUserDto>>> GetAll()
        {
            var users = await _userRepository.GetAll();

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
            var users = await _userRepository.Get(id);

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
        public async Task<ActionResult<User>> Update(int id, [FromBody] UserDto usersDto)
        {
            if (usersDto == null || id != usersDto.userId)
            {
                return BadRequest();
            }

            var users = _mapper.Map<User>(usersDto);

            await _userRepository.update(users);

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

            var role = await _userRepository.Get(id);

            if (role == null)
            {
                return NotFound();
            }

            await _userRepository.delete(role);
            return NoContent();
        }

        //[HttpPost("register")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        //public async Task<ActionResult<UserDto>> Create([FromBody] UserDto userDto)
        //{
        //    var result =  _userRepository.IsRecordExists(x=>x.email== userDto.email);

        //    if (result)
        //    {
        //        return Conflict("User in with this email already exists.");

        //    }
        //    var user = _mapper.Map<User>(userDto);

        //    var passwordHasher = new PasswordHasher<User>();
        //    user.password = passwordHasher.HashPassword(user, userDto.password);

        //    await _userRepository.create(user);
        //    return Ok(new { Message = "Registration successful" });
        //}

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserDto>> Create([FromBody] UserDto userDto)
        {
            // Check if user with this email already exists
            var result = _userRepository.IsRecordExists(x => x.email == userDto.email);
            if (result)
            {
                return Conflict("User with this email already exists.");
            }

            // Parse the role name to the corresponding UserRole enum
            if (!Enum.TryParse(userDto.RoleName, true, out UserRole userRole))
            {
                return BadRequest("Invalid role name.");
            }

            // Map the UserDto to User entity
            var user = _mapper.Map<User>(userDto);

            // Find the Role from the UserRole enum
            var role = await _roleRepository.GetRoleByUserRole(userRole);
            if (role == null)
            {
                return BadRequest("Role not found.");
            }

            // Set the RoleId for the user
            user.roleId = role.roleId;

            // Hash the password
            var passwordHasher = new PasswordHasher<User>();
            user.password = passwordHasher.HashPassword(user, userDto.password);

            // Create the user
            await _userRepository.create(user);
            return Ok(new { Message = "Registration successful" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            var user = await _userRepository.GetByEmail(loginDto.email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Verify the password using PasswordHasher
            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.password, loginDto.password);

            if (verificationResult != PasswordVerificationResult.Success)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Ensure user has a role before generating token
            if (user.Role == null)
            {
                return BadRequest("User role is missing.");
            }

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
        
        [HttpPost("loginWithGoogle")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] LoginWithGoogleDto request)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { "1026346412066-3pfllm7m2j3f92rnk6frmsmng74bootn.apps.googleusercontent.com" }
            };

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.credential, settings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Google token validation failed: {ex.Message}");
                return BadRequest("Invalid Google credential.");
            }

            var user = await _userRepository.GetByEmail(payload.Email);
            if (user == null)
            {
                var token = _jwtService.GenerateToken(new User
                {
                    email = payload.Email, 
                });

                return Ok(new { Token = token });
            }

            return BadRequest("User already exists. Please use another login method.");
        }

    }
}

