class V2MainCollection : System.Collections.ObjectModel.ObservableCollection<V2Data>
{
    public List<V2Data> V;
    public bool Contains(string key)
    {
        for (int i = 0; i < V.Count(); ++i)
        {
            if (V[i].key == key)
            {
                return true;
            }
        }
        return false;
    }

    public new bool Add(V2Data v2Data)
    {
        for (int i = 0; i < V.Count(); ++i)
        {
            if (V[i].key == v2Data.key)
            {
                return false;
            }
        }
        V.Add(v2Data);
        return true;
    }

    public static void Func1_FV(double x, ref double y1, ref double y2)
    {
        y1 = x - 1;
        y2 = x + 1;
    }

    public static DataItem Func1_FDI(double x)
    {
        return new DataItem(x, x - 2, x + 2);
    }

    public V2MainCollection(int nV2DataArray, int nV2DataList)
    {
        FValues F1 = Func1_FV;
        FDI F2 = Func1_FDI;
        V = new List<V2Data>();

        const  int N = 3;
        double[] d = new double[N];
        d[0] = 0.0;
        for (int i = 1; i < N; ++i)
        {
            d[i] = d[i - 1] + 0.05;
        }

        for (int i = 0; i < nV2DataList; ++i)
        {
            V.Add(new V2DataList($"VL_elem{i}", new DateTime(), d, F2));
        }
        for (int i = 0; i < nV2DataArray; ++i)
        {
            V.Add(new V2DataArray($"VA_elem{i}", new DateTime(), 5, 0.0, 1.0, F1));
        }
    }

    public string ToLongString(string format)
    {
        string s = "";
        for (int i = 0; i < V.Count(); ++i)
        {
            s = s + V[i].ToLongString(format);
        }
        return s;
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < V.Count(); ++i)
        {
            s = s + V[i].ToString();
        }
        return s;
    }

/*
    public int num_zero_elem
    {
        get
        {
            var V2A = from p in V where p is V2DataArray select (V2DataArray)p;
            var V2L = from p in V where p is V2DataList select (V2DataList)p;
            var a1 = from p in V2A from k in p 
                        where k.y[0] == 0 && k.y[1] == 0 select k;
            return a1;
            
        }
    }
*/

}

