using FluentValidation;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Features.Review
{
    public class ReviewInputValidator : AbstractValidator<ReviewInputModel>
    {
        public ReviewInputValidator()
        {
            RuleFor(x => x.ReviewContent).NotEmpty();
            RuleFor(x => x.ReviewScore).InclusiveBetween(1, 5);
        }
    }
}
