using MemoryGameLogic;

namespace MemoryGameServer
{

    public class MemoryGameService : IMemoryGameService
    {
        //implementacie metod z interfacu

        private static readonly Players PlayersOnServer;
        private static readonly Card FirstCard;
        private static readonly Card SecondCard;
        private static readonly CardImages Images;
        private static readonly Data AdditionalData;

        static MemoryGameService()
        {
            PlayersOnServer = new Players();
            FirstCard = new Card();
            SecondCard = new Card();
            Images = new CardImages();
            AdditionalData = new Data();
        }

        public Player GetPlayer1()
        {
            lock (PlayersOnServer)
            {
                return PlayersOnServer.Player1;
            }
        }

        public Player GetPlayer2()
        {
            lock (PlayersOnServer)
            {
                return PlayersOnServer.Player2;
            }

        }

        public void SetPlayer1FirstName(string name)
        {
            lock (PlayersOnServer)
            {
                PlayersOnServer.Player1.FirstName = name;
            }

        }

        public void SetPlayer2FirstName(string name)
        {
            lock (PlayersOnServer)
            {
                PlayersOnServer.Player2.FirstName = name;
            }

        }

        public void ClearPlayers()
        {
            lock (PlayersOnServer)
            {
                PlayersOnServer.ClearPlayers();
            }

        }

        public void ResetScore()
        {
            lock (PlayersOnServer)
            {
                PlayersOnServer.ResetScore();
            }

        }

        public void SetFirstCardClicked(bool value)
        {
            lock (FirstCard)
            {
                FirstCard.Clicked = value;
            }
        }

        public void SetFirstCardGuessed(bool value)
        {
            lock (FirstCard)
            {
                FirstCard.Guessed = value;
            }
        }

        public void SetSecondCardClicked(bool value)
        {
            lock (SecondCard)
            {
                SecondCard.Clicked = value;
            }

        }

        public void SetSecondCardGuessed(bool value)
        {
            lock (SecondCard)
            {
                SecondCard.Guessed = value;
            }
        }

        public void SetFirstCardName(string name)
        {
            lock (FirstCard)
            {
                FirstCard.Name = name;
            }
        }

        public string GetFirstCardName()
        {
            lock (FirstCard)
            {
                return FirstCard.Name;
            }
        }


        public void SetSecondCardName(string name)
        {
            lock (SecondCard)
            {
                SecondCard.Name = name;
            }
        }

        public string GetSecondCardName()
        {
            lock (SecondCard)
            {
                return SecondCard.Name;
            }
        }

        public void PrepareCardImages()
        {
            lock (Images)
            {
                Images.ClearCards();
                Images.FillImages();
                Images.ShuffleList();
            }
        }

        public CardImages GetCardImages()
        {
            lock (Images)
            {
                return Images;
            }
        }

        public void SetRestart(bool value)
        {
            AdditionalData.Restart = value;
        }

        public bool GetRestart()
        {
            return AdditionalData.Restart;
        }

        public void SetMatch(bool value)
        {
            AdditionalData.WasMatch = value;
        }

        public bool GetMatch()
        {
            return AdditionalData.WasMatch;
        }

        public void SetFirstCardTimer(bool value)
        {
            AdditionalData.FirstCardTimerToRun = value;
        }

        public bool GetFirstCardTimer()
        {
            return AdditionalData.FirstCardTimerToRun;
        }

        public void SetSecondCardTimer(bool value)
        {
            AdditionalData.SecondCardTimerToRun = value;
        }

        public bool GetSecondCardTimer()
        {
            return AdditionalData.SecondCardTimerToRun;
        }

        public void SetFirstPlayerTurn(bool value)
        {
            AdditionalData.FirstPlayerTurn = value;
        }

        public bool GetFirstPlayerTurn()
        {
            return AdditionalData.FirstPlayerTurn;
        }

        public void SetSecondPlayerTurn(bool value)
        {
            AdditionalData.SecondPlayerTurn = value;
        }

        public bool GetSecondPlayerTurn()
        {
            return AdditionalData.SecondPlayerTurn;
        }
    }
}
