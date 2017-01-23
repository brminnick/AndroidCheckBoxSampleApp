# AndroidCheckBoxSampleApp
This is a simple app to demonstrate how to interact with an Android CheckBox from a UITest.

To interact with an Android CheckBox from a UITest, you must use the [Invoke](https://developer.xamarin.com/api/member/Xamarin.UITest.Queries.AppQuery.Invoke/p/System.String/) method to access the methods in the [native Java Android API](https://developer.android.com/reference/android/widget/CheckBox.html). In our `Invoke` statements, we can take advantage of `performClick()` to toggle the CheckBox, `setChecked(boolean checked)` to set the value of the CheckBox, and `isChecked()` to return a bool that is `true` when the CheckBox is checked and `false` when it is unchecked.

In the UITest project, I created a [ToggleCheckBox](https://github.com/brminnick/AndroidCheckBoxSampleApp/blob/master/CheckBoxSampleApp.UITest/Tests.cs#L128) method to toggle an individual CheckBox, a [IsCheckBoxChecked](https://github.com/brminnick/AndroidCheckBoxSampleApp/blob/master/CheckBoxSampleApp.UITest/Tests.cs#L134) method to return the CheckBox current status, and a [SetCheckBox](https://github.com/brminnick/AndroidCheckBoxSampleApp/blob/master/CheckBoxSampleApp.UITest/Tests.cs#L139) method to set the CheckBox status.

All tests were validated via [Xamarin Test Cloud](https://www.xamarin.com/test-cloud). The test report is viewable [here](https://testcloud.xamarin.com/test/checkboxsampleapp_198eddc7-356c-46db-b88b-7e82ce7af2f6/).

Author
===
Brandon Minnick

Xamarin Customer Success Engineer
