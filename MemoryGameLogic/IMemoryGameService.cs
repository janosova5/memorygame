using System.ServiceModel;

namespace MemoryGameLogic
{
    [ServiceContract]
    public interface IMemoryGameService
    {
        [OperationContract]
        //string GetMessage();
        Player GetPlayer1();

        [OperationContract]
        //void SetMessage(string text);
        Player GetPlayer2();

        [OperationContract]
        //void SetMessage(string text);
        void SetPlayer1FirstName(string name);

        [OperationContract]
        //void SetMessage(string text);
        void SetPlayer2FirstName(string name);

        [OperationContract]
        //void SetMessage(string text);
        void ClearPlayers();

        [OperationContract]
        //void SetMessage(string text);
        void ResetScore();

        [OperationContract]
        void SetFirstCardClicked(bool value);

        [OperationContract]
        void SetFirstCardGuessed(bool value);

        [OperationContract]
        void SetSecondCardClicked(bool value);

        [OperationContract]
        void SetSecondCardGuessed(bool value);

        [OperationContract]
        void SetFirstCardName(string name);

        [OperationContract]
        string GetFirstCardName();

        [OperationContract]
        void SetSecondCardName(string name);

        [OperationContract]
        string GetSecondCardName();

        [OperationContract]
        void PrepareCardImages();

        [OperationContract]
        CardImages GetCardImages();

        [OperationContract]
        void SetRestart(bool value);

        [OperationContract]
        bool GetRestart();

        [OperationContract]
        void SetMatch(bool value);

        [OperationContract]
        bool GetMatch();

        [OperationContract]
        void SetFirstCardTimer(bool value);

        [OperationContract]
        bool GetFirstCardTimer();

        [OperationContract]
        void SetSecondCardTimer(bool value);

        [OperationContract]
        bool GetSecondCardTimer();

        [OperationContract]
        void SetFirstPlayerTurn(bool value);

        [OperationContract]
        bool GetFirstPlayerTurn();

        [OperationContract]
        void SetSecondPlayerTurn(bool value);

        [OperationContract]
        bool GetSecondPlayerTurn();           

    }
}
