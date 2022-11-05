namespace Sapphire.Utils;

public class MessageBox
{
    public static void Show(string message)
    {
        MessageBoxWindow messageBoxWindow = new MessageBoxWindow(message);
        messageBoxWindow.Show();
    }
}