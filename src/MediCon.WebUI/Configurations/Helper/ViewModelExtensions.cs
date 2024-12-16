using MediCon.WebUI.Configurations.Common;

namespace MediCon.WebUI.Configurations.Helper;

public static class ViewModelExtensions
{
    public static ViewModel WithError(this ViewModel model, string errorMessage, ShowErrorEnum showErrorEnum = ShowErrorEnum.DefaultAlert)
    {
        model.IsError = true;
        model.ErrorMessage = errorMessage;
        model.ShowError = showErrorEnum;

        return model;
    }
}
