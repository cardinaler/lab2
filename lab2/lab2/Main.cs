using System.IO.Compression;
using System.Runtime.InteropServices.Marshalling;
using System.Xml.Linq;

class Program
{
    public static void Func1_FV(double x, ref double y1, ref double y2)
    {
        y1 = 0;
        y2 = 0;
    }

    public static DataItem Func1_FDI(double x)
    {
        return new DataItem(x, x - 2, x + 2);
    }

    public static void SaveLoad_debug()
    {
        FValues F1 = Func1_FV;
        FDI F2 = Func1_FDI;
        double xL = 0.0;
        double xR = 2.0;
        int nX = 3;

        V2DataArray VA1 = new V2DataArray("Quant", new DateTime(2023, 1, 1), nX, xL, xR, F1);
        Console.WriteLine(VA1.ToLongString("f3"));
        VA1.Save("my.txt");

        V2DataArray VA2 = new V2DataArray("Atom", new DateTime());
        V2DataArray.Load("my.txt", ref VA2);
        Console.WriteLine(VA2.ToLongString("f3"));
    }

    public static void linq_debug()
    {
        V2MainCollection VM = new V2MainCollection(1, 1);
        V2DataList VL = new V2DataList("Diamond", new DateTime());
        V2DataArray VA = new V2DataArray("Carbon", new DateTime());
        VM.V.Add(VL);
        VM.V.Add(VA);
        Console.WriteLine(VM.ToLongString("f3"));
        Console.WriteLine("Вернуть максимальное число измерений с нулевым значением модуля поля");
        Console.WriteLine(VM.get_max_num_zero_elem);
        Console.WriteLine("Вернуть объект DataItem с максимальным значением модуля поля");
        Console.Write(VM.get_max_DataItem);
        Console.WriteLine("Вернуть координаты x поля, которые встречались лишь раз");
        foreach (var i in VM.get_raw_x)
        {
            Console.Write(i.ToString() + ' ');
        }
        Console.WriteLine();


    }

    static void Main()
    {
        FValues F1 = Func1_FV;
        FDI F2 = Func1_FDI;

        //SaveLoad_debug();
        linq_debug();
    }
}