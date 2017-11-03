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
        Unreveled,
        Reveled,
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
        private BoardCellOuterStatus m_outerStatus = BoardCellOuterStatus.Unreveled;
        
        public CellOutlookEnum Outlook
        {
            get
            {
                if (m_outerStatus == BoardCellOuterStatus.Unreveled)
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

        public void setMine()
        {
            m_innerStatus = BoardCellInnerStatus.Mine;
        }

        public bool reveal()
        {
            if (m_outerStatus != BoardCellOuterStatus.Unreveled)
            {
                OnPropertyChanged("Outlook");
                return false;
            }
            m_outerStatus = BoardCellOuterStatus.Reveled;
            OnPropertyChanged("Outlook");
            return true;
        }

        public void tryMark()
        {
            switch(m_outerStatus)
            {
                case BoardCellOuterStatus.Unreveled:
                    m_outerStatus = BoardCellOuterStatus.Marked;
                    OnPropertyChanged("Outlook");
                    return;
                case BoardCellOuterStatus.Marked:
                    m_outerStatus = BoardCellOuterStatus.Unreveled;
                    OnPropertyChanged("Outlook");
                    return;
                default:
                    return;
            }
        }
    }
}
