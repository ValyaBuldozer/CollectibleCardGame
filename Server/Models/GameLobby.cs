using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData;
using GameData.Enums;
using GameData.Models;
using Server.Network.Models;

namespace Server.Models
{
    public class GameLobby
    {
        public Client FirstClient { set; get; }

        public Client SecondClient { set; get; }

        public PlayerSequencing PlayerSequencing { set; get; }

        public TableCondition TableCondition { set; get; }

        public GameLobby(Client firstClient,Client secondClient)
        {
            TableCondition = new TableCondition();
            FirstClient = firstClient;
            SecondClient = secondClient;
        }

        public PlayerTurn HandlePlayerTurn(PlayerTurn playerTurn)
        {
            throw new NotImplementedException();
        }

        public void InitializeGame()
        {
            if(FirstClient == null && SecondClient == null)
                throw new NullReferenceException();

            //todo: запилить инициализацию
        }
    }
}
