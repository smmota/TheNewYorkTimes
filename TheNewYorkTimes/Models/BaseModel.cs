using System.ComponentModel.DataAnnotations;

namespace TheNewYorkTimes.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
