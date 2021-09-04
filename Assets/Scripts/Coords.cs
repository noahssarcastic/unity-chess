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

    // public static Coords<T> operator-(Coords<T> lhs, Coords<T> rhs) => new Coords<T>(lhs.X-rhs.X, lhs.Y-rhs.Y);
}