namespace Billings.App.Services.Interfaces;

internal interface IBillingTypesService
{
    /// <summary>
    /// Zapewnia dostępność typów płatności.
    /// W przypadku pojawienia się nowego typu płatności zapisuje go w bazie.
    /// </summary>
    Task EnsureBillingTypes();
}