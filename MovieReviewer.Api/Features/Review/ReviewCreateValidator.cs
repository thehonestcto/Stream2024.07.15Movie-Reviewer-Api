using FluentValidation;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Features.Review
{
    public class ReviewCreateValidator: AbstractValidator<ReviewCreateModel>
    {
        public ReviewCreateValidator()
        {
            RuleFor(x => x.ReviewContent).NotEmpty();
            RuleFor(x => x.ReviewScore).InclusiveBetween(1, 5);
        }
    }
}
