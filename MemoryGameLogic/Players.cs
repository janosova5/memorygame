using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogic
{
    public class Players
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Players()
        {
            Player1 = new Player("Player1", 0);
            Player2 = new Player("Player2", 0);
        }

        public void ClearPlayers()
        {
            Player1.FirstName = "Player1";
            Player2.FirstName = "Player2";
        }

        public void ResetScore()
        {
            Player1.Score = 0;
            Player2.Score = 0;
        }
    }
}
