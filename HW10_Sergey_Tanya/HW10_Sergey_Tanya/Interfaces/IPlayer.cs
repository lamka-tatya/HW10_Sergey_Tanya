﻿using System;
using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.Interfaces
{
    public interface IPlayer
    {
        IEnumerable<ICard> AllCards { get; }
        Guid Id { get; }

        bool TakeNewCard();
        void Toss(ICoin coin);
        void JoinGame(Game game);
    }
}