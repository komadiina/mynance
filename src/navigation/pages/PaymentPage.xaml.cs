using mynance.src.navigation.pages.controllers;
using System.Diagnostics;
using System.Windows.Controls;

namespace mynance.src.navigation.pages
{
	/// <summary>
	/// Interaction logic for CalendarPage.xaml
	/// </summary>
	public partial class PaymentPage : Page
	{
		private UserPaymentController Controller;
		public PaymentPage()
		{
			Controller = new();
			InitializeComponent();
		}

		public void btnPaymentIncoming_Click(object sender, EventArgs e)
		{
			Controller.CreatePaymentIncoming(tbPaymentIncomingInput.Text, tbNoteIncoming.Text);
		}

		public void btnPaymentOutgoing_Click(object sender, EventArgs e)
		{
			// send input from tbPaymentOutgoingInput to controller
			Controller.CreatePaymentOutgoing(tbPaymentOutgoingInput.Text, CategoryComboBox.SelectedIndex, tbNoteOutgoing.Text);
		}

		private void ComboBox_DropDownClosed(object sender, EventArgs e)
		{
			if (CategoryComboBox.SelectedItem != null)
			{
				Trace.WriteLine(CategoryComboBox.SelectedIndex);
			}
		}
	}
}
