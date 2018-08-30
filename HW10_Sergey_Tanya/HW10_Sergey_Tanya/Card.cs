namespace HW10_Sergey_Tanya
{
    public class Card
    {
        public Status Status { get; private set; }

        public Card()
        {
            Status = Status.New;
        }

        public void MoveNextStatus()
        {
            Status++;
        }
    }
}