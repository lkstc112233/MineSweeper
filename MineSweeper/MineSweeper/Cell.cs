using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    enum BoardCellOuterStatus
    {
        Unreveled,
        Reveled,
        Marked,
    }

    enum BoardCellInnerStatus
    {
        Mine,
        Safe,
    }

    public class Cell
    {
        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x { get; set; }
        public int y { get; set; }
        private BoardCellInnerStatus m_innerStatus;
        private BoardCellOuterStatus m_outerStatus;
        public bool isMine
        {
            get { return m_innerStatus == BoardCellInnerStatus.Mine; }
        }

        public bool reveal()
        {
            if (m_outerStatus != BoardCellOuterStatus.Unreveled)
                return false;
            m_outerStatus = BoardCellOuterStatus.Reveled;
            return true;
        }

        public void tryMark()
        {
            switch(m_outerStatus)
            {
                case BoardCellOuterStatus.Unreveled:
                    m_outerStatus = BoardCellOuterStatus.Marked;
                    return;
                case BoardCellOuterStatus.Marked:
                    m_outerStatus = BoardCellOuterStatus.Unreveled;
                    return;
                default:
                    return;
            }
        }
    }
}
