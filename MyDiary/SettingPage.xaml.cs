using System.Data;

namespace MyDiary;

public partial class SettingPage : ContentPage
{
	public DateTime Data { get; set; }
	public string Color { get; set; }
	private int _size;
	private string _ssize;
    public string Size
	{
		get
		{
			return _ssize;
		}
		set
        {
            _ssize = value;
            if (value == "Large")
				_size = 36;
			else if (value == "Medium")
				_size = 30;
			else
				_size = 24;
		}
	}
	public string Style { get; set; }
	public SettingPage()
	{
		InitializeComponent();
		Size = "Medium";
		Style = "Arial";
        BindingContext = this;
	}

	private void SaveStyle(object sender, EventArgs e)
	{
		Preferences.Set("BackgraundColor", Color);
		ApplySetting();
		Navigation.PopModalAsync();
    }

	private void RestorStyle(object sender, EventArgs e)
	{
		Data = DateTime.Now;
		Color = "Blue";
		Size = "Medium";
		Style = "Arial";
		ApplySetting();
        Navigation.PopModalAsync();
    }

    private void ApplySetting()
	{
		if (Color == "Black")
		{
			Application.Current.Resources["BackgraundColor"] = Colors.Black;
            Application.Current.Resources["BackgraundColorButton"] = Colors.White;
            Application.Current.Resources["TextColorLabel"] = Colors.White;
            Application.Current.Resources["TextColorButton"] = Colors.Black;
        }
		else if (Color == "Green")
		{
			Application.Current.Resources["BackgraundColor"] = Colors.ForestGreen;
			Application.Current.Resources["BackgraundColorButton"] = Colors.DarkGreen;
            Application.Current.Resources["TextColorLabel"] = Colors.Black;
            Application.Current.Resources["TextColorButton"] = Colors.White;
        }
		else if (Color == "Pink")
		{
			Application.Current.Resources["BackgraundColor"] = Colors.HotPink;
            Application.Current.Resources["BackgraundColorButton"] = Colors.MediumVioletRed;
            Application.Current.Resources["TextColorLabel"] = Colors.Black;
            Application.Current.Resources["TextColorButton"] = Colors.White;
        }
		else
		{
			Application.Current.Resources["BackgraundColor"] = Colors.LightSkyBlue;
            Application.Current.Resources["BackgraundColorButton"] = Colors.Navy;
            Application.Current.Resources["TextColorLabel"] = Colors.Black;
            Application.Current.Resources["TextColorButton"] = Colors.White;
        }

        Application.Current.Resources["FontSize1"] = _size;
		Application.Current.Resources["FontSize2"] = _size - 12;
        Application.Current.Resources["FontFamily"] = Style;

    }
}