using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app23Receiver
{
    internal class DeserializationBox
    {
        string _wordToDeserialize;
        public DeserializationBox(string wordToDeserialize)
        {
            _wordToDeserialize = wordToDeserialize;
        }

        public User DeserializeJson()
        {
            var User = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(_wordToDeserialize);
            return User;
        }
    } 
}
