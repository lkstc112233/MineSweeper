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
        private BoardCellInnerStatus m_innerStatus;
        private BoardCellOuterStatus m_outerStatus;
        
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
                    // Get Count;
                    return CellOutlookEnum.Revealed_0;
                return CellOutlookEnum.Unrevealed;
            }
        }

        public bool isMine
        {
            get { return m_innerStatus == BoardCellInnerStatus.Mine; }
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
                    OnPropertyChanged("Outlook");
                    m_outerStatus = BoardCellOuterStatus.Marked;
                    return;
                case BoardCellOuterStatus.Marked:
                    OnPropertyChanged("Outlook");
                    m_outerStatus = BoardCellOuterStatus.Unreveled;
                    return;
                default:
                    return;
            }
        }
    }
}
