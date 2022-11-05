using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Sapphire.MVVM.Model;

public class NoteModel
{
    [Key]
    public string Title { get; set; }
    public string Text { get; set; }

}