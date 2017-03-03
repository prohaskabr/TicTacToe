using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohaska.tictactoe.Core
{
    public interface IBoard
    {
        IPlayer PlayerOne { get; set; }
        IPlayer PlayerTwo { get; set; }
        bool ReadyToStart();
        Dictionary<string, IPlayer> Spot { get; set; }
        void Start();
        void SetSpot(string spot, IPlayer playerOne);
        List<List<string>> GetValidRows();
        bool IsFinished { get; }

        IPlayer GetWonPlayer();
                     
        event PlayerWon PlayerWon;
    }
    
}

