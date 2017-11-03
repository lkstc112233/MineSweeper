using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    enum BoardCellOuterStatus
    {
        Unrevealed,
        Revealed,
        Marked,
    }

    enum BoardCellInnerStatus
    {
        Mine,
        Safe,
    }
    
    public enum CellOutlookEnum
    {
        Mine,
        Marked,
        Unrevealed,
        Revealed_0,
        Revealed_1,
        Revealed_2,
        Revealed_3,
        Revealed_4,
        Revealed_5,
        Revealed_6,
        Revealed_7,
        Revealed_8,
    }

    public class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x { get; set; }
        public int y { get; set; }
        public int mineNeighbors = 0;
        private BoardCellInnerStatus m_innerStatus = BoardCellInnerStatus.Safe;
        private BoardCellOuterStatus m_outerStatus = BoardCellOuterStatus.Unrevealed;

        public CellOutlookEnum Outlook
        {
            get
            {
                if (m_outerStatus == BoardCellOuterStatus.Unrevealed)
                    return CellOutlookEnum.Unrevealed;
                if (m_outerStatus == BoardCellOuterStatus.Marked)
                    return CellOutlookEnum.Marked;
                if (m_innerStatus == BoardCellInnerStatus.Mine)
                    return CellOutlookEnum.Mine;
                if (m_innerStatus == BoardCellInnerStatus.Safe)
                    return CellOutlookEnum.Revealed_0 + mineNeighbors;
                return CellOutlookEnum.Unrevealed;
            }
        }

        public bool isMine
        {
            get { return m_innerStatus == BoardCellInnerStatus.Mine; }
        }
        public bool isMarked
        {
            get { return m_outerStatus == BoardCellOuterStatus.Marked; }
        }
        public bool isRevealed
        {
            get { return m_outerStatus == BoardCellOuterStatus.Revealed; }
        }

        public void setMine()
        {
            m_innerStatus = BoardCellInnerStatus.Mine;
        }

        public bool reveal()
        {
            if (m_outerStatus != BoardCellOuterStatus.Unrevealed)
            {
                OnPropertyChanged("Outlook");
                return false;
            }
            m_outerStatus = BoardCellOuterStatus.Revealed;
            OnPropertyChanged("Outlook");
            return true;
        }

        public void tryMark()
        {
            switch(m_outerStatus)
            {
                case BoardCellOuterStatus.Unrevealed:
                    m_outerStatus = BoardCellOuterStatus.Marked;
                    OnPropertyChanged("Outlook");
                    return;
                case BoardCellOuterStatus.Marked:
                    m_outerStatus = BoardCellOuterStatus.Unrevealed;
                    OnPropertyChanged("Outlook");
                    return;
                default:
                    return;
            }
        }
    }
}
