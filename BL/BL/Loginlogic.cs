using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Shared;

namespace BL.BL
{
    public class Loginlogic
    {
        DB _dbContext;
        public Loginlogic(DB dB)
        {
            _dbContext = dB;
        }

        public bool ValidUser(string username, string pw)
        {
            if(_dbContext.GetUser(username) != null)
            {
               return _dbContext.CheckPw(username, pw);
            }
            return false;
        }

        public bool validRefreshtoken(string token)
        {
            if (_dbContext.CheckRefreshtoken(token))
            {
                return true;
            }
            return false;
        }

        public void RemoveRefreshToken(string token)
        {
            _dbContext.InvalidRefreshtoken(token);
        }
        public TokenRequest Usertokenrequest(string token)
        {
            var item = _dbContext.GetUser(_dbContext.getuseroftoken(token));
            return new TokenRequest()
            {
                email = item.UserName,
                UserId = item.UserId
            };
        }
        public void SetRefreshToken(string user,string token)
        {
            _dbContext.SetRefreshtoken(user, token);
        }
        public void SetRefreshToken(string user, string token, string oldtoken)
        {
            _dbContext.InvalidRefreshtoken(oldtoken);
            _dbContext.SetRefreshtoken(user, token);
        }

        public void InvalidToken(string user)
        {
            _dbContext.InvalidRefreshtokenLogout(user);
        }
        public void UpdatePassword(string UserId, string NewPassword, string OldPassword)
        {
            if(_dbContext.CheckPw(UserId,OldPassword))
            {
                _dbContext.ChangePassword(_dbContext.GetUser(UserId).UserId, NewPassword);
            }
        }
    }
}
