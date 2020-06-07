using System.Runtime.Serialization;

namespace MemoryGameLogic
{
    [DataContract]
    public class Data
    {
        [DataMember]
        public bool Restart;

        [DataMember]
        public bool WasMatch;

        [DataMember]
        public bool FirstCardTimerToRun;

        [DataMember]
        public bool SecondCardTimerToRun;

        [DataMember]
        public bool FirstPlayerTurn;

        [DataMember]
        public bool SecondPlayerTurn;

        public Data()
        {
            Restart = false;
            WasMatch = false;
            FirstCardTimerToRun = false;
            SecondCardTimerToRun = false;
            FirstPlayerTurn = false;
            SecondPlayerTurn = false;
        }

    }
}
