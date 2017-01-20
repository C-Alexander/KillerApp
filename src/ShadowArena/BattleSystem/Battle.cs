using System;
using System.Collections.Generic;
using System.Linq;
using Shadow_Arena.Models;

namespace Shadow_Arena.BattleSystem
{
    public class Battle
    {
        public int?[] PlayerIDs = new int?[2];
        private bool ended = false;
        public bool NeedsCleanup { get; set; } = false;
        public List<BattleCharacter> characters { get; set; }

        void ProgressForCharacters()
        {
            
        }

        void CheckForDeath()
        {
            
        }

        void CheckIfWon()
        {
            
        }

        public void Start(ICollection<Player> players)
        {
            NeedsCleanup = true;
            var playersInBattle = players.Where(p => PlayerIDs.Contains(p.Id));
            foreach (var player in playersInBattle)
            {
                characters.AddRange(player.Character.Select(c => 
                new BattleCharacter()
                {
                    CharName = c.Name,
                    CharacterId = c.Id,
                    LastAttack = DateTime.Now,
                    PlayerId = player.Id,
                    Stats = c.Stat
                })); //add chars to battle
            }
            foreach (BattleCharacter c in characters)
            {
                c.HP = (c.Stats.Vitality*3) + 100; //set default hp
            }
        }

        public void Fight(int characterId)
        {
            var friendlyStats = characters.First(ch => ch.CharacterId == characterId).Stats;
            var friendlyPlayerId = characters.First(ch => ch.CharacterId == characterId).PlayerId;
            foreach (BattleCharacter c in characters.Where(ch => ch.PlayerId != friendlyPlayerId))
            {
                c.HP -= ((friendlyStats.Attack * 2) + 10) - c.Stats.Defense;
            }
        }
    }
}