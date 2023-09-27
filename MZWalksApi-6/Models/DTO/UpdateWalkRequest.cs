namespace MZWalksApi_6.Models.DTO
{
    public class UpdateWalkRequest
    {
        public string Name { get; set; }

        public double Length { get; set; }

        public Guid RegionId { get; set; }   //relationship here

        public Guid WalkDifficultyId { get; set; }  //relationship here
    }
}