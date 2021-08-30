public struct Coords<T>
{
    public Coords(T x, T y)
    {
        X = x;
        Y = y;
    }

    public T X { get; set;}
    public T Y { get; set;}

    public override string ToString() => $"({X}, {Y})";
}