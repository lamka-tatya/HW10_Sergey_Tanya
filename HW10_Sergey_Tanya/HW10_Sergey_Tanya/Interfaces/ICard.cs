﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICard
    {
        Status Status { get; }

        bool IsBlocked { get; }

        Guid PlayerId { get; }

        void MoveNextStatus();

        void AssignTo(Player player);

        void Block();
    }

}
