using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
