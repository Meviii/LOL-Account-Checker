using LOLClient.DataFiles;
using LOLClient.Models;
using LOLClient.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOLClient.Utility;

/*
 * This class provides utility methods for the application UI.
 */
public class UIUtility
{
    public UIUtility() { }

    // This method is in charge of switching to (or creating) the Main form.
    public void LoadMainView()
    {
        // Loops over each active form and makes it visible, else, creates new instance of the form
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(Main))
            {

                form.Visible = true;
                form.Focus();
                return;
            }
        }

        new Main().Show();
    }

    // This method is in charge of switching to the Single Account form.
    public void LoadSingleAccountView(Account account)
    {
        // Loops over each form and closes the existant Single Account form and creates a new instance.
        // This is to ensure that an updated account of the current application instance is also updated in this view.
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(SingleAccount))
            {
                form.Close();
            }
        }

        new SingleAccount(account).Show();
    }

    // This method is in charge of switching to the Accounts List form.
    public void LoadAccountsListView()
    {
        // Loops over active forms and closes the instance of an AccountList if initiated.
        // This is to ensure that a new account that is checked is also visible in the view.
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(AccountList))
            {
                form.Hide();
            }
        }
        
        new AccountList().Show();

    }

    // This method is responsible of opening the Settings Form as a Dialog.
    public void LoadSettingsViewAsDialog()
    {

        // Loops over active forms and shows (or creates) the Settings form.
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(Settings))
            {
                form.Visible = true;
                form.Show();
                return;
            }
        }

        new Settings().ShowDialog();
    }
}
