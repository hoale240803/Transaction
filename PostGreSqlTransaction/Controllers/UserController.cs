using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostGreSqlTransaction.DTOs;
using PostGreSqlTransaction.Entities;
using PostGreSqlTransaction.Interfaces;
using PostGreSqlTransaction.Repositories.Contracts;

namespace PostGreSqlTransaction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(ILogger<WeatherForecastController> logger, IUserRepository userRepo, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAllUsersAsync();
                _logger.LogInformation("Returned all users from database.");
                var usersResult = _mapper.Map<IEnumerable<UserDTO>>(users);
                return Ok(usersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned user with id: {id}");

                    var userResult = _mapper.Map<UserDTO>(user);
                    return Ok(userResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/account")]
        public async Task<IActionResult> GetUserWithDetails(Guid id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserWithDetailsAsync(id);
                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned user with details for id: {id}");

                    var userResult = _mapper.Map<UserDTO>(user);
                    return Ok(userResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var userEntity = _mapper.Map<User>(user);

                _unitOfWork.Users.CreateUser(userEntity);
                await _unitOfWork.SaveAsync();

                var createdUser = _mapper.Map<UserDTO>(userEntity);

                return CreatedAtRoute("UserById", new {id = createdUser.Id}, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var userEntity = await _unitOfWork.Users.GetUserByIdAsync(id);
                if (userEntity == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(user, userEntity);

                _unitOfWork.Users.UpdateUser(userEntity);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                var accounts = await _unitOfWork.Accounts.AccountsByUser(id);
                if (accounts.Any())
                {
                    _logger.LogError($"Cannot delete user with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete Users. It has related accounts. Delete those accounts first");
                }

                _unitOfWork.Users.DeleteUser(user);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }


    //        using(var unitOfWork = new UnitOfWork())
    //using(var transaction = new unitOfWork.BeginTransaction())
    //{
    //     try
    //     {
    //         unitOfWork.Users.Add(new User(... User One ...))
    //         unitOfWork.Save();

    //         unitOfWork.Addresses(new Address(... Address For User One ...))
    //         unitOfWork.Save();

    //         unitOfWork.Users.Add(new User(... User Two...))
    //         unitOfWork.Save();

    //         unitOfWork.Addresses(new Address(... Address For User Two...))
    //         unitOfWork.Save();
    //         transaction.Commit();
    //     }
    //     catch (Exception)
    //{
    //    transaction.Rollback();
    //}

    //}
}