namespace IT_4_term_Lab_6;

public class Location
{

    
    public double Latitude;
    public double Longitude;
    public string Title; 

    public Location(double longitude, double latitude, string title = "")
    {
        Longitude = longitude;
        Latitude = latitude;
        Title = title;
    }
    

    public double GetDistanceTo(double longitude, double latitude)
    {
        var deltaLat = (latitude - Latitude) * Math.PI / 180;
        var deltaLong = (longitude - Longitude) * Math.PI / 180;
        var a = Math.Pow(Math.Sin(deltaLat / 2), 2) + Math.Cos(latitude * Math.PI / 180) *
            Math.Cos(Latitude * Math.PI / 180) * Math.Pow(Math.Sin(deltaLong / 2), 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return 6371000 * c;
    }

    public override string ToString()
    {
        return Title;
    }
}
    
    
