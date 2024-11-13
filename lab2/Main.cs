using System.Runtime.InteropServices.Marshalling;

class Program
{
    public static void Func1_FV(double x, ref double y1, ref double y2)
    {
        y1 = x - 1;
        y2 = x + 1;
    }

    public static DataItem Func1_FDI(double x)
    {
        return new DataItem(x, x - 2, x + 2);
    }
    static void Main()
    {
        FValues F1 = Func1_FV;
        FDI F2 = Func1_FDI;
        //1
        int Case = 2;
        if (Case == 1)
        {
            double xL = 0.0;
            double xR = 2.0;
            int nX = 3;
            V2DataArray VA = new V2DataArray("Quant", new DateTime(), nX, xL, xR, F1);
            Console.WriteLine(VA.ToLongString("f3"));
            V2DataList VL2 = (V2DataList)VA;
            Console.WriteLine(VL2.ToLongString("f3"));
        }
        
        //2
        if (Case == 2)
        {
            /*
            const int N = 5;
            double[] d = new double[N];
            double step = 0.1;
            d[0] = 0.0;
            for (int i = 1; i < N; ++i)
            {
                d[i] = d[i - 1] + step;
            }
            V2DataList VL = new V2DataList("Foton", new DateTime(), d, F2);
            Console.WriteLine(VL.ToLongString("f3"));
            V2DataArray VA = VL.VArr;
            Console.WriteLine(VA.ToLongString("f3"));
            foreach(DataItem D in VA)
            {
                Console.WriteLine(D.ToLongString("f3"));
            }
            
            */
            int [][] a = new int [2][];
            for(int i = 0; i < 2; ++i)
            {
                a[i] = new int [2] {i + 1, i + 2};
            }
            var ans = from p in a from k in p select k;
            foreach(var i in ans)
            {
                Console.WriteLine(i);
            }
        }
        //3
        if (Case == 3)
        {
            V2MainCollection VM = new V2MainCollection(1, 1);
            Console.WriteLine(VM.ToLongString("f3"));
            int size = VM.V.Count();
            for(int i = 0; i < size; ++i)
            {
                Console.WriteLine(VM.V[i].MinField);
            }
        }

    }
}