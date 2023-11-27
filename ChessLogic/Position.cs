namespace ChessLogic
{
    public class Position // square on the board. Rows incress from top to bottum and colums from left to right. So the top left square is (0,0)
    {
        public int Row { get; }
        public int Column { get; }


        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;

        }

        public Player SquareColor() // Using the Player to determen color.
        {
            if(Row + Column % 2 == 0) // if row is even
            {
                return Player.White;
            }
            else // if row is odd
            {
                return Player.Black;
            }
        }

        public override bool Equals(object obj) // Ctrl+. generate Get has code and operators. 
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public static Position operator +(Position pos, Direction dir) 
        {
            return new Position(pos.Row + dir.RowDelta, pos.Column + dir.ColumnDelta);
        }
    }
}




