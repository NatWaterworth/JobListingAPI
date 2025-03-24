﻿namespace JobListingAPI.DTOs
{
    public class UpdateJobDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}
