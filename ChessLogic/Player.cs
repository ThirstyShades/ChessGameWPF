namespace ChessLogic
{
    public enum Player
    {
        None,
        White,
        Black,

    }

    public static class PlayerExtensions
    {
        public static Player Opponent(this Player player)
        {
            return player switch // switch case statment changed by VS
            {
                Player.White => Player.Black, // if player is white return black as oppenent
                Player.Black => Player.White, // if player is black return white as oppenent
                _ => Player.None, // defalt case is none
            };
        }
    }
}


