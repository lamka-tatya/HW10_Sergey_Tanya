﻿using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Game
    {
        private IList<IPlayer> _players = new List<IPlayer>();
        private IList<ICard> _cards = new List<ICard>();
        private IBoard _board;
        private ICoin _coin;

        public Game(IBoard board, ICoin coin)
        {
            _coin = coin ?? throw new NullCoinException(); 
            _board = board ?? throw new NullBoardException();
        }

        public void AddPlayer(IPlayer player)
        {
            player.JoinGame(this);

            player.TakeNewCard();

            _players.Add(player); // todo проверить на null
        }

        public ICard GiveNewCard()
        {
            var card = _board.GiveNewCard();
            _cards.Add(card);
            return card;
        }

        public void PlayRound()
        {
            foreach(var player in _players)
            {
                player.Toss(_coin);
            }
        }

        public IEnumerable<ICard> CardsThat(Status inWork)
        {
            return _cards.Where(x => x.Status == inWork);
        }

        public IEnumerable<IPlayer> TakePlayers()
        {
            return _players;
        }

        public int CardsCount(Status status)
        {
            return CardsThat(status).Count();
        }
    }
}
