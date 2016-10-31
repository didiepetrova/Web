using Telerik.TestingFramework.Controls.KendoUI;
using Telerik.WebAii.Controls.Html;
using Telerik.WebAii.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Design;
using ArtOfTest.WebAii.Design.Execution;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;

using ArtOfTest.WebAii.Win32.Dialogs;
using System.Windows.Forms;

namespace Web
{

    public class Dialog : BaseWebAiiTest
    {

        string dialogText;

[CodedStep(@"Navigate then verify text in popup dialog")]
public void VerifyDialogText_CodedStep()
{
    ActiveBrowser.NavigateTo("http://www.w3schools.com/JS/tryit.asp?filename=tryjs_alert");

    

    // Click the button to fire the Alert Dialog inside the browser
    FrameInfo myFrame = new FrameInfo("iframeResult", "", "", 0);
    Browser frame = ActiveBrowser.Frames[myFrame];
    HtmlButton tryItButton = frame.Find.ByTagIndex<HtmlButton>("button", 0);
    
    //Console.WriteLine(tryItButton);
    
    Assert.IsNotNull(tryItButton);
    
    
    // Initialize custom 'Alert' dialog handler
    AlertDialog alertDialog = AlertDialog.CreateAlertDialog(ActiveBrowser, DialogButton.OK);
    Manager.DialogMonitor.Start();
    alertDialog.HandlerDelegate = MyCustomAlertHandler;
    Manager.DialogMonitor.AddDialog(alertDialog);
    tryItButton.Click();
    
    // Wait Until Dialog is Handled.
    alertDialog.WaitUntilHandled(20000);

    // Validate the text that was captured by the custom dialog handler
    Assert.AreEqual<string>("I am an alert box!", dialogText);
    
}

public void MyCustomAlertHandler(IDialog dialog)
{
    // Capture the text displayed in the dialog. The contents will be validated by the main thread.
    dialogText = dialog.Window.AllChildren[dialog.Window.AllChildren.Count - 1].Caption;
    Log.WriteLine("Dialog text: " + dialogText);

    Manager.Desktop.KeyBoard.KeyPress(Keys.Enter);
    dialog.HandleCount++;
}
    }
}
