using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.DSL
{
    public static class Builder
    {
        public static BoardBuilder CreateBoard => new BoardBuilder();

        public static GameBuilder CreateGame => new GameBuilder();

        public static PlayerBuilder CreatePlayer => new PlayerBuilder();

    }
}
