using System;
using Shadow_Arena.Models;

namespace Shadow_Arena.BattleSystem
{
    public class BattleCharacter
    {
        int playerId;
        int characterId;
        string charName;
        Stat stats;
        private int _HP;
        DateTime lastAttack;

        public int PlayerId
        {
            get { return playerId; }
            set { playerId = value; }
        }

        public int CharacterId
        {
            get { return characterId; }
            set { characterId = value; }
        }

        public string CharName
        {
            get { return charName; }
            set { charName = value; }
        }

        public Stat Stats
        {
            get { return stats; }
            set { stats = value; }
        }

        public DateTime LastAttack
        {
            get { return lastAttack; }
            set { lastAttack = value; }
        }

        public int HP
        {
            get
            {
                return _HP;
            }

            set
            {
                _HP = value;
            }
        }
    }
}