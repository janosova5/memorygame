using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;

namespace MemoryGameLogic
{
    //NA SERVER POSIELAT MAX NEJAKY LIST, V KTOROM BUDU CISLA, KTORE BUDU SIGNALIZOVAT ZAMIESANIE KARIET ?
    //NA SERVER NEMOZEM POSIELAT LIST OBRAZKOV, LEBO PRETECIE BUFFER !!!!!!
    [DataContract]
    public class CardImages //premenovat na CardImages
    {
        [DataMember]
        public List<BitmapImage> Images { get; set; }

        public CardImages()
        {
            Images = new List<BitmapImage>();
        }

        public void FillImages()
        {
            var address = "E:/Skola-moja/Potko/MemoryGame/MemoryGameClient/Images/";

            var eric = new BitmapImage(new Uri(address + "eric.png"));
            var tweak = new BitmapImage(new Uri(address + "tweak.png"));
            var timmy = new BitmapImage(new Uri(address + "timmy.png"));
            var girl = new BitmapImage(new Uri(address + "girl.png"));
            var kenny = new BitmapImage(new Uri(address + "kenny.png"));
            var chef = new BitmapImage(new Uri(address + "chef.png"));
            var butters = new BitmapImage(new Uri(address + "butters.png"));
            var stan = new BitmapImage(new Uri(address + "stan.png"));
            var kyle = new BitmapImage(new Uri(address + "kyle.png"));

            for (int i = 0; i < 2; i++)
            {
                Images.Add(eric);
                Images.Add(tweak);
                Images.Add(timmy);
                Images.Add(girl);
                Images.Add(kenny);
                Images.Add(chef);
                Images.Add(butters);
                Images.Add(stan);
                Images.Add(kyle);

            }

        }

        //source -  http://www.vcskicks.com/randomize_array.php
        public void ShuffleList()
        {
            List<BitmapImage> randomList = new List<BitmapImage>();

            Random r = new Random();
            while (Images.Count > 0)
            {
                var randomIndex = r.Next(0, Images.Count);
                randomList.Add(Images[randomIndex]); //add it to the new, random list
                Images.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            Images = randomList; //return the new random list
        }

        public void ClearCards()
        {
            Images.Clear();
        }


    }
}

