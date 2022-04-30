using System.Diagnostics;
using System.Runtime.CompilerServices;
using IT_4_term_Lab_6;

var db = "";
var lat= 0.0;
var lonng = 0.0;
var size = 0.0;


foreach (var arg in args)
{
     if ("--db=" == arg.Substring(0, 5))
     {
           db = arg.Substring(5);
     }

     if ("--lat=" == arg.Substring(0, 6))
     {
            lat = double.Parse(arg.Substring(6));
     }

     if ("--long=" == arg.Substring(0, 7))
     {
          lonng = double.Parse(arg.Substring(7));
     }

     if ("--size=" == arg.Substring(0, 7))
     {
          size = double.Parse(arg.Substring(7));
     }
    
}
Console.WriteLine(db);
Console.WriteLine(lat);
Console.WriteLine(lonng);
Console.WriteLine(size);

var location = new Location(lonng, lat);


var file =File.ReadAllLines(db);
foreach (var line in file)
{
     var spliteLine = line.Split(";");
     var distance = location.GetDistanceTo(double.Parse(spliteLine[1]),double.Parse(spliteLine[0]));
     if (distance < size)
     {
          Console.WriteLine(spliteLine[2]);
          Console.WriteLine(spliteLine[3]);
          Console.WriteLine(spliteLine[4]);
          Console.WriteLine(spliteLine[5]);
     }
     
}
var sw = new Stopwatch();
sw.Start();
foreach (var line in file)
{
     var spliteLine = line.Split(";");
     var distance = location.GetDistanceTo(double.Parse(spliteLine[1]),double.Parse(spliteLine[0]));
     if (distance < size)
     {
          Console.WriteLine(spliteLine[2]);
          Console.WriteLine(spliteLine[3]);
          Console.WriteLine(spliteLine[4]);
          Console.WriteLine(spliteLine[5]);
     }
     
}
sw.Stop();
Console.WriteLine($"Elapsed time: {sw.Elapsed}");


     
