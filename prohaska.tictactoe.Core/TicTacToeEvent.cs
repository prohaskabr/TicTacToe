using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohaska.tictactoe.Core
{
    public delegate void PlayerWon(PlayerWonEventArgs e);
    public class PlayerWonEventArgs : EventArgs
    {
        public IPlayer Player { get; set; }
    }    
}