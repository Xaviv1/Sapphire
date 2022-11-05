using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Sapphire.MVVM.Model;
using Sapphire.Utils;

namespace Sapphire.MVVM.ViewModel;

public class MainViewModel
{
    public ApplicationContext db = new();
    
    public ObservableCollection<NoteModel> NoteModels { get; set; }

    public MainViewModel()
    {
        db.Database.EnsureCreated();
        db.NoteModels.Load();

        NoteModels = db.NoteModels.Local.ToObservableCollection();
    }
}