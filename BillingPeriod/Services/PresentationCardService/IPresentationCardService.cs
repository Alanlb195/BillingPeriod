using BillingPeriod.Models;

namespace BillingPeriod.Services.PresentationCardService
{
    public interface IPresentationCardService
    {
        string ImagePath { get; }
        string SavePresentationCardAsImage(PresentationCard presentationCard);
    }
}
