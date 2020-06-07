using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogic
{
    //Player is part of the model which is used in the view
    [DataContract]
    public class Player: INotifyPropertyChanged
    {
        private string _firstName;

        [DataMember]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (_firstName == value) return;
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private int _score;

        [DataMember]
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        public Player(string firstName, int initialScoreValue)
        {
            FirstName = firstName;
            Score = initialScoreValue;
        }

        public void IncreaseScore()
        {
            Score += 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
