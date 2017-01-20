using System;
using System.Linq;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Shadow_Arena.Enumerations;
using Shadow_Arena.Models;
using Shadow_Arena.Repositories;
using Shadow_Arena.Services;

namespace Shadow_Arena
{
    internal class LoginManager
    {
        private PlayerRepository _playerRepo;
        private IHashing _hashing;


        public LoginManager(PlayerRepository playerRepository)
        {
            _playerRepo = playerRepository;
            _hashing = new Hashing();
        }

        public bool IsLoggedIn(ISession session)
        {
            if (session != null && session.GetString(ContextData.PlayerId.ToString()) != null)
            {
                if (_playerRepo.Read(session.GetInt32(ContextData.PlayerId.ToString()).GetValueOrDefault()) != null)
                {
                    return true;
                }
            }
            return false;
        }

        internal void Logout(ISession session)
        {
            session.LoadAsync();
            session.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextSession"></param>
        /// <param name="player"></param>
        /// <param name="protector"></param>
        /// <returns>Wether the login was successful</returns>
        public bool Login(ISession httpContextSession, LoginViewModel player)
        {
            var playerModel = _playerRepo.Read()?
                .FirstOrDefault(p => _hashing.GetHashedPassword(player.Password) == p.PassWord && p.UserName == player.Username);
            if (playerModel != null)
            {
              httpContextSession.SetInt32(ContextData.PlayerId.ToString(), playerModel.Id);
                return true;
            }
            return false;
        }
    }
}