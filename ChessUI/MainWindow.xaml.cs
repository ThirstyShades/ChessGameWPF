using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLogic;


namespace ChessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8,8];

        private readonly System.Windows.Shapes.Rectangle[,] highlights = new System.Windows.Shapes.Rectangle[8,8];
        private readonly Dictionary<Position, Move> moveCashe = new Dictionary<Position, Move>();

        private GameState gameState;
        private Position selectedPos = null;
        

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
        }

        private void InitializeBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieceImages[r, c] = image;
                    PieceGrid.Children.Add(image);

                    System.Windows.Shapes.Rectangle highlight = new System.Windows.Shapes.Rectangle();
                    highlights[r, c] = highlight;
                    HighLightGrid.Children.Add(highlight);

                }
            }
        }

        private void DrawBoard(Board board)
        {
            for(int r = 0; r < 8; r++) 
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    pieceImages[r,c].Source = Images.GetImage(piece);
                }
                    
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e) // this method is called when somoneclicks somwhere on teh board
        {
            System.Windows.Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);

            if(selectedPos == null)
            {
                OnFromPositionsSelected(pos);

            }
            else
            {
                OnToPositionsSelected(pos);
            }
        }

        private Position ToSquarePosition(System.Windows.Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);  
        }

        private void OnFromPositionsSelected(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPiece(pos);

            if(moves.Any())
            {
                selectedPos = pos;
                CasheMoves(moves);
                ShowHighlights();
            }
        }

        private void OnToPositionsSelected(Position pos)
        {
            selectedPos = null;
            HideHighlights();

            if(moveCashe.TryGetValue(pos, out Move move))
            {
                HandleMove(move);
            }
        }

        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
        }


        private void CasheMoves(IEnumerable<Move> moves)
        {
            moveCashe.Clear();

            foreach(Move move in moves)
            {
                moveCashe[move.ToPos] = move;
            }
        }

        private void ShowHighlights()
        {
            System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(150, 125, 255, 125);

            foreach(Position to in moveCashe.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);

            }
        }

        private void HideHighlights()
        {
            foreach(Position to in moveCashe.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }



    }
}
