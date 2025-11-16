using Umbraco.Cms.Core.Services;
using UmbracoCMS.ViewModels;

namespace UmbracoCMS.Services;

public class FormSubmissionsService(IContentService contentService, EmailService emailService)
{
    private readonly IContentService _contentService = contentService;
    private readonly EmailService _emailService = emailService;

    public async Task<bool> SaveCallbackRequestAsync(CallbackFormViewModel model)
    {
        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(c => c.ContentType.Alias == "formSubmissions");
            if (container == null)
            {
                return false;
            }

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {model.Name}";
            var request = _contentService.Create(requestName, container, "callbackRequest");

            request.SetValue("callbackRequestName", model.Name);
            request.SetValue("callbackRequestEmail", model.Email);
            request.SetValue("callbackRequestPhone", model.Phone);
            request.SetValue("callbackRequestOption", model.SelectedOption);

            var saveResult = _contentService.Save(request);

            // Send confirmation email
            await _emailService.SendConfirmationEmailAsync(model.Email, model.Name);

            return saveResult.Success;
        }
        catch (Exception ex )
        {
            return false;
        }
    }

}
