using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogic
{
    [DataContract]
    public class Card
    {

        [DataMember]
        public bool Guessed;
        [DataMember]
        public bool Clicked;

        [DataMember]
        public string Name;

        public Card()
        {
            Guessed = false;
            Clicked = false;
            Name = "";
        }
    }
}
