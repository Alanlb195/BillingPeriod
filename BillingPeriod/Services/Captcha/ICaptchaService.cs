namespace BillingPeriod.Services.Captcha
{
    public interface ICaptchaService
    {
        string GenerateCaptchaText(int length, bool useLetters);
        byte[] GenerateCaptchaImage(string captchaText);
    }
}
