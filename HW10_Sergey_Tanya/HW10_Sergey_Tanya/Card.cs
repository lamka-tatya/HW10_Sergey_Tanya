﻿using Domain.Interfaces;
using System;
using Domain.Extensions;

namespace Domain
{
    internal class Card : ICard
    {
        private IBoard _board;

        public Status Status { get; private set; }

        public Guid PlayerId { get; private set; }

        public bool IsBlocked { get; private set; }

        public Card(IBoard board)
        {
            _board = board ?? throw new NullBoardException();
            Status = Status.New;
            
        }

        public bool TryMoveNextStatus()
        {
            if (Status == Status.Done)
            {
                throw new CardStatusException();
            }

            if(!_board.WipLimitIsReached(Status.Next()))
            {
                Status = Status.Next();
                return true;
            }

            return false;
        }

        public void AssignTo(IPlayer player)
        {
            PlayerId = player.Id;
        }

        public void Block()
        {
            IsBlocked = true;
        }

        public void UnBlock()
        {
            IsBlocked = false;
        }
    }
}