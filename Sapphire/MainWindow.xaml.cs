using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Sapphire.MVVM.Model;
using Sapphire.MVVM.ViewModel;
using Sapphire.Utils;
using System.Data.Entity.Migrations;

using MessageBox = Sapphire.Utils.MessageBox;

namespace Sapphire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Drag_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed) DragMove();
        }

        private void Minimize_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ChangeState_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState !=
                                                         WindowState.Maximized
                ? WindowState.Maximized
                : WindowState.Normal;
        }

        private void CloseApplication_OnClick(object sender, RoutedEventArgs e)
        {
            WindowCollection windowCollection = Application.Current.Windows;

            for (int i = 0; i < windowCollection.Count; i++)
            {
                windowCollection[i]?.Close();
            }
        }

        //On Click
        private void AddNewNote_OnClick(object sender, RoutedEventArgs e)
        {
            TitleLabel.Visibility = Visibility.Hidden;
            TextBox.Visibility = Visibility.Hidden;

            ListView.SelectedItem = null;
            
            TitleBox.Visibility = Visibility.Visible;
            TitleBox.Text = "";
            TitleBox.Focus();
        }

        //On Key Down
        private void TitleBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string title = TitleBox.Text;

                if (title.Length == 0)
                {
                    MessageBox.Show("Please enter the name of note");
                    return;
                }
                
                NoteModel noteModel = new NoteModel()
                {
                    Title = title,
                    Text = ""
                };

                ((MainViewModel)Application.Current.MainWindow.DataContext).db.NoteModels.Add(noteModel);
                ((MainViewModel)Application.Current.MainWindow.DataContext).db.SaveChanges();

                TitleBox.Visibility = Visibility.Hidden;
                
                TitleLabel.Visibility = Visibility.Visible;
                TitleLabel.Content = noteModel.Title;

                TextBox.Text = noteModel.Text;
                ListView.SelectedItem = noteModel;
            }
        }

        private void ListView_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (ListView.SelectedItem == null) return;

            if (e.Key == Key.Delete)
            {
                NoteModel noteModel = (NoteModel)ListView.SelectedItem;
                ((MainViewModel)Application.Current.MainWindow.DataContext).db.NoteModels.Remove(noteModel);
                ((MainViewModel)Application.Current.MainWindow.DataContext).db.SaveChanges();

                TitleLabel.Content = "";
                TextBox.Text = "";
            }
        }

        //On Change
        private void ListView_OnSelected(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItem == null) return;

            TitleBox.Visibility = Visibility.Hidden;
            
            TitleLabel.Visibility = Visibility.Visible;
            TitleLabel.Content = ((NoteModel)ListView.SelectedItem).Title;
            
            TextBox.Text = ((NoteModel)ListView.SelectedItem).Text;
        }

        private void TitleBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (TitleBox.Text.Length == 0)
            {
                TitleBox.Foreground = Brushes.Gray;
                TitleBox.Text = "Enter title name";
            }
        }

        private void TitleBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (TitleBox.Text.Length == 0 || TitleBox.Text == "Enter title name")
            {
                TitleBox.Foreground = Brushes.White;
                TitleBox.Text = "";
            }
        }

        private void SaveNote_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ListView.SelectedItem == null) return;
            
            NoteModel noteModel = (NoteModel)ListView.SelectedItem;
            noteModel.Text = TextBox.Text;

            ((MainViewModel)Application.Current.MainWindow.DataContext).db.NoteModels.Add(noteModel);
            ((MainViewModel)Application.Current.MainWindow.DataContext).db.Entry(noteModel).State =
                EntityState.Modified;
            ((MainViewModel)Application.Current.MainWindow.DataContext).db.SaveChanges();
        }
    }
}