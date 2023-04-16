namespace ProAirApiServices.DataLayer.Models.Post
{
    public class ContactQuestionPost
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Subject { get; set; } = null!;
    }
}
