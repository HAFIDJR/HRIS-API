namespace HRIS.models
{
    public class Training
    {
        public int TrainingID { get; set; }
        public required string Type { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}