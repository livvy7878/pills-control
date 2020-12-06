using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PillsControl.ViewModels;
using Button = System.Windows.Controls.Button;

namespace PillsControl.Windows
{
	/// <summary>
	/// Interaction logic for MainSelectionPage.xaml
	/// </summary>
	public partial class MainSelectionPage : Page
	{
		public MainSelectionPage()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();

			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				MainWindowViewModel mainViewModel = FindResource("MainWindowViewModel") as MainWindowViewModel;
				mainViewModel.UserProfileHandler.CurrentUserCreatedImagePath = fileDialog.FileName;
				ImageBrush imgBrush = new ImageBrush();
				imgBrush.ImageSource = new BitmapImage(new Uri(fileDialog.FileName));
				DisplayedImage.Source = imgBrush.ImageSource;
			}
		}
	}
}
