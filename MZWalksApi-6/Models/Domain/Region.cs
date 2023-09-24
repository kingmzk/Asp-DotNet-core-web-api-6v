namespace MZWalksApi_6.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Area { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public long Population { get; set; }

        //Navigation Properties
        //one region can have many walks

        public IEnumerable<Walk> Walks { get; set; }
    }
}