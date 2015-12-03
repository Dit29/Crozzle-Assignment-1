using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class Form1 : Form
    {
        private Crozzle crozzleGame;

        #region constructors
        public Form1()
        {
            InitializeComponent();

            crozzleGame = new Crozzle();
        }
        #endregion

        #region file menu event handlers
        // Open and validate Wordlist File.
        private void openWordlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;

            // Disable all other open button before the wordlist file being opened and validated.
            // Open Crozzle button, Open Wordlist button and Crozzle Validator are disabled.
            // Also reset the GUI.
            openWordlistToolStripMenuItem.Checked = false;
            openCrozzleToolStripMenuItem.Checked = false;
            validateCrozzleToolStripMenuItem.Checked = false;
            validateCrozzleToolStripMenuItem.Enabled = false;
            scoreContainer.Text = "";
            crozzleArea.Text = "";

            // Process wordlist file
            result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                // reead wordlist crozzleGame
                crozzleGame.readWordlistFile(openFileDialog1.FileName);
                if (crozzleGame.WordlistFileValidator)
                {
                    openWordlistToolStripMenuItem.Checked = true;
                    MessageBox.Show("Wordlist file is valid");
                }
                else
                    MessageBox.Show("Wordlist file is invalid");
                openCrozzleToolStripMenuItem.Enabled = true;
            }
        }

        // Open and validate Crozzle File.
        private void openCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;

            // Disable all other open button before the crozzle file being opened and validated.
            // Open Crozzle button, and Crozzle Validator are disabled.
            // Also reset the GUI.
            openCrozzleToolStripMenuItem.Checked = false;
            validateCrozzleToolStripMenuItem.Checked = false;
            validateCrozzleToolStripMenuItem.Enabled = false;
            scoreContainer.Text = "";
            crozzleArea.Text = "";

            // Process crozzle file.
            result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                crozzleGame.readCrozzleFile(openFileDialog1.FileName);
                if (crozzleGame.CrozzleFileValidator)
                {
                    openCrozzleToolStripMenuItem.Checked = true;
                    MessageBox.Show("Crozzle is valid");
                }
                else
                {
                    MessageBox.Show("Crozzle file is invalid");
                    scoreContainer.Text = "0"; // Default opening score.
                }

                validateCrozzleToolStripMenuItem.Enabled = true;

                if (openWordlistToolStripMenuItem.Checked && openCrozzleToolStripMenuItem.Checked)
                    validateCrozzleToolStripMenuItem.Enabled = true;

                // Display the crozzle even it's invalid.
                crozzleArea.Text = crozzleGame.ToString();
            }
        }

        // Exit button.
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region validate crozzle event handler
        private void validateCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the crozzle is valid.
            crozzleGame.validateCrozzle(openFileDialog1.FileName);

            // Get score.
            String score = "";
            if (crozzleGame.CrozzleValidator)
            {
                validateCrozzleToolStripMenuItem.Checked = true;
                score = crozzleGame.score().ToString();
            }
            else
            {
                validateCrozzleToolStripMenuItem.Checked = false;
                score = "INVALID";
                MessageBox.Show("INVALID CROZZLE");
            }

            scoreContainer.Text = score;
        }       
        #endregion
    }
}
