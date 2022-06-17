namespace Test.Winforms
{
    public interface IHasOut
    {
        bool TryGetValue(int key, out string value);
    }
}