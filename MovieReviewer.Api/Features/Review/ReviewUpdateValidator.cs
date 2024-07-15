using FluentValidation;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Features.Review
{
    public class ReviewUpdateValidator : AbstractValidator<ReviewUpdateModel>
    {
        public ReviewUpdateValidator()
        {
            Include(new ReviewCreateValidator());
            RuleFor(x => x.IsDisabled).NotNull();
        }
    }
}
