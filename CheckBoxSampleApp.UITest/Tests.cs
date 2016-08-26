using NUnit.Framework;

using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace CheckBoxSampleApp.UITest
{
	[TestFixture]
	public class Tests
	{
		AndroidApp app;

		[SetUp]
		public void BeforeEachTest()
		{
			app = ConfigureApp
				.Android
				.InstalledApp("CheckBoxSampleApp.CheckBoxSampleApp")
				.PreferIdeSettings()
				.StartApp();

			app.Screenshot("First screen.");
		}

		[Ignore]
		[Test]
		public void ReplTest()
		{
			app.Repl();
		}

		[TestCase("CheckBox1")]
		[TestCase("CheckBox2")]
		[TestCase("CheckBox3")]
		[Test]
		public void ToggleIndividualCheckBox(string textBoxContentDescription)
		{
			//Arrange
			bool isCheckBoxChecked;

			//Act
			ToggleCheckBox(textBoxContentDescription);

			//Assert
			isCheckBoxChecked = IsCheckBoxChecked(textBoxContentDescription);
			Assert.IsTrue(isCheckBoxChecked, "The check box is not checked");

			//Act
			ToggleCheckBox(textBoxContentDescription);

			//Assert
			isCheckBoxChecked = IsCheckBoxChecked(textBoxContentDescription);
			Assert.IsFalse(isCheckBoxChecked, "The check box is not checked");
		}

		[Test]
		public void ToggleAllCheckBoxes()
		{
			//Arrange
			object[] isCheckBoxCheckedArray;

			//Act
			app.Query(x => x.Class("CheckBox").Invoke("performClick"));

			//Assert
			isCheckBoxCheckedArray = app.Query(x => x.Class("CheckBox").Invoke("isChecked"));
			for (int i = 0; i < isCheckBoxCheckedArray.Length; i++)
				Assert.IsTrue((bool)isCheckBoxCheckedArray[i], $"Check box {i} is not checked");

			//Act
			app.Query(x => x.Class("CheckBox").Invoke("performClick"));

			//Assert
			isCheckBoxCheckedArray = app.Query(x => x.Class("CheckBox").Invoke("isChecked"));
			for (int i = 0; i < isCheckBoxCheckedArray.Length; i++)
				Assert.False((bool)isCheckBoxCheckedArray[i], $"Check box {i} is checked");
		}

		void ToggleCheckBox(string textBoxContentDescription)
		{
			app.Query(x => x.Marked(textBoxContentDescription).Invoke("performClick"));
			app.Screenshot($"Toggled {textBoxContentDescription}");
		}

		bool IsCheckBoxChecked(string textBoxContentDescription)
		{
			return (bool)app.Query(x => x.Marked(textBoxContentDescription).Invoke("isChecked"))[0];
		}

	}
}

