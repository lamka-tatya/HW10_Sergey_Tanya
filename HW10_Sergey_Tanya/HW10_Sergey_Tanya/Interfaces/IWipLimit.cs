namespace Domain
{
    internal interface IWipLimit
    {
        bool IsReached(uint count);
    }
}