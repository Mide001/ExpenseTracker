using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; 

namespace ExpenseTracker.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public required string Category { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Username { get; set; } = null!;
    }
}