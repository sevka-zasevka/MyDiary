namespace MyDiary;

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public partial class NotePage : ContentPage
{
    public event EventHandler<Note> NoteSaved;

	public string Title { get; set; }
	public string Text { get; set; }
    public DateTime dt { get; set; }
    public TimeSpan tod { get; set; }
    public Note Note { get; set; }

    public NotePage(Note note)
    {
        Note = note;
        Title = note.Title;
        Text = note.Text;
        dt = note.DateTime;
        tod = note.DateTime.TimeOfDay;
        InitializeComponent();
        BindingContext = this;
    }

	private async void CancelClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

    public async void SaveClicked(object sender, EventArgs e)
    { 
        var datetime = dt.Add(tod);
        Note.Title = Title;
        Note.Text = Text;
        Note.DateTime = datetime;
        NoteSaved?.Invoke(this, Note);
		await Navigation.PopAsync();
    }
}