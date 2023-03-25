using AccountChecker.DataFiles;
using AccountChecker.Models;
using AccountChecker.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountChecker.Utility;

/*
 * This class provides utility methods for the application UI.
 */
public class UIUtility
{
    public UIUtility() { }

    // This method is in charge of switching to (or creating) the Main form while preserving the same location as passed Form.
    public void LoadMainView(Form currentForm)
    {
        // Loops over each active form and makes it visible, else, creates new instance of the form
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(Main))
            {

                form.Location = currentForm.Location;
                form.Visible = true;
                form.Focus();
                return;
            }
        }

        var newForm = new Main
        {
            Location = currentForm.Location
        };
        newForm.Show();
    }

    // This method is in charge of switching to (or creating) the Main form without preserving the location of the passed form.
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

        var newForm = new Main();
        newForm.Show();
    }

    // This method is in charge of switching to the Single Account form.
    public void LoadSingleAccountView(Account account, Form currentForm)
    {
        // Loops over each form and closes the existant Single Account form and creates a new instance.
        // This is to ensure that an updated account of the current application instance is also updated in this view.
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(SingleAccount))
            {
                form.Hide();
            }
        }

        var newForm = new SingleAccount(account)
        {
            Location = currentForm.Location
        };
        newForm.Show();
    }

    // This method is in charge of switching to the Accounts List form.
    public void LoadAccountsListView(Form currentForm)
    {
        // Loops over active forms and closes the instance of an AccountList if initiated.
        // This is to ensure that a new account that is checked is also visible in the view.
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(AccountList))
            {
                form.Location = currentForm.Location;
                form.Visible = true;
                form.Focus();
                return;
            }
        }

        var accountList = new AccountList
        {
            Location = currentForm.Location
        };
        accountList.Show();

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
                form.Focus();
                return;
            }
        }

        new Settings().ShowDialog();
    }

    // This method is responsible of opening the Tasks Form as a Dialog.
    public void LoadTasksViewAsDialog()
    {

        // Loops over active forms and shows (or creates) the Tasks form.
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(FeatureTasks))
            {
                form.Visible = true;
                form.Show();
                form.Focus();
                return;
            }
        }

        new FeatureTasks().ShowDialog();
    }

    // This method initializes the provided progress bar.
    public void InitializeProgressBar(ProgressBar progressBar, int accountCount)
    {

        if (progressBar.InvokeRequired)
        {
            progressBar.Invoke(new Action(() => InitializeProgressBar(progressBar, accountCount)));
        }
        else
        {

            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = accountCount;
        }
    }

    // This method increments the provided progress bar.
    public void IncrementProgressBar(ProgressBar progressBar)
    {

        if (progressBar.InvokeRequired)
        {
            progressBar.Invoke(new Action(() => IncrementProgressBar(progressBar)));
        }
        else
        {

            if (progressBar.Maximum >= progressBar.Value + 1)
                progressBar.Value += 1;
        }
    }
}
