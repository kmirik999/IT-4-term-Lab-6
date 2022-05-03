namespace IT_4_term_Lab_6;


public class RNode
{
    public bool IsParent;

    public RNode FirstChild;
    public RNode SecondChild;

    public int Size;
    private int capacity = 5;

    public double latitudeMax;
    public double latitudeMin;
    public double longitudeMax;
    public double longitudeMin;

    public List<Location> list = new();

    public void Add(Location e)
    {
        Size++;

        if (Size == 1)
        {
            latitudeMax = latitudeMin = e.Latitude;
            longitudeMax = longitudeMin = e.Longitude;

            list.Add(e);
        }
        else
        {
            latitudeMax = Math.Max(e.Latitude, latitudeMax);
            longitudeMax = Math.Max(e.Longitude, longitudeMax);
            latitudeMin = Math.Min(e.Latitude, latitudeMin);
            latitudeMax = Math.Min(e.Latitude, latitudeMax);
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
            if (Size > capacity)
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

        if (latitudeMax - latitudeMin > longitudeMax - longitudeMin)
            list.Sort(CompareByCoordinateX);
        else list.Sort(CompareByCoordinateY);

        FirstChild.Add(list[0]);
        SecondChild.Add(list[Size - 1]);

        list.RemoveAt(Size - 1);
        list.RemoveAt(0);
    }
    public static bool OptimalInclude(RNode first, RNode second, Location place)
    {
        double latitudeMax1 = Math.Max(first.latitudeMax, place.Latitude);
        double latitudeMin1 = Math.Min(first.latitudeMax, place.Latitude);

        double longitudeMax1 = Math.Max(first.longitudeMax, place.Longitude);
        double longitudeMin1 = Math.Min(first.longitudeMax, place.Longitude);

        double area1 = (latitudeMax1 - latitudeMin1) *
                      (longitudeMax1 - longitudeMin1) +
                      (second.latitudeMax - second.latitudeMin) *
                      (second.longitudeMax - second.longitudeMin);

        double latitudeMax2 = Math.Max(second.latitudeMax, place.Latitude);
        double latitudeMin2 = Math.Min(second.latitudeMax, place.Latitude);

        double longitudeMax2 = Math.Max(second.longitudeMax, place.Longitude);
        double longitudeMin2 = Math.Min(second.longitudeMax, place.Longitude);

        double area2 = (first.latitudeMax - first.latitudeMin) *
                      (first.longitudeMax - first.longitudeMin) +
                      (latitudeMax2 - latitudeMin2) *
                      (longitudeMax2 - longitudeMin2);

        if(area1 != area2)
            return area1 < area2;
        return first.Size < second.Size;
    }
}
