using System;
using System.Reflection;
using System.Xml.Linq;

class PERSON
{
    public string gender { get; set; }
    public int age { get; set; }
    public string name { get; set; }
    public PERSON()
    {
        gender = "";
        age = 0;
        name = "";
    }
    public PERSON(string gender, int age, string name)
    {
        this.name = name;
        this.gender = gender;
        this.age = age;
    }
    public virtual void nhap()
    {
        Console.Write("Nhap ten: ");
        name = Console.ReadLine();
        Console.Write("Nhap gioi tinh: ");
        gender = Console.ReadLine();
        Console.Write("Nhap tuoi: ");
        age = int.Parse(Console.ReadLine());
    }
    public virtual void xuat()
    {
        Console.Write("{0,-20}", name);
        Console.Write("{0,-10}", gender);
        Console.Write("{0,-10}", age);
    }
}
class CONGNHAN : PERSON
{
    public string cty { get; set; }
    public double hsl { get; set; }
    public double luong;
    public CONGNHAN() : base()
    {
        cty = "";
        hsl = 1;
    }
    public CONGNHAN(string name, int age, string gender, string cty, double hsl) : base(name, age, gender)
    {
        cty = cty;
        hsl = hsl;
    }
    public override void nhap()
    {
        base.nhap();
        Console.Write("Nhap ten cty: "); cty = Console.ReadLine();
        Console.Write("HSL: "); hsl = int.Parse(Console.ReadLine());
        luong = hsl * 850000;
    }
    public override void xuat()
    {
        base.xuat();
        Console.Write("{0,-20}", cty);
        Console.Write("{0,-10}", hsl);
        Console.WriteLine("{0,-20}", luong);
    }
}
class Program
{
    static void Main(string[] args)
    {
        List<CONGNHAN> dscn = new List<CONGNHAN>();
        int n;
        Console.Write("Nhap so cong nhan: ");
        n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Nhap con nhan {0}", i + 1);
            CONGNHAN a = new CONGNHAN();
            a.nhap();
            dscn.Add(a);
        }
        Console.Write("{0,-20}", "name");
        Console.Write("{0,-10}", "gender");
        Console.Write("{0,-10}", "age");
        Console.Write("{0,-20}", "cty");
        Console.Write("{0,-10}", "hsl");
        Console.WriteLine("{0,-20}", "thu nhap");

        foreach (CONGNHAN a in dscn)
        {
            a.xuat();
        }
        Console.ReadLine();
        double max = dscn[0].hsl;
        string tencn = "";
        Console.WriteLine("cong nhan co he so luong cao nhat la: ", dscn[0].hsl);
        foreach (CONGNHAN a in dscn)
        {

            if (a.hsl > max)
            {
                max = a.hsl;
                tencn = a.name;
            }
        }


    }

}