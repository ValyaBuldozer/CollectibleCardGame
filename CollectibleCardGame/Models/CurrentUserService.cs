using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Controllers.Data;
using GameData.Enums;
using GameData.Models.Cards;
using GameData.Network;
using Newtonsoft.Json;
using Exception = System.Exception;

namespace CollectibleCardGame.Models
{
    public class CurrentUserService
    {
        private readonly IDataRepositoryController<Card> _cardRepositoryController;

        private DeckInfo _southDeck;
        private DeckInfo _northDeck;
        private DeckInfo _darkDeck;

        public string Username { set; get; }

        public IEnumerable<Card> SouthDeck { set; get; }

        public IEnumerable<Card> NorthDeck { set; get; }

        public IEnumerable<Card> DarkDeck { set; get; }

        public UnitCard SouthHeroCard { set; get; }

        public UnitCard NorthHeroCard { set; get; }

        public UnitCard DarkHeroCard { set; get; }

        public int GameLoseCount { set; get; }
        public int GameWinCount { set; get; }

        public CurrentUserService(IDataRepositoryController<Card> cardRepositoryController)
        {
            _cardRepositoryController = cardRepositoryController;
        }

        public void SetData(UserInfo userInfo)
        {
            if(userInfo == null) return;

            Username = userInfo.Username;

            try
            {
                if (!string.IsNullOrEmpty(userInfo.SouthDeck))
                    _southDeck = JsonConvert.DeserializeObject<DeckInfo>(userInfo.SouthDeck);

                if (!string.IsNullOrEmpty(userInfo.NorthDeck))
                    _northDeck = JsonConvert.DeserializeObject<DeckInfo>(userInfo.NorthDeck);

                if (!string.IsNullOrEmpty(userInfo.DarkDeck))
                    _darkDeck = JsonConvert.DeserializeObject<DeckInfo>(userInfo.DarkDeck);
            }
            catch (JsonException e)
            {
                return;
            }

            SouthDeck = _cardRepositoryController.GetById(_southDeck?.DeckIds);
            NorthDeck = _cardRepositoryController.GetById(_northDeck?.DeckIds);
            DarkDeck = _cardRepositoryController.GetById(_darkDeck?.DeckIds);

            SouthHeroCard = _southDeck?.HeroCard;
            NorthHeroCard = _northDeck?.HeroCard;
            DarkHeroCard = _darkDeck?.HeroCard;
        }

        public IEnumerable<Card> GetDeckByFraction(Fraction fraction)
        {
            switch (fraction)
            {
                case Fraction.Common:
                    return null;
                case Fraction.North:
                    return NorthDeck;
                case Fraction.South:
                    return SouthDeck;
                case Fraction.Dark:
                    return DarkDeck;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fraction), fraction, null);
            }
        }

        public UnitCard GetHeroByFraction(Fraction fraction)
        {
            switch (fraction)
            {
                case Fraction.Common:
                    return null;
                case Fraction.North:
                    return NorthHeroCard;
                case Fraction.South:
                    return SouthHeroCard;
                case Fraction.Dark:
                    return DarkHeroCard;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fraction), fraction, null);
            }
        }
    }
}
