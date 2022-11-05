using System.Windows;
using System.Windows.Input;

namespace Sapphire;

public partial class MessageBoxWindow : Window
{
    private string Message;
    
    public MessageBoxWindow()
    {
        InitializeComponent();
    }

    public MessageBoxWindow(string message) : this()
    {
        Message = message;
    }

    private void MessageBoxWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        Label.Content = Message;
    }
    
    private void CloseApplication_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    private void Drag_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed) DragMove();
    }
}