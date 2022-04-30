namespace IT_4_term_Lab_6;

public class Location
{
    private readonly double _longitude;
    private readonly double _latitude;

    public Location(double longitude, double latitude)
    {
        _longitude = longitude;
        _latitude = latitude;
    }
    public void Print()
    {
        Console.Write(_longitude); Console.Write(" "); Console.WriteLine(_latitude);
    }

    public double GetDistanceTo(double longitude, double latitude)
    {
        var deltaLat = (latitude - _latitude) * Math.PI / 180;
        var deltaLong = (longitude - _longitude) * Math.PI / 180;
        var a = Math.Pow(Math.Sin(deltaLat / 2), 2) + Math.Cos(latitude * Math.PI / 180) *
            Math.Cos(_latitude * Math.PI / 180) * Math.Pow(Math.Sin(deltaLong / 2), 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return 6371000 * c ;
    }
}

