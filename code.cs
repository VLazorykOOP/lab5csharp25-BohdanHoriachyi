using System;

class Money
{
    protected int nominal;
    protected int num;

    public Money(int nominal, int num)
    {
        this.nominal = nominal;
        this.num = num;
    }

    public int Nominal
    {
        get { return nominal; }
        set { nominal = value; }
    }

    public int Num
    {
        get { return num; }
        set { num = value; }
    }

    public int TotalAmount
    {
        get { return nominal * num; }
    }

    public void Show()
    {
        Console.WriteLine($"Nominal: {nominal}, Kilkist: {num}, Zahalna suma: {TotalAmount}");
    }

    public bool CanAfford(int price)
    {
        return TotalAmount >= price;
    }

    public int MaxQuantity(int price)
    {
        return TotalAmount / price;
    }

    public int this[int index]
    {
        get
        {
            if (index == 0) return nominal;
            else if (index == 1) return num;
            else throw new IndexOutOfRangeException("Nevirnyi indeks!");
        }
        set
        {
            if (index == 0) nominal = value;
            else if (index == 1) num = value;
            else throw new IndexOutOfRangeException("Nevirnyi indeks!");
        }
    }

    public static Money operator ++(Money m)
    {
        m.nominal++;
        m.num++;
        return m;
    }

    public static Money operator --(Money m)
    {
        m.nominal--;
        m.num--;
        return m;
    }

    public static bool operator !(Money m)
    {
        return m.num != 0;
    }

    public static Money operator +(Money m, int scalar)
    {
        return new Money(m.nominal, m.num + scalar);
    }

    public static implicit operator string(Money m)
    {
        return $"{m.nominal},{m.num}";
    }

    public static explicit operator Money(string s)
    {
        var parts = s.Split(',');
        if (parts.Length != 2) throw new FormatException("Nepravylʹnyi format riadka.");
        int nominal = int.Parse(parts[0]);
        int num = int.Parse(parts[1]);
        return new Money(nominal, num);
    }
}


public class VectorLong
{
    protected long[] IntArray;
    protected uint size;
    protected int codeError;
    private static uint num_vl;

    public VectorLong()
    {
        size = 1;
        IntArray = new long[size];
        IntArray[0] = 0;
        num_vl++;
    }

    public VectorLong(uint size)
    {
        this.size = size;
        IntArray = new long[size];
        for (int i = 0; i < size; i++)
            IntArray[i] = 0;
        num_vl++;
    }

    public VectorLong(uint size, long initialValue)
    {
        this.size = size;
        IntArray = new long[size];
        for (int i = 0; i < size; i++)
            IntArray[i] = initialValue;
        num_vl++;
    }

    ~VectorLong()
    {
        Console.WriteLine($"Destructor called for vector with size {size}");
    }

    public static uint CountVectors()
    {
        return num_vl;
    }

    public uint Size => size;

    public int CodeError
    {
        get { return codeError; }
        set { codeError = value; }
    }

    public long this[int index]
    {
        get
        {
            if (index < 0 || index >= size)
            {
                codeError = 1;
                return 0;
            }
            return IntArray[index];
        }
        set
        {
            if (index < 0 || index >= size)
            {
                codeError = 1;
            }
            else
            {
                IntArray[index] = value;
            }
        }
    }

    public void Input()
    {
        for (int i = 0; i < size; i++)
        {
            Console.Write($"Enter element {i + 1}: ");
            IntArray[i] = Convert.ToInt64(Console.ReadLine());
        }
    }

    public void Print()
    {
        for (int i = 0; i < size; i++)
        {
            Console.WriteLine($"Element {i + 1}: {IntArray[i]}");
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is VectorLong other)
        {
            if (this.size != other.size) return false;
            for (int i = 0; i < this.size; i++)
            {
                if (this.IntArray[i] != other.IntArray[i]) return false;
            }
            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var value in IntArray)
        {
            hash = hash * 31 + value.GetHashCode();
        }
        return hash;
    }

    public static VectorLong operator ++(VectorLong v)
    {
        for (int i = 0; i < v.size; i++) v.IntArray[i]++;
        return v;
    }

    public static VectorLong operator --(VectorLong v)
    {
        for (int i = 0; i < v.size; i++) v.IntArray[i]--;
        return v;
    }

    public static VectorLong operator +(VectorLong v1, VectorLong v2)
    {
        uint newSize = Math.Max(v1.size, v2.size);
        VectorLong result = new VectorLong(newSize);
        for (uint i = 0; i < newSize; i++)
        {
            long value1 = (i < v1.size) ? v1.IntArray[i] : 0;
            long value2 = (i < v2.size) ? v2.IntArray[i] : 0;
            result.IntArray[i] = value1 + value2;
        }
        return result;
    }

    public static VectorLong operator +(VectorLong v, long scalar)
    {
        VectorLong result = new VectorLong(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] + scalar;
        return result;
    }

    public static VectorLong operator -(VectorLong v1, VectorLong v2)
    {
        uint newSize = Math.Max(v1.size, v2.size);
        VectorLong result = new VectorLong(newSize);
        for (uint i = 0; i < newSize; i++)
        {
            long value1 = (i < v1.size) ? v1.IntArray[i] : 0;
            long value2 = (i < v2.size) ? v2.IntArray[i] : 0;
            result.IntArray[i] = value1 - value2;
        }
        return result;
    }

    public static VectorLong operator -(VectorLong v, long scalar)
    {
        VectorLong result = new VectorLong(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] - scalar;
        return result;
    }

    public static VectorLong operator *(VectorLong v1, VectorLong v2)
    {
        uint newSize = Math.Max(v1.size, v2.size);
        VectorLong result = new VectorLong(newSize);
        for (uint i = 0; i < newSize; i++)
        {
            long value1 = (i < v1.size) ? v1.IntArray[i] : 0;
            long value2 = (i < v2.size) ? v2.IntArray[i] : 0;
            result.IntArray[i] = value1 * value2;
        }
        return result;
    }

    public static VectorLong operator *(VectorLong v, long scalar)
    {
        VectorLong result = new VectorLong(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] * scalar;
        return result;
    }

    public static VectorLong operator /(VectorLong v1, VectorLong v2)
    {
        uint newSize = Math.Max(v1.size, v2.size);
        VectorLong result = new VectorLong(newSize);
        for (uint i = 0; i < newSize; i++)
        {
            long value1 = (i < v1.size) ? v1.IntArray[i] : 0;
            long value2 = (i < v2.size) ? v2.IntArray[i] : 0;
            result.IntArray[i] = (value2 != 0) ? value1 / value2 : 0;
        }
        return result;
    }

    public static VectorLong operator /(VectorLong v, long scalar)
    {
        VectorLong result = new VectorLong(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = (scalar != 0) ? v.IntArray[i] / scalar : 0;
        return result;
    }

    public static VectorLong operator %(VectorLong v1, VectorLong v2)
    {
        uint newSize =