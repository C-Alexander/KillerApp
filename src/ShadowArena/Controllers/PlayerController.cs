using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShadowArena.Models;

namespace DalFun2Application
{
    //not exactly secure, normally you'd use Identity and an Authorize attribute. But this is mostly a POC and frankly, worrying about security in this is out of scope

        //alex said, right before some kid on infralab figured out the requests and deleted every player with Charles
    class PlayerController : Controller
    {
        private IPlayerRepository repository;
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public PlayerController(IPlayerContext context)
        {
            repository = new PlayerRepository(context);
        }
        
        [HttpPost]
        private IActionResult DeletePlayer(int playerId)
        {
          
            Player player = new Player();
            player.Id = playerId;
            repository.delete(player);
            return View();
        }

        [HttpPost]
        private IActionResult UpdatePlayer(Player player)
        {
            if (ModelState.IsValid) { 
            repository.update(player);
               }
            return View();
        }

        private void OnClickCreatePlayer(object sender, EventArgs eventArgs)
        {
            Player player; 
            getPlayerData(out player, (Form1)((Button)sender).Parent);
            repository.add(player);
        }

        private void getPlayerData(out Player player, Form1 form)
        {
            var newPlayer = new Player();
            newPlayer.UserName = ((TextBox)form.Controls["tbUsername"]).Text;
            newPlayer.Password = ((TextBox)form.Controls["tbPass"]).Text;
            newPlayer.Level = Convert.ToInt32(((TextBox)form.Controls["tbLevel"]).Text);
            newPlayer.Experience = Convert.ToInt32(((TextBox)form.Controls["tbExperience"]).Text);
            player = newPlayer;
        }
    }
}
