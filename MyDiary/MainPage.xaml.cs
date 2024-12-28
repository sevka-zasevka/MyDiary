using System.Collections.ObjectModel;
using SQLite;


namespace MyDiary
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Note> Notes {get; set;}=new ObservableCollection<Note>();
        private readonly SQLiteAsyncConnection _connection;
        public Note SelectedNote { get; set; }
        public bool edit = false;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            ResourceDictionary resourceDictionary = new ResourceDictionary();

            // Определение стиля для кнопки
            Style buttonStyle = new Style(typeof(Button));
            // Добавление стиля в ResourceDictionary
            resourceDictionary.Add("MyButtonStyle", buttonStyle);

            // Применение ресурсов к текущему окну
            this.Resources = resourceDictionary;

            _connection = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), "Diary.db"));
            InitializeDatabase();
        }

        private async Task InitializeDatabase()
        {
            await _connection.CreateTableAsync<Note>();
            await LoadNote();
        }

        private async Task LoadNote()
        {
            var notes = await _connection.Table<Note>().ToListAsync();
            foreach (var note in notes) 
            {
                Notes.Add(note);
            }
        }

        private async void CreateNewNote(object sender, EventArgs e)
        {
            var notePage = new NotePage(new Note());
            notePage.NoteSaved += NoteSaved;
            await Navigation.PushAsync(notePage);
        }
        private async void EditNote(object sender, EventArgs e)
        {
            if (SelectedNote != null)
            {
                var editNote = new Note(SelectedNote.Title, SelectedNote.Text, SelectedNote.DateTime);
                var notePage = new NotePage(editNote);
                edit = true;
                notePage.NoteSaved += NoteSaved;
                await Navigation.PushAsync(notePage);
            }
        }
        private async void DeleteNote(object sender, EventArgs e)
        {
            if (SelectedNote != null)
            {
                await _connection.DeleteAsync(SelectedNote);
                Notes.Remove(SelectedNote);
            }
        }

        private async void NoteSaved(object sender, Note note)
        {
            if (edit)
            {
                int index = Notes.IndexOf(SelectedNote);
                Notes.Remove(SelectedNote);
                Notes.Insert(index, note);
                await _connection.UpdateAsync(note);
                edit = false;
            }
            else
            {
                await _connection.InsertAsync(note);
                Notes.Add(note);
            }
        }

        private async void ChangeSetting(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SettingPage());
        }
    }

}
