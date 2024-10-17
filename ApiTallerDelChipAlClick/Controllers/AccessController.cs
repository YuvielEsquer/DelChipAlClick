using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiTallerDelChipAlClick.Models;
using ApiTallerDelChipAlClick.Helpers;
using ApiTallerDelChipAlClick.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace ApiTallerDelChipAlClick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private TallerContext _context;
        private Utilities _utilities;
        public AccessController(TallerContext context,
            Utilities utilities)
        {
            _context = context;
            _utilities = utilities;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var UserModel = new UsersModel
            {
                UserName = dto.UserName,
                UserKey = _utilities.EncryptSHA256(dto.UserKey),
            };

            await _context.Users.AddAsync(UserModel);
            await _context.SaveChangesAsync();

            if (UserModel.UserID != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var UserFound = await _context.Users
                            .Where(u =>
                            u.UserName == dto.UserName &&
                            u.UserKey == _utilities.EncryptSHA256(dto.UserKey)
                            ).FirstOrDefaultAsync();
            if (UserFound == null)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilities.triggerJwT(UserFound) });
        }
    }
}
