using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohaska.tictactoe.Core
{

    public class TicTacToeException : Exception
    {
        protected string _message;

        public TicTacToeException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }

    public class NoAvailableSpotException : TicTacToeException
    {
        public NoAvailableSpotException(string message = "This spot is not available.") : base(message)
        {
        }
    }

    public class NotEnoughPlayersException : TicTacToeException
    {
        public NotEnoughPlayersException(string message = "We need two players to start.") : base(message)
        {
        }
    }

    public class NotPlayerTurnException : TicTacToeException
    {
        public NotPlayerTurnException(string message = "It is not your turn."):base(message)
        {         
        }        
    }
}