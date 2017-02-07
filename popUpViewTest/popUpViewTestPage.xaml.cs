using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace popUpViewTest
{
	public partial class popUpViewTestPage : ContentPage
	{
		PopupLayout _PopUpLayout = new PopupLayout();
			
		public popUpViewTestPage()
		{
			InitializeComponent();

			_PopUpLayout.Content = GanzeSeite; // GanzeSeite is my object with the whole page-definition / before using the PopUP, I had: content = GanzeSeite
			Content = _PopUpLayout; // Make _PopUpLayot to top-layout


			var btPopUpPLZOrtZeigen = new Button { Text = "POP UP", TextColor = Color.White, BackgroundColor = Color.Blue };


			if (_PopUpLayout.IsPopupActive)
			{
				_PopUpLayout.DismissPopup();// Close the PopUp
			}
			GanzeSeite.IsVisible = true; // Don’t forget, to make the page visible after close the PopUp!! 


			btPopUpPLZOrtZeigen.Clicked += async (sender, e) => // event-handler to Button btPopUpZeigen
			{
				int iBreite = 400;
				if (iBreite > GV.iPageBreite)
				{ iBreite = GV.iPageBreite; }
				int iHoeheKeyboard = HoeheKeyBoard();
				#if WINDOWS_PHONE // Special case, as PopUp in Frame crashes on WP, if called more than one time -> directly StackLayou
				// => See comments to the next #
				 iHoeheKeyboard = HoeheKeyBoard();
				 var PopUpPLZOrt = new StackLayout // Define directly the StackLayout as PopUp
				 {
				 WidthRequest = iBreite,
				 HeightRequest = GV.iPageHoehe - iHoeheKeyboard - 20, //
				 Orientation = StackOrientation.Vertical,
				 BackgroundColor = Color.FromHex("#28283E"), // Link to Color-Picker: http://www.w3schools.com/tags/ref_colorpicker.asp
				 Children =
				 {
								 PLZOrt_LabelPopUp,
				 SearchBarPLZOrt,
				 LVPLZOrt,
				 }
				};
				#else
				iBreite = 300; // needed width
				if (iBreite > GV.iPageBreite) // for sercurity only
				{ iBreite = GV.iPageBreite;
				}
				var PopUpInhaltPLZOrt = new StackLayout
				{
					WidthRequest = iBreite - 40,
					HeightRequest = GV.iPageHoehe - iHoeheKeyboard - 20, // calculate the Height
					Orientation = StackOrientation.Vertical,
					Children =
  				{
 					PLZOrt_LabelPopUp,
 					SearchBarPLZOrt,
 					LVPLZOrt,
 				}
				};
				if (Device.OS == TargetPlatform.iOS) // Set platform-specific color
				{ PopUpInhaltPLZOrt.BackgroundColor = Color.White; }
				//Note: Frame have a default Layout.Padding of 20
				var PopUpPLZOrt = new Frame // set a Frame as PopUp and add the the StackLayout as content
				{
					Content = PopUpInhaltPLZOrt,
					HasShadow = true,
					OutlineColor = Color.White,
				};
				if (Device.OS == TargetPlatform.Android) // Set platform-specific color
				{ PopUpPLZOrt.BackgroundColor = Color.FromHex("#28283E"); }
				if (Device.OS == TargetPlatform.iOS) // Set p
					#endif
				if (_PopUpLayout.IsPopupActive) // should never be the case
					{
						_PopUpLayout.DismissPopup();
					}
				double Position = (GV.iPageBreite - iBreite) / 2; // Calculate the position (middle of the page)
																  // show the PopUp in the middle of the screen and “padding” of 20
				_PopUpLayout.ShowPopup(PopUpPLZOrt, Constraint.Constant(Position), Constraint.Constant(20));
				GanzeSeite.IsVisible = false; // Hide the whole page
				SearchBarPLZOrt.Focus();// Set Focus to entry
			};



		}
	}
}
