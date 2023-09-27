using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Models.DTO
{
    public class Walk
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        public Guid RegionId { get; set; }   //relationship here

        public Guid WalkDifficultyId { get; set; }  //relationship here

        //Navigation property

        public Region Region { get; set; }

        public WalkDifficulty WalkDifficulty { get; set; }
    }
}