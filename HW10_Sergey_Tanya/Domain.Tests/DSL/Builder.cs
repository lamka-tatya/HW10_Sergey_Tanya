namespace Domain.Tests.DSL
{
    internal static class Builder
    {
        public static BoardBuilder CreateBoard => new BoardBuilder();

        public static GameBuilder CreateGame => new GameBuilder();

        public static PlayerBuilder CreatePlayer => new PlayerBuilder();
    }
}
