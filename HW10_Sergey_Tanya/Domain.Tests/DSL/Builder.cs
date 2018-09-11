namespace Domain.Tests.DSL
{
    internal static class Builder
    {
        public static GameBuilder CreateGame => new GameBuilder();

        public static PlayerBuilder CreatePlayer => new PlayerBuilder();

        public static CardBuilder CreateCard => new CardBuilder();

        public static WipLimitBuilder CreateWipLimit => new WipLimitBuilder();
    }
}
