namespace Domain
{
    public interface IWipLimit
    {
        bool IsReached(uint count);
    }
}