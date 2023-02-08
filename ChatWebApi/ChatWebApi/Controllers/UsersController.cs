using ChatWebApi.Models;
using ChatWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpPost("register")]
        public ActionResult<string> Register(User user)
        {
            var regMessage = userService.IsNewUser(user);
            return Ok(regMessage);
        }


        [HttpPost("login")]
        public ActionResult<string> Login(User user)
        {
            var logMessage = userService.Login(user);
            return Ok(logMessage);
        }


        [HttpPost("getUserByUsername")]
        public ActionResult<User> GetUserByUsername(User user)
        {
            var loggedUser = userService.GetUserByUsername(user.username!);
            return Ok(loggedUser);
        }


        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var loggedUser = userService.GetUserById(id);
            return Ok(loggedUser);
        }


        //[HttpGet("getUsers/{id}")]
        //public ActionResult<IEnumerable<User>> GetUsersExceptSelf(int id)
        //{
        //    var users = userService.GetUsersExceptSelf(id);

        //    return Ok(users);
        //}


        [HttpPut("logout")]
        public IActionResult Logout(User user)
        {
            userService.Logout(user.userId);
            return Ok();
        }


        [HttpPut("setActiveUser")]
        public ActionResult<User> SetActiveUser(User user)
        {
            var updatedUser = userService.SetActiveUser(user);
            return Ok(updatedUser);
        }


        [HttpPost("getUsersBySearch")]
        public ActionResult<IEnumerable<User>> GetUsersBySearch(object[] search)
        {
            string searchWord = search[0].ToString();
            int.TryParse(search[1].ToString(), out int userId);

            var searchUsers = userService.GetUsersBySearch(searchWord, userId);
            return Ok(searchUsers);
        }


        [HttpPost("checkRoom")]
        public ActionResult<string> CheckIfRoomExist(User[] users)
        {
            var roomId = userService.CheckIfRoomExist(users);
            return Ok(roomId);
        }


        [HttpPut("setRoom")]
        public ActionResult<string> SetRoom(User[] users)
        {
            var roomId = userService.CreateRoom(users);
            return Ok(roomId);
        }


    }
}
