namespace Test.Winforms
{
    public interface IHasOut
    {
        bool TryGetValue(int key, out string value);
        bool TryGetValue(int key, out int value);
        bool DoNothing();
    }
}