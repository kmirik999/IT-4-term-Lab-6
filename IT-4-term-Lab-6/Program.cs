using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using IT_4_term_Lab_6;

var database = "";
var lat= 0.0;
var lonng = 0.0;
var size = 0.0;

var commaDecimal = new CultureInfo("en")
{
     NumberFormat = {NumberDecimalSeparator = ","}
};


foreach (var arg in args)
{
     if ("--db=" == arg.Substring(0, 5))
     {
           database = arg.Substring(5);
     }

     if ("--lat=" == arg.Substring(0, 6))
     {
            lat = double.Parse(arg.Substring(6), commaDecimal);
     }

     if ("--long=" == arg.Substring(0, 7))
     {
          lonng = double.Parse(arg.Substring(7), commaDecimal);
     }

     if ("--size=" == arg.Substring(0, 7))
     {
          size = double.Parse(arg.Substring(7), commaDecimal);
     }
    
}
Console.WriteLine(database);
Console.WriteLine(lat);
Console.WriteLine(lonng);
Console.WriteLine(size);

var location = new Location(lonng, lat);

var file =File.ReadAllLines(database);

var sw = new Stopwatch();
sw.Start();
foreach (var line in file)
{
     var spliteLine = line.Split(";");
     var distance = location.GetDistanceTo(double.Parse(spliteLine[1], commaDecimal),double.Parse(spliteLine[0], commaDecimal));
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


sw.Restart();
var Rtree = new RNode();
foreach(var line in file)
{
     var spliteLine = line.Split(";");
     Rtree.Add(new Location(double.Parse(spliteLine[1],commaDecimal),double.Parse(spliteLine[0],commaDecimal),
          spliteLine[2] +  ";" +  spliteLine[3] + ";" + spliteLine[4] + ";" + spliteLine[5]));
}
sw.Stop();
Console.WriteLine($"Elapsed time: {sw.Elapsed}");


sw.Restart(); 

foreach (var l in Rtree.GetPointsInRadius(lonng,lat, size) )
{
    Console.WriteLine(l);
}


sw.Stop();
Console.WriteLine($"Elapsed time: {sw.Elapsed}");