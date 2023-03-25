using AccountChecker.Data;
using AccountChecker.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountChecker.UI;

public partial class FeatureTasks : Form
{

    private readonly CoreUtility _coreUtility;

    public FeatureTasks()
    {
        _coreUtility = new CoreUtility();
        InitializeComponent();
        LoadTasksConfig();

        //ClaimEventRewardsCheckBox.Enabled = false;
        BuyChampionShardsCheckBox.Enabled = false;
        BuyBlueEssenceCheckBox.Enabled = false;
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        this.Hide();

        // if main form exists, only hide settings dialog
        foreach (Form form in Application.OpenForms)
        {
            if (form.GetType() == typeof(Main))
            {
                return;
            }
        }

        // if main form doesnt exist, close app
        Application.Exit();

    }

    private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
    {

    }

    private void panel39_Paint(object sender, PaintEventArgs e)
    {

    }

    private void panel40_Paint(object sender, PaintEventArgs e)
    {
    }

    private void panel41_Paint(object sender, PaintEventArgs e)
    {
    }

    private void panel49_Paint(object sender, PaintEventArgs e)
    {
    }

    private void panel57_Paint(object sender, PaintEventArgs e)
    {
    }

    private void panel65_Paint(object sender, PaintEventArgs e)
    {
    }

    private void panel66_Paint(object sender, PaintEventArgs e)
    {
    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {

    }

    private void FeatureTasks_Load(object sender, EventArgs e)
    {

    }

    private void OpenChestsCheckBox_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void label45_Click(object sender, EventArgs e)
    {
    }

    private void RemoveFriendRequestsCheckBox_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void SaveTasks()
    {
        //var tasks = GetTaskCheckBoxes();


    }

    private Dictionary<string, bool> GetTaskCheckBoxes()
    {
        return new Dictionary<string, bool>()
        {
            {CraftKeysCheckBox.Name, CraftKeysCheckBox.Checked},
            {OpenChestsCheckBox.Name, OpenChestsCheckBox.Checked},
            {DisenchantChampionShardsCheckBox.Name, DisenchantChampionShardsCheckBox.Checked},
            {DisenchantEternalShardsCheckBox.Name, DisenchantEternalShardsCheckBox.Checked},
            {OpenCapsulesOrbsShardsCheckBox.Name, OpenCapsulesOrbsShardsCheckBox.Checked},
            {DisenchantSkinShardsCheckBox.Name, DisenchantSkinShardsCheckBox.Checked},
            {BuyChampionShardsCheckBox.Name, BuyChampionShardsCheckBox.Checked},
            {ClaimEventRewardsCheckBox.Name, ClaimEventRewardsCheckBox.Checked},
            {BuyBlueEssenceCheckBox.Name, BuyBlueEssenceCheckBox.Checked },
            {DisenchantWardSkinShardsCheckBox.Name, DisenchantWardSkinShardsCheckBox.Checked},
            {RemoveFriendRequestsCheckBox.Name, RemoveFriendRequestsCheckBox.Checked},
            {RemoveFriendsCheckBox.Name, RemoveFriendsCheckBox.Checked}

        };
    }

    private async void LoadTasksConfig()
    {
        var tasks = await _coreUtility.ReadFromTasksConfigFile();

        foreach (var field in typeof(TasksConfig).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            if (field.FieldType != typeof(string))
            {
                continue;
            }

            if (Controls.Find(field.GetValue(null).ToString(), true).FirstOrDefault() is not CheckBox checkBox)
            {
                continue;
            }

            if (tasks.TryGetValue(field.GetValue(null).ToString(), out bool value))
            {
                checkBox.Checked = value;
            }
        }
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        // Get Tasks
        var tasks = GetTaskCheckBoxes();

        // Update Task file
        _coreUtility.OverwriteTaskConfigFile(tasks);

        this.Hide();
    }
}
