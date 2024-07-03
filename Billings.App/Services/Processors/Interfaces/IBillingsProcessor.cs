namespace Billings.App.Services.Processors.Interfaces;

internal interface IBillingsProcessor
{
    /// <summary>
    /// Przetwarza pobrane płatności.
    /// Mapuje je na obiekty domenowe i zapisuje w bazie.
    /// </summary>
    Task Process(); 
}
