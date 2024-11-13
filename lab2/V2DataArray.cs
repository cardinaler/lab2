class V2DataArray : V2Data
{
    public double[] Net
    {
        get;
        set;
    }
    public double[,] Field_values
    {
        get;
        set;
    }

    public double[] this[int index]
    {

        get
        {
            int size = Field_values.Length / 2;
            double[] tmp = new double[size];
            for (int i = 0; i < size; ++i)
            {
                tmp[i] = Field_values[index, i];
            }
            return tmp;
        }
    }

    public V2DataArray(string key, DateTime date) : base(key, date)
    {
        Net = new double[0];
        Field_values = new double[0, 0];
    }

    public V2DataArray(string key, DateTime date, double[] x, FValues F) : base(key, date)
    {
        Net = new double[0];
        Field_values = new double[2, x.Length];
        for (int i = 0; i < x.Length; ++i)
        {
            F(x[i], ref Field_values[0, i], ref Field_values[1, i]);
        }
    }
    public V2DataArray(string key, DateTime date, int nX, double xL, double xR, FValues F) : base(key, date)
    {
        Net = new double[nX];
        Field_values = new double[2, nX];
        double step = (xR - xL) / (nX - 1); //nX != 1 
        Net[0] = xL;
        F(Net[0], ref Field_values[0, 0], ref Field_values[1, 0]);
        for (int i = 1; i < nX; ++i)
        {
            Net[i] = Net[i - 1] + step;
            F(Net[i], ref Field_values[0, i], ref Field_values[1, i]);
        }
    }

    public static explicit operator V2DataList(V2DataArray source)
    {
        V2DataList V2 = new V2DataList(source.key, source.date);
        int size = source.Field_values.Length / 2;
        for (int i = 0; i < size; ++i)
        {
            V2.L.Add(new DataItem(source.Net[i], source.Field_values[0, i], source.Field_values[1, i]));
        }
        return V2;
    }

    public override double MinField
    {
        get
        {
            double min = Math.Abs(Field_values[0, 0]);
            int size = Field_values.Length / 2;
            for (int i = 0; i < size; ++i)
            {
                if (Math.Abs(Field_values[0, i]) < min)
                {
                    min = Math.Abs(Field_values[0, i]);
                }
                if (Math.Abs(Field_values[1, i]) < min)
                {
                    min = Math.Abs(Field_values[1, i]);
                }
            }
            return min;
        }
    }

    public override string ToString()
    {
        string s = $"Type name: {this.GetType().ToString()}\n";
        s += $"key = {base.key}, date = {base.date}\n";
        return s;
    }

    public override string ToLongString(string format)
    {
        string s = ToString();
        int size = Field_values.Length / 2;
        for (int i = 0; i < size; ++i)
        {
            s += $"{i}: x = {Net[i].ToString(format)}, y = ({Field_values[0, i].ToString(format)}, {Field_values[1, i].ToString(format)})\n";
        }
        return s;
    }

    public override IEnumerator<DataItem> GetEnumerator()
    {
        V2DataList V2L = (V2DataList)this;
        return V2L.GetEnumerator();
    }

};

