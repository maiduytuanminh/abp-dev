using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmartSoftware.AspNetCore.Components.Web.Theming.PageToolbars;
using SmartSoftware.BlazoriseUI;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Web.Theming.Layout;

public class PageLayout : IScopedDependency, INotifyPropertyChanged
{
    private string? title;

    // TODO: Consider using this property for setting Page Title too.
    public virtual string? Title {
        get => title;
        set {
            title = value;
            OnPropertyChanged();
        }
    }

    private string? menuItemName;

    public string? MenuItemName {
        get => menuItemName;
        set
        {
            menuItemName = value;
            OnPropertyChanged();
        }
    }

    public virtual ObservableCollection<BreadcrumbItem> BreadcrumbItems { get; } = new();

    public virtual ObservableCollection<PageToolbarItem> ToolbarItems { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}