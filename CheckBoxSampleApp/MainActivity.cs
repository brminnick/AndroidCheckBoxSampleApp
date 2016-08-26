using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CheckBoxSampleApp
{
    [Activity(Label = "CheckBoxSampleApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var checkBox1 = FindViewById<CheckBox>(Resource.Id.CheckBox1);
            var checkBox2 = FindViewById<CheckBox>(Resource.Id.CheckBox2);
            var checkBox3 = FindViewById<CheckBox>(Resource.Id.CheckBox3);

            checkBox1.Click += HandleCheckBoxClick;
            checkBox2.Click += HandleCheckBoxClick;
            checkBox3.Click += HandleCheckBoxClick;
        }

        void HandleCheckBoxClick(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;

            if (checkBox?.ContentDescription == null)
                return;

            var checkBoxNumber = int.Parse(checkBox?.ContentDescription.Substring(8));
            checkBox.Text = checkBox.Checked ? $"Check Box {checkBoxNumber} is Checked" : $"Check Box {checkBoxNumber} is Unchecked";
        }
    }
}

