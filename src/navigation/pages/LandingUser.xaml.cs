using mynance.src.localization;
using System.Windows.Controls;

namespace mynance.src.navigation.pages
{
	/// <summary>
	/// Interaction logic for LandingUser.xaml
	/// </summary>
	public partial class LandingUser : Page
	{
		public LandingUser()
		{
			DataContext = new LandingUserViewModel();
			InitializeComponent();
		}

		private void ComboBox_DropDownClosed(object sender, EventArgs e)
		{
			return;
		}

		private void btnEditBudget_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			try
			{
				if (((LandingUserViewModel)this.DataContext).TryEditBudget(tbEditBudget.Text, CategoryComboBox.SelectedIndex) == false)
				{
					tbEditBudget.Text = LocalizationProvider.GetLocalizedValue<string>("Error");
				}
			}
			catch (Exception)
			{
				tbEditBudget.Text = LocalizationProvider.GetLocalizedValue<string>("Error");
			}
		}
	}
}
