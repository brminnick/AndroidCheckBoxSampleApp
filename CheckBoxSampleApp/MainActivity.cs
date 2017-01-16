using System;

using Java.Interop;

using Android.OS;
using Android.App;
using Android.Widget;
using Android.Graphics;

using CheckBoxSampleApp.Shared;

namespace CheckBoxSampleApp
{
	[Activity(Label = "CheckBoxSampleApp", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Material.Light.DarkActionBar")]
	public class MainActivity : Activity
	{
		Button _button1;
		CheckBox _checkBox1, _checkBox2, _checkBox3;
		TextView _textView1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			_button1 = FindViewById<Button>(Resource.Id.Button1);
			_checkBox1 = FindViewById<CheckBox>(Resource.Id.CheckBox1);
			_checkBox2 = FindViewById<CheckBox>(Resource.Id.CheckBox2);
			_checkBox3 = FindViewById<CheckBox>(Resource.Id.CheckBox3);
			_textView1 = FindViewById<TextView>(Resource.Id.TextView1);

			_button1.Click += HandleButtonClick;
			_checkBox1.Click += HandleCheckBoxClick;
			_checkBox2.Click += HandleCheckBoxClick;
			_checkBox3.Click += HandleCheckBoxClick;

			AssignContentDescriptions();
		}

		void AssignContentDescriptions()
		{
			_button1.ContentDescription = AutomationConstants.Button1;
			_checkBox1.ContentDescription = AutomationConstants.CheckBox1;
			_checkBox2.ContentDescription = AutomationConstants.CheckBox2;
			_checkBox3.ContentDescription = AutomationConstants.CheckBox3;
			_textView1.ContentDescription = AutomationConstants.TextView1;
		}

		void HandleCheckBoxClick(object sender, EventArgs e)
		{
			var checkBox = sender as CheckBox;

			if (checkBox?.ContentDescription == null)
				return;

			var checkBoxNumber = int.Parse(checkBox?.ContentDescription.Substring(8));
			checkBox.Text = checkBox.Checked ? $"Check Box {checkBoxNumber} is Checked" : $"Check Box {checkBoxNumber} is Unchecked";
		}

		void HandleButtonClick(object sender, EventArgs e)
		{
			int numberOfCheckBoxesChecked = 0;

			if (_checkBox1.Checked)
				numberOfCheckBoxesChecked++;
			if (_checkBox2.Checked)
				numberOfCheckBoxesChecked++;
			if (_checkBox3.Checked)
				numberOfCheckBoxesChecked++;

			if (numberOfCheckBoxesChecked == 1)
				_textView1.Text = $"{numberOfCheckBoxesChecked} Check Box Is Checked";
			else
				_textView1.Text = $"{numberOfCheckBoxesChecked} Check Boxes Are Checked";
		}

		#region Xamarin UITest Backdoor Methods
#if DEBUG
		[Export("GetColorAsInt")]
		public string GetColorAsInt(string colorName)
		{
			int colorAsInt = Color.ParseColor(colorName.ToString());

			return colorAsInt.ToString();
		}
#endif
		#endregion
	}
}

