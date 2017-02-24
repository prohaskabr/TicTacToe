using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohaska.tictactoe.UI
{
    public interface IBoard
    {
        int GetPositions();
        IPlayer PlayerOne { get; set; }
        IPlayer PlayerTwo { get; set; }
        bool ReadyToStart();
        Dictionary<string, IPlayer> Spot { get; set; }
        void Start();
        void SetSpot(string spot, IPlayer playerOne);
    }

   
}
