using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Board
    {
        int width;
        int height;
        int mines;

        List<List<Cell>> m_board;

        public Board(int width, int height, int mines)
        {
            this.width = width;
            this.height = height;
            this.mines = mines;
            m_board = new List<List<Cell>>();
            for (int i = 0; i < width; ++i)
            {
                List<Cell> column = new List<Cell>();
                for (int j = 0; j < height; ++j)
                {
                    column.Add(new Cell(i, j));
                }
                m_board.Add(column);
            }
        }

        public bool Reveal(int x, int y)
        {
            // edge check should be here.

            if(m_board[x][y].reveal())
                return !m_board[x][y].isMine;
            return true;
        }

        public void TryMark(int x, int y)
        {
            // edge check should be here.

            m_board[x][y].tryMark();
        }
    }
}
