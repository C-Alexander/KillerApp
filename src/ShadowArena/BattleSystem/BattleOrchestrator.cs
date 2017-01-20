using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shadow_Arena.Models;

namespace Shadow_Arena.BattleSystem
{
    public static class BattleOrchestrator
    {
        static List<Battle> _battleQueue = new List<Battle>();
        static List<Battle> _battles = new List<Battle>();

        public static void FindBattle(ICollection<Player> players, int playerid)
        {
            foreach (Battle b in _battles)
            {
                if (b.NeedsCleanup)
                {
                    //why did I make two lists and why did I do it so.. weird. Oh well.
                    b.NeedsCleanup = false;
                }
                
            }
            foreach (Battle b in _battleQueue)
            {
                    if (players.First(p => p.Id == b.PlayerIDs[0]).Level == players.First(p => p.Id == playerid).Level)
                    {
                        b.PlayerIDs[1] = playerid;
                        b.Start(players);
                        _battles.Add(b);
                        return;
                    }
                }
                Battle battle = new Battle();
            battle.PlayerIDs[0] = playerid;
                _battleQueue.Add(battle);
            
        }

        public static Battle GetBattle(int playerid)
        {
            return _battles.FirstOrDefault(b => b.PlayerIDs.Any(p => p.Equals(playerid)));
        }

        public static bool IsNotFighting(int pid)
        {
            return !_battleQueue.Any(b => b.PlayerIDs.Any(i => i.GetValueOrDefault() == pid));
        }

        public static void Fight(int characterId, int playerid)
        {
            GetBattle(playerid).Fight(characterId);
        }
    }
}
