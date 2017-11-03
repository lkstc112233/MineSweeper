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
        int CellRemaining;
        bool generated;

        public List<List<Cell>> m_board { get; set; }

        public Board(int width, int height, int mines)
        {
            this.width = width;
            this.height = height;
            this.mines = mines;
            CellRemaining = width * height - mines;
            generated = false;
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

        private void PlusMineCounter(int x, int y)
        {
            if (x - 1 >= 0 && y - 1 >= 0)
                m_board[x - 1][y - 1].mineNeighbors += 1;
            if (x - 1 >= 0)
                m_board[x - 1][y].mineNeighbors += 1;
            if (x - 1 >= 0 && y + 1 < height)
                m_board[x - 1][y + 1].mineNeighbors += 1;
            if (y - 1 >= 0)
                m_board[x][y - 1].mineNeighbors += 1;
            if (y + 1 < height)
                m_board[x][y + 1].mineNeighbors += 1;
            if (x + 1 < width && y - 1 >= 0)
                m_board[x + 1][y - 1].mineNeighbors += 1;
            if (x + 1 < width)
                m_board[x + 1][y].mineNeighbors += 1;
            if (x + 1 < width && y + 1 < height)
                m_board[x + 1][y + 1].mineNeighbors += 1;
        }

        private void Generate(int x, int y)
        {
            Random ran = new Random();
            for (int Count = 0; Count < mines; ++Count)
            {
                int xg = ran.Next() % width;
                int yg = ran.Next() % height;
                if ((Math.Abs(xg - x) <= 1 && Math.Abs(yg - y) <= 1) || m_board[xg][yg].isMine)
                {
                    Count--;
                    continue;
                }
                PlusMineCounter(xg, yg);
                m_board[xg][yg].setMine();
            }
            generated = true;
        }

        public bool Reveal(int x, int y)
        {
            // edge check should be here.

            if (!generated)
                Generate(x, y);

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
