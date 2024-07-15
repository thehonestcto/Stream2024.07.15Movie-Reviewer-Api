using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.control.services.imdb {
    public class BaseEntity {
        [Key] public int Id { get; set; }
    }
}
