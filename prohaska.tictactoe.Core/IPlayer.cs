﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prohaska.tictactoe.Core
{
    public interface IPlayer
    {
        string Icon { get; set; }
        string Name { get; set; }
    }
}
