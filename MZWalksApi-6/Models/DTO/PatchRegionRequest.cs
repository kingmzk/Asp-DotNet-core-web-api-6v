﻿namespace MZWalksApi_6.Models.DTO
{
    public class PatchRegionRequest
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public double Area { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public long Population { get; set; }
    }
}