namespace Schedulizer_WebApp.Services
{
    public class APIServiceResult
    {
        public bool Success { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string classType { get; set; }
        public string className { get; set; }
        public string classDays { get; set; }
        public string classInstructor { get; set; }
        public string openSeats { get; set; }
        public string classBldg { get; set; }
        public string classRoom { get; set; }

    }
}