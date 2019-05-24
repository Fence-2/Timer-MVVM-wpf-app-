using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace Timer__MVVM_proj_by_Dmitrii_Chistyakov_.ViewModels
{
    class MainViewModel : ObservableObject
    {
        DateTime TimerTime = new DateTime();
        bool trigger = true;
        int lap_num = 1;
        string _LabelText = "00:00:00";
        DispatcherTimer timer = new DispatcherTimer();
        private readonly ObservableCollection<string> _ListBoxItems = new ObservableCollection<string>();

        public string LabelText
        {
            get { return _LabelText; }
            set
            {
                _LabelText = value;
                RaisePropertyChangedEvent("LabelText");
            }
        }

        public IEnumerable<string> ListBoxItems
        {
            get { return _ListBoxItems; }
        }

        private void AddToListBox(string item)
        {
            _ListBoxItems.Add(item);
        }

        private void ClearListBox()
        {
            _ListBoxItems.Clear();
        }

        public ICommand start_Command
        {
            get { return new RelayCommand(start_Click); }
        }

        public ICommand pause_Command
        {
            get { return new RelayCommand(pause_Click); }
        }

        public ICommand lap_Command
        {
            get { return new RelayCommand(lap_Click); }
        }

        public ICommand reset_Command
        {
            get { return new RelayCommand(reset_Click); }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            TimerTime = TimerTime.AddSeconds(1);
            LabelText = (TimerTime).ToString("HH:mm:ss");
        }

        public void start_Click()
        {
            if (trigger)
            {
                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Start();
                trigger = false;
            }
            timer.Start();
        }

        private void pause_Click()
        {
            timer.Stop();
        }

        private void lap_Click()
        {
            AddToListBox($"Круг {lap_num}: " + TimerTime.ToString("HH:mm:ss"));
            lap_num++;
        }

        private void reset_Click()
        {
            timer.Stop();
            TimerTime = DateTime.MinValue;
            LabelText = TimerTime.ToString("HH:mm:ss");
            lap_num = 1;
            ClearListBox();
            trigger = true;
        }
    }
}
