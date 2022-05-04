namespace IT_4_term_Lab_6;


public class RNode
{
    public bool IsParent;

    public RNode FirstChild;
    public RNode SecondChild;

    public int Size;
    private int Capacity = 5;

    public double LatitudeMax;
    public double LatitudeMin;
    public double LongitudeMax;
    public double LongitudeMin;

    public List<Location> list = new();

    public void Add(Location e)
    {
        Size++;

        if (Size == 1)
        {
            LatitudeMax = LatitudeMin = e.Latitude;
            LongitudeMax = LongitudeMin = e.Longitude;

            list.Add(e);
        }
        else
        {
            LongitudeMin = Math.Min(e.Longitude, LongitudeMin);
            LongitudeMax = Math.Max(e.Longitude, LongitudeMax);
            LatitudeMin = Math.Min(e.Latitude, LatitudeMin);
            LatitudeMax = Math.Max(e.Latitude, LatitudeMax);
        }
        if (IsParent)
        {
            if (OptimalInclude(FirstChild, SecondChild, e))
                FirstChild.Add(e);
            else SecondChild.Add(e);
        }
        else
        {
            list.Add(e);
            if (Size > Capacity)
                Divide();
        }
    }

   

    private static int CompareByCoordinateX(Location firstplace, Location secondplace)
    {
        if (firstplace.Latitude > secondplace.Latitude)
            return 1;
        if (firstplace.Latitude < secondplace.Latitude)
            return -1;
        if (firstplace.Longitude > secondplace.Longitude)
            return 1;
        if (firstplace.Longitude < secondplace.Longitude)
            return -1;
        return String.Compare(firstplace.Title, secondplace.Title, StringComparison.Ordinal);
    }

    private static int CompareByCoordinateY(Location firstplace, Location secondplace)
    {
        if (firstplace.Longitude > secondplace.Longitude)
            return 1;
        if (firstplace.Longitude < secondplace.Longitude)
            return -1;
        if (firstplace.Latitude > secondplace.Latitude)
            return 1;
        if (firstplace.Latitude < secondplace.Latitude)
            return -1;
        return String.Compare(firstplace.Title, secondplace.Title, StringComparison.Ordinal);

    }

    private void Divide()
    {
        IsParent = true;

        FirstChild = new RNode();
        SecondChild = new RNode();

        if (LatitudeMax - LatitudeMin > LongitudeMax - LongitudeMin)
            list.Sort(CompareByCoordinateX);
        else list.Sort(CompareByCoordinateY);

        FirstChild.Add(list[0]);
        SecondChild.Add(list[Size - 1]);

        list.RemoveAt(Size - 1);
        list.RemoveAt(0);
    }
    public static bool OptimalInclude(RNode first, RNode second, Location place)
    {
        double latitudeMax1 = Math.Max(first.LatitudeMax, place.Latitude);
        double latitudeMin1 = Math.Min(first.LatitudeMin, place.Latitude);

        double longitudeMax1 = Math.Max(first.LongitudeMax, place.Longitude);
        double longitudeMin1 = Math.Min(first.LongitudeMin, place.Longitude);

        double area1 = ((latitudeMax1 - latitudeMin1) *
                        (longitudeMax1 - longitudeMin1) -
                        (first.LatitudeMax - first.LatitudeMin) *
                        (first.LongitudeMax - first.LongitudeMin));

        double latitudeMax2 = Math.Max(second.LatitudeMax, place.Latitude);
        double latitudeMin2 = Math.Min(second.LatitudeMin, place.Latitude);

        double longitudeMax2 = Math.Max(second.LongitudeMax, place.Longitude);
        double longitudeMin2 = Math.Min(second.LongitudeMin, place.Longitude);

        double area2 = ((latitudeMax2 - latitudeMin2) *
                        (longitudeMax2 - longitudeMin2) -
                        (second.LatitudeMax - second.LatitudeMin) *
                        (second.LongitudeMax - second.LongitudeMin));

        if(area1 != area2)
            return area1 < area2;
        return first.Size < second.Size;

    }

    public void PrintPointsInRadius(double lonng, double lat, double distance)
    {
        var radius = 6371000;
        var delta = distance / radius;
        var lonngRad =  lonng * Math.PI / 180;
        var latRad = lat * Math.PI / 180;
        var longitudeMax = GetBearingLonng(lonngRad, latRad, delta, 0.5 * Math.PI)* 180/Math.PI;
        var longitudeMin = GetBearingLonng(lonngRad, latRad, delta, 1.5 * Math.PI)* 180/Math.PI;
        var latitudeMax = GetBearindLat(latRad, delta, Math.PI * 0)* 180/Math.PI;
        var latitudeMin = GetBearindLat(latRad, delta, Math.PI * 1)* 180/Math.PI;
        PrintPointsInRadius( lonng,lat,distance,longitudeMax,longitudeMin,latitudeMax,latitudeMin);
        
    }

    public void PrintPointsInRadius(double lonng, double lat, double distance, double longitudeMax, double longitudeMin,
        double latitudeMax, double latitudeMin)
    {
        if (!IsOverlapping(longitudeMax, longitudeMin, latitudeMax, latitudeMin))
        {
            return;
        }

        for (int i = 0; i < list.Count; i++)
        {
            var distanceToPoint = list[i].GetDistanceTo(lonng, lat);

            if (distanceToPoint < distance)
            {
                Console.WriteLine(list[i].Title + "," + distanceToPoint + "m");
            }
            
        }

        if (IsParent)
        {
            FirstChild.PrintPointsInRadius(lonng,lat,distance,longitudeMax,longitudeMin,latitudeMax,latitudeMin);
            SecondChild.PrintPointsInRadius(lonng,lat,distance,longitudeMax,longitudeMin,latitudeMax,latitudeMin);
        }
    }

    public bool IsOverlapping(double longitudeMax,double longitudeMin,double latitudeMax,double latitudeMin)
    {
        return (LatitudeMax >= latitudeMin && latitudeMax >= LatitudeMin && LongitudeMax >= longitudeMin &&
                longitudeMax >= LongitudeMin);
    }

    public double GetBearindLat( double lat,double delta,double brng)
    {
        
        var phiLat = Math.Asin(Math.Sin(lat) * Math.Cos(delta) + Math.Cos(lat) * Math.Sin(delta) * Math.Cos(brng));
        return phiLat;
    }

    public double GetBearingLonng(double lonng, double lat,double delta, double brng)
    {
       
        var lambdaLong = lonng + Math.Atan2(Math.Sin(brng) *Math.Sin(delta) * Math.Cos(lat),Math.Cos(delta) - Math.Sin(lat)*Math.Sin(GetBearindLat(lat,delta, brng)));
        return lambdaLong;
    }
}
