using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MemoryGameData;
using MemoryGameLogic;
using MemoryGameServer;
using IMemoryGameService = MemoryGameLogic.IMemoryGameService;

namespace MemoryGameClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMemoryGameService _memoryGameProxy;
        private ChannelFactory<IMemoryGameService> client;
        private DispatcherTimer _firstCardTimer;
        private DispatcherTimer _secondCardTimer;
        private DispatcherTimer _gameTimer;
        private DispatcherTimer _startTimer;
        private DispatcherTimer _matchTimer;
        private Image _firstClickedImage;
        private Image _secondClickedImage;
       // private GameViewModel _viewModel;
        private bool _firstPlayerTurn;
        private bool _wasMatch;
        private bool _secondCardWasClicked;
        private bool _wasStartClicked;
        private string uri = "E:/Skola-moja/Potko/MemoryGame/MemoryGameClient/Images/cover.png";
        public MainWindow()
        {
            InitializeComponent();
            client = new ChannelFactory<IMemoryGameService>("WcfConfigKlienta");
            _memoryGameProxy = client.CreateChannel();
            _gameTimer = new DispatcherTimer();
            _gameTimer.Tick += GameTimerTick;
            _gameTimer.Interval = TimeSpan.FromSeconds(1);
            _gameTimer.Start();
            _startTimer = new DispatcherTimer();
            _startTimer.Tick += StartTimerTick;
            _startTimer.Interval = TimeSpan.FromSeconds(1);
            _startTimer.Start();
            _firstCardTimer = new DispatcherTimer();
            _firstCardTimer.Tick += FirstCardTimerTick;
            _firstCardTimer.Interval = TimeSpan.FromSeconds(1);
            _secondCardTimer = new DispatcherTimer();
            _secondCardTimer.Tick += SecondCardTimerTick;
            _secondCardTimer.Interval = TimeSpan.FromSeconds(1);
            //_matchTimer = new DispatcherTimer();
            //_matchTimer.Tick += MatchTimerTick;
            //_matchTimer.Interval = TimeSpan.FromSeconds(3);
            //_matchTimer.Start();

            //view model su len karty
            //_viewModel = new GameViewModel();
            //tu sa bude context mapovat na hracov, ktori su na serveri -> memoryGameProxy.GetPlayer, SetPlayer
            //FirstNameLabelPlayer1.DataContext = _viewModel.Player1;
            //ScorePlayer1.DataContext = _viewModel.Player1;
            //FirstNameLabelPlayer2.DataContext = _viewModel.Player2;
            //ScorePlayer2.DataContext = _viewModel.Player2;
            //var player = _memoryGameProxy.GetPlayer1();
            //FirstNameLabelPlayer1.DataContext = _memoryGameProxy.GetPlayer1();
        }

        //private void MatchTimerTick(object sender, EventArgs e)
        //{
        //    //kazde 3 sekundy kontroluje, ci bola zhoda karticiek
        //    if (!_memoryGameProxy.GetMatch())
        //    {
        //        //ak nie, zakryje druhu karticku
        //        var name = _memoryGameProxy.GetSecondCardName();
        //        if (name != "")
        //        {
        //            var image = (Image)FindName(name);
        //            image.Source = new BitmapImage(new Uri(uri));
        //        }
        //    }
        //}

        private void GameTimerTick(object sender, EventArgs e)
        {
            if (_memoryGameProxy.GetRestart())
            {
                FillCoverPictures();
            }
            //kontroluje, ci bola kliknuta 1.karta, ak ano nastavi jej obrazok a spusti timer
            if (_memoryGameProxy.GetFirstCardTimer()) //1.karta bola kliknuta
            {
                var name = _memoryGameProxy.GetFirstCardName();
                var index = GetImageIndexForCard(name);
                var image = (Image) FindName(name);
                image.Source = _memoryGameProxy.GetCardImages().Images[index];
                _firstCardTimer.Start();
            }
            else
            {
                _firstCardTimer.Stop();
            }
            //kontroluje, ci bola kliknuta 2.karta, ak ano nastavi jej obrazok a spusti timer
            if (_memoryGameProxy.GetSecondCardTimer()) //2.karta bola kliknuta
            {
                var name = _memoryGameProxy.GetSecondCardName();
                var index = GetImageIndexForCard(name);
                var image = (Image) FindName(name);
                image.Source = _memoryGameProxy.GetCardImages().Images[index];
                _secondCardTimer.Start();
            }
            else
            {
                _secondCardTimer.Stop();
                
            }

            if (!_memoryGameProxy.GetMatch())
            {
                var name = _memoryGameProxy.GetFirstCardName();
                if (name != "")
                {
                    var image = (Image)FindName(name);
                    image.Source = new BitmapImage(new Uri(uri));
                }
                
            }

            
        }

        private void StartTimerTick(object sender, EventArgs e)
        {
            string player1Name = _memoryGameProxy.GetPlayer1().FirstName;
            string player2Name = _memoryGameProxy.GetPlayer2().FirstName;
            if (player1Name  != "Player1")
            {
                FirstNameLabelPlayer1.Content = player1Name;
            }
            if (player2Name != "Player2")
            {
                FirstNameLabelPlayer2.Content = player2Name;
                WhoseTurnLabel.Content = player1Name + "'s turn";
                //na server posielat len indexy jednotlivych obrazkov a nie cele obrazky, lebo pretecie buffer
                if (_memoryGameProxy.GetCardImages().Images.Count > 0)
                {
                    FillCoverPictures();
                    _startTimer.Stop();
                }
            }
        }

        private void FillCoverPictures()
        {
            Image01.Source = new BitmapImage(new Uri(uri));
            Image02.Source = new BitmapImage(new Uri(uri));
            Image03.Source = new BitmapImage(new Uri(uri));
            Image04.Source = new BitmapImage(new Uri(uri));
            Image05.Source = new BitmapImage(new Uri(uri));
            Image06.Source = new BitmapImage(new Uri(uri));
            Image07.Source = new BitmapImage(new Uri(uri));
            Image08.Source = new BitmapImage(new Uri(uri));
            Image09.Source = new BitmapImage(new Uri(uri));
            Image10.Source = new BitmapImage(new Uri(uri));
            Image11.Source = new BitmapImage(new Uri(uri));
            Image12.Source = new BitmapImage(new Uri(uri));
            Image13.Source = new BitmapImage(new Uri(uri));
            Image14.Source = new BitmapImage(new Uri(uri));
            Image15.Source = new BitmapImage(new Uri(uri));
            Image16.Source = new BitmapImage(new Uri(uri));
            Image17.Source = new BitmapImage(new Uri(uri));
            Image18.Source = new BitmapImage(new Uri(uri));
            //Console.WriteLine(Image1.Source.Height);
            //Image2.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image3.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image4.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image5.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image6.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image7.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image8.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image9.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image10.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image11.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image12.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image13.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image14.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image15.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image16.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image17.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
            //Image18.Source = new BitmapImage(new Uri(uri, UriKind.Relative));

        }

        private void FirstCardTimerTick(object sender, EventArgs e)
        {
            //kazdu sekundu sa kontroluje, ci sa text na serveri zmenil 
            //if (memoryGameProxy.GetMessage() != "Hello ")
            //{  
            //ak ano, do labelu sa nastavi text zo servera
            //label.Content = memoryGameProxy.GetMessage();
            // }

            //secondCard != null namiesto secondCardWasClicked
           if (_memoryGameProxy.GetSecondCardName() != "") //caka, kym nebude kliknuta druha karticka
           {
                //tu prebehne porovnanie prvej s druhou a ak nie su rovnake, prva zmizne
                //tu zavolam metodu GetImageSourceHashCode(_memoryGameProxy.GetFirstImageName())
               if (GetImageSourceHashCode(_memoryGameProxy.GetFirstCardName()) == GetImageSourceHashCode(_memoryGameProxy.GetSecondCardName()))
               //if (_secondClickedImage.Source.GetHashCode() == _firstClickedImage.Source.GetHashCode())
               {
                   _memoryGameProxy.SetFirstCardTimer(false);
                   _memoryGameProxy.SetFirstCardName("");
                    //_firstClickedImage = null;
                   _memoryGameProxy.SetMatch(true);
                    //_wasMatch = true;
               }
               else
               {
                    // _firstClickedImage.Source = new BitmapImage(new Uri(uri));
                   _memoryGameProxy.SetFirstCardTimer(false);
                   _memoryGameProxy.SetFirstCardName("");
                   _memoryGameProxy.SetMatch(false);
                    //_firstCardTimer.Stop();
                    //_firstClickedImage = null;
                }
                //    _secondCardWasClicked = false;
            }
            
        }
        private void SecondCardTimerTick(object sender, EventArgs e)
        {
            //kazdu sekundu sa kontroluje, ci sa text na serveri zmenil 
            //if (memoryGameProxy.GetMessage() != "Hello ")
            //{  
            //ak ano, do labelu sa nastavi text zo servera
            //label.Content = memoryGameProxy.GetMessage();
            // }

            //if (_secondTime.AddSeconds(1) > DateTime.Now)
           // {
           if (_memoryGameProxy.GetMatch())
           {
               _memoryGameProxy.SetSecondCardTimer(false);
               _memoryGameProxy.SetSecondCardName("");

                if (_memoryGameProxy.GetFirstPlayerTurn())
                {
                    _memoryGameProxy.GetPlayer1().IncreaseScore();
                    WhoseTurnLabel.Content = _memoryGameProxy.GetPlayer1().FirstName + "'s turn";
                }
                else
                {
                   _memoryGameProxy.GetPlayer2().IncreaseScore();
                   WhoseTurnLabel.Content = _memoryGameProxy.GetPlayer2().FirstName + "'s turn";
                }
                _memoryGameProxy.SetMatch(false);
                //_secondClickedImage = null;
                // _secondCardWasClicked = false;

                // _wasMatch = false;
                //_secondCardTimer.Stop();
                //_secondClickedImage = null;
                //_secondCardWasClicked = false;
                //_wasMatch = false;
            }
            else //nebola zhoda obrazkov
            {
                //zakryje druhy obrazok
                var name = _memoryGameProxy.GetSecondCardName();
                if (name != "")
                {
                    var image = (Image)FindName(name);
                    image.Source = new BitmapImage(new Uri(uri));
                }
               // _secondClickedImage.Source = new BitmapImage(new Uri(uri));
                 //   _secondCardTimer.Stop();
                 //stopne timer
                _memoryGameProxy.SetSecondCardTimer(false);
                //vyresetuje druhu kartu
                _memoryGameProxy.SetSecondCardName("");
                //_secondClickedImage = null;
                    //_secondCardWasClicked = false;
                if (_memoryGameProxy.GetFirstPlayerTurn())
                {
                    _memoryGameProxy.SetFirstPlayerTurn(false);
                    WhoseTurnLabel.Content = _memoryGameProxy.GetPlayer2().FirstName + "'s turn";
                }
                else
                {
                    _memoryGameProxy.SetFirstPlayerTurn(true);
                    //_firstPlayerTurn = true;
                    WhoseTurnLabel.Content = _memoryGameProxy.GetPlayer2().FirstName + "'s turn";
                }
                    //_firstPlayerTurn = !_firstPlayerTurn;
                    
                    //_secondPlayerTurn = true;
            }
                
               
           // }

        }

        private void StartGameButtonWasClicked(object sender, RoutedEventArgs e)
        {
            //text z textboxu sa posle na server
            // memoryGameProxy.SetMessage(textBox.Text);

            //_wasStart je pre jedneho hraca, to zostava tu 
            if (_wasStartClicked) return;
            _wasStartClicked = true;
            var firstName = FirstNameTextBox.Text;
            //tu sa potom bude kontrolovat ci tam je prvy hrac na SERVERI !
            if (_memoryGameProxy.GetPlayer1().FirstName == "Player1")
            {
                _memoryGameProxy.SetPlayer1FirstName(firstName);
            }
            else if (_memoryGameProxy.GetPlayer2().FirstName == "Player2")
            {
                _memoryGameProxy.SetPlayer2FirstName(firstName);
                //az ked pride druhy hrac, karty sa zamiesaju a zakryju cover obrazkom
                _memoryGameProxy.PrepareCardImages();
            }
            
        }

        private void Image0_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }

        private void Image2_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }

        private void SomeCardWasClicked(Image image)
        {
            if (_memoryGameProxy.GetFirstCardName() == "")
            {
                //nastav jej meno
                _memoryGameProxy.SetFirstCardName(image.Name);
                //nastav, ze prva karta bola kliknuta
                _memoryGameProxy.SetFirstCardClicked(true);
                //posli na server, ze treba odstartovat timer pre prvu kartu
                _memoryGameProxy.SetFirstCardTimer(true);
            }
           // if (_firstClickedImage == null)
           // {
               // var myImage = (Image)FindName("Image01"); 
              //  _firstClickedImage = image;
                //tu nastavi karte na serveri, ze je kliknuta, ale nebude jej este priamo tu nastavovat obrazok
                //obrazok jej nastavi timer, ktory bude kazdu sekundu sledovat stav vsetkych kariet a nastavovat im obrazky podla ich stavu
                //pribudne nova trieda Card - bool uhadnuta, bool kliknuta, BitmapImage image a string name
                //na serveri bude List<Card>
                //timer bude sledovat ich stavy - uhadnuta, kliknuta a podla toho menit kazdej images
                //uhadnutym sa nastavi uhadnuty obrazok a vymazu sa z tohto listu, nech zbytocne nezavadzaju
                //pri kliknuti na kartu pozname jej meno = podla mena sa kazdej karte nastavi stav
                //logika, ktora je tu vo view zostava rovnaka s tym rozdielom, ze stavy a obrazky karticiek sa nebudu menit tu ale
                //NA SERVERI !!!!!
                //ALEBO
                //sa na server posle len prva a druha karticka a timer nastavi ich obrazky podla ich stavu !!!!!
//            }
            else if (_memoryGameProxy.GetSecondCardName() == "")
            {
                //nastav jej meno
                _memoryGameProxy.SetSecondCardName(image.Name);
                //nastav, ze prva karta bola kliknuta
                _memoryGameProxy.SetSecondCardClicked(true);
                //posli na server, ze treba odstartovat timer pre prvu kartu
                _memoryGameProxy.SetSecondCardTimer(true);

                //_secondClickedImage = image;
                //_secondClickedImage.Source = _viewModel.Images.Images[index];
                //_secondCardTimer = new DispatcherTimer();
                //_secondCardTimer.Tick += SecondCardTimerTick;
                //_secondCardTimer.Interval = TimeSpan.FromSeconds(1);
                //_secondCardWasClicked = true;
                //_secondCardTimer.Start();
            }
           
               
        }

        private void ResetGame()
        {
            //kto je na rade tiez PRIDAT NA SERVER
            _firstPlayerTurn = true;
            _memoryGameProxy.SetFirstCardClicked(false);
            //_firstClickedImage = null;
            _memoryGameProxy.SetSecondCardClicked(false);
            //_secondClickedImage = null;
            //match PRIDAT NA SERVER
            _wasMatch = false;
            //asi netreba, ked je secondClickedImage
            _secondCardWasClicked = false;
        }

        private void Image03_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }

        private void Image04_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }

        private void Image05_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }

        private void Image06_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }

        private void Image07_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }
        private void Image08_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }
        private void Image09_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }
        private void Image10_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }
        private void Image11_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }
        private void Image12_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }
        private void Image13_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }
        private void Image14_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }
        private void Image15_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }
        private void Image16_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }
        private void Image17_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }
        private void Image18_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SomeCardWasClicked(sender as Image);
        }

        private void ExitGameButton_OnClickGameButtonWasClicked(object sender, RoutedEventArgs e)
        {
            _gameTimer.Stop();
            _memoryGameProxy.ClearPlayers();
            Close();
        }

        private void ButtonRestart_OnClick(object sender, RoutedEventArgs e)
        {
            ResetGame();
            _memoryGameProxy.ResetScore();
            //ten kto stlaci restart, posle na server nove rozmiestnenie karticiek
            _memoryGameProxy.PrepareCardImages();
            _memoryGameProxy.SetRestart(true);
        }

        private int GetImageSourceHashCode(string name)
        {
            var index = GetImageIndexForCard(name);
            return _memoryGameProxy.GetCardImages().Images[index].GetHashCode();
        }

        private int GetImageIndexForCard(string name)
        {
            var sufix = name.Substring(5, 2);
            char zero = Convert.ToChar("0");
            var last = sufix.Last().ToString();
            var index = 0;
            if (sufix.First() == zero)
            {
                index = int.Parse(last) - 1;
            }
            else
            {
                index = int.Parse(sufix) - 1;
            }
            return index;
        }
    }
}
