using prohaska.tictactoe.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prohaska.tictactoe.UI.win
{
    public partial class Form1 : Form
    {
        IBoard _board;
        private int _playerTurn;

        public Form1()
        {
            InitializeComponent();
            _board = new Board();
            _board.PlayerWon += OnPlayerWon;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClearMoves();

            txtPlayerOne.Text = string.Empty;
            txtPlayerTwo.Text = string.Empty;
        }

        private void ClearMoves()
        {
            A1.Text = string.Empty;
            A2.Text = string.Empty;
            A3.Text = string.Empty;

            B1.Text = string.Empty;
            B2.Text = string.Empty;
            B3.Text = string.Empty;

            C1.Text = string.Empty;
            C2.Text = string.Empty;
            C3.Text = string.Empty;

            _playerTurn = 1;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                StartGame();
                MessageBox.Show($"{_board.PlayerOne.Name} starts!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void StartGame()
        {
            SetPayers();
            ClearMoves();
            _board.Start();

        }

        private void SetPayers()
        {
            if (!string.IsNullOrEmpty(txtPlayerOne.Text))
                _board.PlayerOne = new Player() { Name = txtPlayerOne.Text };

            if (!string.IsNullOrEmpty(txtPlayerTwo.Text))
                _board.PlayerTwo = new Player() { Name = txtPlayerTwo.Text };


        }

        private void Button_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string spot = btn.Name;
                MakeAPlayerMove(btn, spot);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MakeAPlayerMove(Button btn, string spot)
        {
            var player = GetCurrentPlayer();

            _board.SetSpot(spot, player);
            UpdateScreen(btn, player);
            SetNextPlayer();
        }

        private void UpdateScreen(Button btn, IPlayer player)
        {
            btn.Text = GetTextForPlayer(player);
        }

        private string GetTextForPlayer(IPlayer player)
        {
            return player == _board.PlayerOne ? "X" : "O";
        }


        private IPlayer GetCurrentPlayer()
        {
            IPlayer result;
            switch (_playerTurn)
            {
                case 1:
                    result = _board.PlayerOne;
                    break;
                case 2:
                    result = _board.PlayerTwo;
                    break;
                default:
                    result = new Player() { Name = "Undefined" };
                    break;
            }
            return result;
        }

        private void SetNextPlayer()
        {
            _playerTurn = _playerTurn == 1 ? 2 : 1;
        }

        private void OnPlayerWon(PlayerWonEventArgs e)
        {
            Task.Run(()=> MessageBox.Show($"{e.Player.Name} Win!"));
        }
    }
}
