namespace Domain.Tests.DSL
{
    internal static class Builder
    {
        public static GameBuilder CreateGame => new GameBuilder();

        public static PlayerBuilder CreatePlayer => new PlayerBuilder();
    }
}
