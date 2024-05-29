using System.Threading.Tasks;
using Blazorise;

namespace SmartSoftware.BlazoriseUI;

public static class SmartSoftwareBlazoriseUiModalExtensions
{
    public static Task CancelClosingModalWhenFocusLost(this Modal modal, ModalClosingEventArgs eventArgs)
    {
        // cancel close if clicked outside of modal area
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;

        return Task.CompletedTask;
    }
}
