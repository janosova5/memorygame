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

        }

        private void MatchTimerTick(object sender, EventArgs e)
        {
            if (!_memoryGameProxy.GetMatch())
            {
                var name = _memoryGameProxy.GetSecondCardName();
                if (name != "")
                {
                    var image = (Image)FindName(name);
                    image.Source = new BitmapImage(new Uri(uri));
                }
            }
        }

        private void GameTimerTick(object sender, EventArgs e)
        {
            if (_memoryGameProxy.GetRestart())
            {
                FillCoverPictures();
            }
    
            if (_memoryGameProxy.GetFirstCardTimer()) 
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

            if (_memoryGameProxy.GetSecondCardTimer()) 
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
        }

        private void FirstCardTimerTick(object sender, EventArgs e)
        {
           if (_memoryGameProxy.GetSecondCardName() != "")
           {
               if (GetImageSourceHashCode(_memoryGameProxy.GetFirstCardName()) == GetImageSourceHashCode(_memoryGameProxy.GetSecondCardName()))
               {
                   _memoryGameProxy.SetFirstCardTimer(false);
                   _memoryGameProxy.SetFirstCardName("");
                   _memoryGameProxy.SetMatch(true);
               }
               else
               {
                   _memoryGameProxy.SetFirstCardTimer(false);
                   _memoryGameProxy.SetFirstCardName("");
                   _memoryGameProxy.SetMatch(false);
                }
            }
            
        }
        private void SecondCardTimerTick(object sender, EventArgs e)
        {
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
            }
            else 
            {
                var name = _memoryGameProxy.GetSecondCardName();
                if (name != "")
                {
                    var image = (Image)FindName(name);
                    image.Source = new BitmapImage(new Uri(uri));
                }
                _memoryGameProxy.SetSecondCardTimer(false);
                _memoryGameProxy.SetSecondCardName("");
                
                if (_memoryGameProxy.GetFirstPlayerTurn())
                {
                    _memoryGameProxy.SetFirstPlayerTurn(false);
                    WhoseTurnLabel.Content = _memoryGameProxy.GetPlayer2().FirstName + "'s turn";
                }
                else
                {
                    _memoryGameProxy.SetFirstPlayerTurn(true);
                    WhoseTurnLabel.Content = _memoryGameProxy.GetPlayer2().FirstName + "'s turn";
                }
            }
        }

        private void StartGameButtonWasClicked(object sender, RoutedEventArgs e)
        {
            if (_wasStartClicked) return;
            _wasStartClicked = true;
            var firstName = FirstNameTextBox.Text;
            
            if (_memoryGameProxy.GetPlayer1().FirstName == "Player1")
            {
                _memoryGameProxy.SetPlayer1FirstName(firstName);
            }
            else if (_memoryGameProxy.GetPlayer2().FirstName == "Player2")
            {
                _memoryGameProxy.SetPlayer2FirstName(firstName);
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
                _memoryGameProxy.SetFirstCardName(image.Name);
                _memoryGameProxy.SetFirstCardClicked(true);
                _memoryGameProxy.SetFirstCardTimer(true);
            }
            else if (_memoryGameProxy.GetSecondCardName() == "")
            {
                _memoryGameProxy.SetSecondCardName(image.Name);
                _memoryGameProxy.SetSecondCardClicked(true);
                _memoryGameProxy.SetSecondCardTimer(true);
            }
           
               
        }

        private void ResetGame()
        {
            _firstPlayerTurn = true;
            _memoryGameProxy.SetFirstCardClicked(false);
            _memoryGameProxy.SetSecondCardClicked(false);
            _wasMatch = false;
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
