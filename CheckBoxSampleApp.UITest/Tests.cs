using System;
using System.Linq;

using NUnit.Framework;

using Xamarin.UITest;
using Xamarin.UITest.Android;

using CheckBoxSampleApp.Shared;

namespace CheckBoxSampleApp.UITest
{
    [TestFixture]
    public class Tests
    {
        AndroidApp _app;

        [SetUp]
        public void BeforeEachTest()
        {
            _app = ConfigureApp.Android.PreferIdeSettings().StartApp();
            _app.Screenshot("First screen.");
        }

        [Ignore]
        [Test]
        public void ReplTest() => _app.Repl();

        [Test]
        public void VerifyButtonClick_VerifyButtonColor_VerifyTextViewColor()
        {
            //Arrange
            var expectedButtonTextColorAsInt = ConvertAndroidDrawingHexColorToInt("#ef4444");
            var expectedTextViewTextColorAsInt = ConvertAndroidDrawingHexColorToInt("#ef4444");
            var expectedTextViewText = "1 Check Box Is Checked";

            //Act
            ToggleCheckBox(AutomationConstants.CheckBox1);
            _app.Tap(x => x.Marked(AutomationConstants.Button1));
            _app.WaitForElement(x => x.Marked(AutomationConstants.TextView1));

            //Assert
            var actualButtonColorAsInt = GetHexColorAsInt(AutomationConstants.Button1);
            var actualTextViewTextColorAsInt = GetHexColorAsInt(AutomationConstants.TextView1);
            var actualTextViewText = _app.Query(x => x.Marked(AutomationConstants.TextView1))?.FirstOrDefault()?.Text;

            Assert.AreEqual(expectedButtonTextColorAsInt, actualButtonColorAsInt);
            Assert.AreEqual(expectedTextViewTextColorAsInt, actualTextViewTextColorAsInt);
            Assert.AreEqual(expectedTextViewText, actualTextViewText);
        }

        [TestCase(AutomationConstants.CheckBox1)]
        [TestCase(AutomationConstants.CheckBox2)]
        [TestCase(AutomationConstants.CheckBox3)]
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

        [TestCase(AutomationConstants.CheckBox1, true)]
        [TestCase(AutomationConstants.CheckBox2, true)]
        [TestCase(AutomationConstants.CheckBox3, true)]
        [TestCase(AutomationConstants.CheckBox1, false)]
        [TestCase(AutomationConstants.CheckBox2, false)]
        [TestCase(AutomationConstants.CheckBox3, false)]
        public void SetIndividualCheckBox(string textBoxContentDescription, bool isChecked)
        {
            //Arrange
            bool isCheckBoxChecked;

            //Act
            SetCheckBox(textBoxContentDescription, isChecked);
            isCheckBoxChecked = IsCheckBoxChecked(textBoxContentDescription);

            //Assert
            Assert.AreEqual(isChecked, isCheckBoxChecked);
        }

        [Test]
        public void ToggleAllCheckBoxes()
        {
            //Arrange

            //Act
            _app.Query(x => x.Class("CheckBox").Invoke("performClick"));
            _app.Screenshot("All CheckBoxes Checked");

            //Assert
            var isCheckBoxCheckedArray = _app.Query(x => x.Class("CheckBox").Invoke("isChecked")).Select(x => (bool)x).ToList();
            for (int i = 0; i < isCheckBoxCheckedArray.Count; i++)
                Assert.IsTrue(isCheckBoxCheckedArray[i], $"Check box {i} is not checked");

            //Act
            _app.Query(x => x.Class("CheckBox").Invoke("performClick"));
            _app.Screenshot("All CheckBoxes Unchecked");

            //Assert
            isCheckBoxCheckedArray = _app.Query(x => x.Class("CheckBox").Invoke("isChecked")).Select(x=>(bool)x).ToList();
            for (int i = 0; i < isCheckBoxCheckedArray.Count; i++)
                Assert.False(isCheckBoxCheckedArray[i], $"Check box {i} is checked");
        }

        void ToggleCheckBox(string checkBoxContentDescription)
        {
            _app.Query(x => x.Marked(checkBoxContentDescription).Invoke("performClick"));
            _app.Screenshot($"Toggled {checkBoxContentDescription}");
        }

        bool IsCheckBoxChecked(string textBoxContentDescription) =>
            (bool)_app.Query(x => x.Marked(textBoxContentDescription).Invoke("isChecked")).First();

        void SetCheckBox(string checkBoxContentDescription, bool IsChecked)
        {
            _app.Query(x => x.Marked(checkBoxContentDescription).Invoke("setChecked", IsChecked));
            _app.Screenshot($"Set {checkBoxContentDescription} to {IsChecked}");
        }

        int GetHexColorAsInt(string contentDescription) =>
           int.Parse(_app.Query(x => x.Marked(contentDescription).Invoke("getCurrentTextColor")).First().ToString());

        int ConvertAndroidDrawingHexColorToInt(string colorStringAsHex)
        {
            if (!colorStringAsHex[0].Equals('#') || colorStringAsHex.Length != 7)
                throw new Exception("Invalid Hex String. Color string must start with \"#\" and be followed by 6 hexadecimal characters");

            return int.Parse(_app.Invoke("GetColorAsInt", colorStringAsHex).ToString());
        }
    }
}

