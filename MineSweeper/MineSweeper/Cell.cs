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

        private CellOutlookEnum m_Outlook;
        public CellOutlookEnum Outlook
        {
            get { return m_Outlook; }
            set
            {
                m_Outlook = value;
                OnPropertyChanged("Outlook");
            }
        }

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
