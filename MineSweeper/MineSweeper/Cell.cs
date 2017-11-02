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

    class Cell
    {
        int x;
        int y;
        private BoardCellInnerStatus m_innerStatus;
        private BoardCellOuterStatus m_outerStatus;
        bool isMine
        {
            get { return m_innerStatus == BoardCellInnerStatus.Mine; }
        }
    }
}
