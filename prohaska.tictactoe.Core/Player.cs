using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohaska.tictactoe.Core
{
    public class Player : IPlayer
    {
        string _icon;
        public string Icon
        {
            get { return _icon?[0].ToString(); }
            set { _icon = value; }
        }

        public string Name { get; set; }
    }
}
