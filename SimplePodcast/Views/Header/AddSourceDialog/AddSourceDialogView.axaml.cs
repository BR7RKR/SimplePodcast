using Avalonia.Controls;
using SimplePodcast.ViewModels;

namespace SimplePodcast.Views;

public partial class AddSourceDialogView : UserControl
{
    public AddSourceDialogView()
    {
        if (Design.IsDesignMode)
        {
            DataContext = new AddSourceDialogViewModel();
        }
        
        InitializeComponent();
    }
}