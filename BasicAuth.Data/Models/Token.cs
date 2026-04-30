namespace BasicAuth.Data.Models {
    public class Token {
        public int Id { get; set; }
        public Guid Key { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
