using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Shared;

namespace WebApplication2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> ValidCred(LoginModel login)
        {
             
            return "";
        }

        [HttpPost("token")]
        //[RoleAttriub("Admin")]
        public ActionResult generatetoken([FromBody]TokenRequest request)
        {
            BL.BL.Loginlogic loginlogic = new BL.BL.Loginlogic(new DB());
            if (loginlogic.ValidUser(request.email, request.Userpw))
            {
                var asa = new JWTAuth("asdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasd");
                var token = asa.generatetoken(request);
                var refreshtoken = asa.GenerateRefreshToken();
                loginlogic.SetRefreshToken(request.email, refreshtoken);
                var response = JsonConvert.SerializeObject(new Token() {token = token,RefreshToken = refreshtoken });
                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpGet("refreshtoken")]
        public ActionResult CreateNewTokenBaseRefresh([FromHeader]string authorization)
        {
           string request = authorization.Replace("Bearer ","");
           BL.BL.Loginlogic loginlogic = new BL.BL.Loginlogic(new DB());
            if (loginlogic.validRefreshtoken(request))
            {
                var user = loginlogic.Usertokenrequest(request);
                loginlogic.RemoveRefreshToken(request);
                var asa = new JWTAuth("asdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasd");
                var token = asa.generatetoken(user);
                var refreshtoken = asa.GenerateRefreshToken();
                loginlogic.SetRefreshToken(user.email,refreshtoken, request);
                var response = JsonConvert.SerializeObject(new Token { token = token, RefreshToken = refreshtoken});
                return Ok(response);
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpDelete("signout")]
        public ActionResult Signout([FromHeader] string username)
        {
            BL.BL.Loginlogic loginlogic = new BL.BL.Loginlogic(new DB());
            loginlogic.InvalidToken(username);
            return Ok();
        }
        [Authorize]
        [HttpPost("UpdatePassword")]
        public ActionResult UpdatePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            BL.BL.Loginlogic loginlogic = new BL.BL.Loginlogic(new DB());
            loginlogic.UpdatePassword(changePasswordModel.Username, changePasswordModel.NewPassword, changePasswordModel.Password);
            return Ok();
        }
    }
}
