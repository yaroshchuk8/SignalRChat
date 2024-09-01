using System.ComponentModel.DataAnnotations;

namespace SignalRChat.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        //[Required]
        public string Sentiment { get; set; }
    }
}
