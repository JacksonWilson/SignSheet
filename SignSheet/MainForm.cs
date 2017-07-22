using System;
using System.IO;
using System.Windows.Forms;

namespace SignSheet
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            countryCB.SelectedIndex = 234;
        }

        public void ToggleFullscreen()
        {
            if (WindowState == FormWindowState.Normal)
            {
                fullScreenBtn.Visible = false;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                TopMost = true;
            }
            else
            {
                fullScreenBtn.Visible = true;
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                TopMost = false;
            }
        }

        private void FullScreenBtn_Click(object sender, EventArgs e)
        {
            ToggleFullscreen();
        }

        private void NUD_Enter(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            if (nud.Value == 0)
                nud.Text = string.Empty;
            else
                nud.Select(0, nud.Text.Length);
        }

        private void NUD_Leave(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            if (nud.Text == string.Empty)
                nud.Text = "0";
        }

        private void WithinUSA_ChB_CheckedChanged(object sender, EventArgs e)
        {
            cityRF_Lb.Visible = stateRF_Lb.Visible = zipRF_Lb.Visible = ((CheckBox)sender).Checked;
        }

        private void CountryCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex == 234)
            {
                cityRF_Lb.Visible = stateRF_Lb.Visible = zipRF_Lb.Visible = true;
                stateLb.Visible = zipLb.Visible = stateCB.Visible
                    = zipTB.Visible = stateCB.Enabled = zipTB.Enabled = true;
            }
            else
            {
                cityRF_Lb.Visible = stateRF_Lb.Visible = zipRF_Lb.Visible = false;
                stateLb.Visible = zipLb.Visible = stateCB.Visible
                    = zipTB.Visible = stateCB.Enabled = zipTB.Enabled = false;
            }
        }

        private bool ValidateData(out int validatedZip)
        {
            bool ret = true;
            string errorMessage = "";
            validatedZip = -1;

            if (string.IsNullOrEmpty(firstNameTB.Text))
            {
                ret = false;
                firstNameLb.ForeColor = System.Drawing.Color.Red;
                errorMessage += "First name is empty.\n";
            }

            if (string.IsNullOrEmpty(lastNameTB.Text))
            {
                ret = false;
                lastNameLb.ForeColor = System.Drawing.Color.Red;
                errorMessage += "Last name is empty.\n";
            }

            if (countryCB.SelectedIndex == 234 && string.IsNullOrEmpty(cityTB.Text))
            {
                ret = false;
                cityLb.ForeColor = System.Drawing.Color.Red;
                errorMessage += "City is empty.\n";
            }

            if (stateCB.Enabled && stateCB.SelectedIndex == -1)
            {
                ret = false;
                stateLb.ForeColor = System.Drawing.Color.Red;
                errorMessage += "No state selected.\n";
            }

            if (zipTB.Enabled && string.IsNullOrEmpty(zipTB.Text))
            {
                ret = false;
                zipLb.ForeColor = System.Drawing.Color.Red;
                errorMessage += "Zip is empty.\n";
            }
            else if (zipTB.Enabled)
            {
                if (!Int32.TryParse(zipTB.Text, out validatedZip))
                {
                    ret = false;
                    validatedZip = -1;
                    zipLb.ForeColor = System.Drawing.Color.Red;
                    errorMessage += "Zip is invalid. (must be 6 digits)\n";
                }
            }

            if (seniorsNUD.Value == 0 && adultsNUD.Value == 0 && childOverNUD.Value == 0 && childUnderNUD.Value == 0)
            {
                ret = false;
                numPartyLb.ForeColor = System.Drawing.Color.Red;
                errorMessage += "At least 1 in a party.\n";
            }

            if (!string.IsNullOrEmpty(emailAddrTB.Text) && emailAddrTB.Text.IndexOf('@') == -1)
            {
                ret = false;
                emailAddrLb.ForeColor = System.Drawing.Color.Red;
                errorMessage += "Email is not valid.\n";
            }

            if (errorMessage != string.Empty)
            {
                validationErrorTitleLb.Visible = true;
                validationErrorLb.Text = errorMessage;
            }
            return ret;
        }

        private void DoneBtn_Click(object sender, EventArgs e)
        {
            validationErrorTitleLb.Visible = false;
            validationErrorLb.Text = string.Empty;

            firstNameLb.ForeColor = System.Drawing.SystemColors.ControlText;
            lastNameLb.ForeColor = System.Drawing.SystemColors.ControlText;
            countryLb.ForeColor = System.Drawing.SystemColors.ControlText;
            stateLb.ForeColor = System.Drawing.SystemColors.ControlText;
            cityLb.ForeColor = System.Drawing.SystemColors.ControlText;
            zipLb.ForeColor = System.Drawing.SystemColors.ControlText;
            numPartyLb.ForeColor = System.Drawing.SystemColors.ControlText;
            emailAddrLb.ForeColor = System.Drawing.SystemColors.ControlText;

            firstNameTB.Text = firstNameTB.Text.Trim();
            lastNameTB.Text = lastNameTB.Text.Trim();
            cityTB.Text = cityTB.Text.Trim();
            zipTB.Text = zipTB.Text.Trim();
            emailAddrTB.Text = emailAddrTB.Text.Trim();

            if (ValidateData(out int validatedZip))
            {
                SignSheet signSheet = new SignSheet()
                {
                    FirstName = firstNameTB.Text,
                    LastName = lastNameTB.Text,
                    EmailAddr = emailAddrTB.Text,
                    CountryIndex = countryCB.SelectedIndex,
                    StateIndex = stateCB.SelectedIndex,
                    City = cityTB.Text,
                    NumInParty = new int[] { (int)seniorsNUD.Value, (int)adultsNUD.Value, (int)childOverNUD.Value, (int)childUnderNUD.Value },
                    Zip = validatedZip
                };

                // Clear all fields
                firstNameTB.Text = string.Empty;
                lastNameTB.Text = string.Empty;
                emailAddrTB.Text = string.Empty;
                countryCB.SelectedIndex = 234;
                cityTB.Text = string.Empty;
                stateCB.SelectedIndex = -1;
                stateCB.Text = string.Empty;
                zipTB.Text = string.Empty;
                seniorsNUD.Value = 0;
                adultsNUD.Value = 0;
                childOverNUD.Value = 0;
                childUnderNUD.Value = 0;

                try
                {
                    string path = $"PAM_SignIn_Export.csv";

                    if (!File.Exists(path))
                        File.AppendAllText(path, SignSheet.Header + "\n");

                    using (StreamWriter sw = File.AppendText(path))
                        sw.WriteLine(signSheet.ToString());

                    closeMessageLb.Text = "Thank You!";
                    closeMessageLb.Visible = true;
                    firstNameTB.Select();
                    Timer timer = new Timer()
                    {
                        Interval = 3000
                    };
                    timer.Tick += (source, e2) => { closeMessageLb.Visible = false; timer.Stop(); };
                    timer.Start();
                }
                catch (Exception exc)
                {
                    closeMessageLb.Text = "Oops something broke.\nPlease ask for assistance.";
                    File.WriteAllText($"Error_Message [{DateTime.Now.ToString("MM-dd-yyyy h-mm-ss tt")}].txt", $"Message:\n{exc.Message}\n\nStack Trace:\n{exc.StackTrace}");
                }
            }
        }
    }
}
