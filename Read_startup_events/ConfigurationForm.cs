using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Read_startup_events
{
    public partial class ConfigurationForm : Form
    {
        private ReadStartupEvents objPlugin;
        public ConfigurationForm(ref ReadStartupEvents Plugin)
        {
            InitializeComponent();
            this.objPlugin = Plugin;
            this.eventId.Text = this.objPlugin.GetValueForKey("Event Id");
            this.timeInMinutes.Text = this.objPlugin.GetValueForKey("Time in minutes");
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.eventId = new System.Windows.Forms.TextBox();
            this.timeInMinutes = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(471, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter the event id for which to check at startup";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(626, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please enter the time for how long back to check (less than 15 minutes)";
            // 
            // eventId
            // 
            this.eventId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventId.Location = new System.Drawing.Point(37, 90);
            this.eventId.MaxLength = 6;
            this.eventId.Name = "eventId";
            this.eventId.Size = new System.Drawing.Size(100, 30);
            this.eventId.TabIndex = 2;
            // 
            // timeInMinutes
            // 
            this.timeInMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeInMinutes.Location = new System.Drawing.Point(37, 192);
            this.timeInMinutes.MaxLength = 2;
            this.timeInMinutes.Name = "timeInMinutes";
            this.timeInMinutes.Size = new System.Drawing.Size(100, 30);
            this.timeInMinutes.TabIndex = 3;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(384, 238);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(101, 33);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonCancel_MouseClick);
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(276, 238);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 33);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonOK_MouseClick);
            // 
            // ConfigurationForm
            // 
            this.ClientSize = new System.Drawing.Size(528, 301);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.timeInMinutes);
            this.Controls.Add(this.eventId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 357);
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.Text = "Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label label1;
        private Label label2;
        private TextBox eventId;
        private TextBox timeInMinutes;
        private Button buttonCancel;
        private Button buttonOK;
        private bool am = false;
        private bool am1 = false;
        //private int a1 = 0;
        //private int b1 = 0;
        private string msg = null;
        private string msg2 = null;

        private void buttonOK_MouseClick(object sender, MouseEventArgs e)
        {
            int a = 0;
            int b = 0;
            am = false;
            am1 = false;
            a = validate(eventId.Text, "Event Id");
            b = validate(timeInMinutes.Text, "Time in minutes");
            bool av = checkRange(a, 0, 99999, "Event Id");
            bool av1 = checkRange(b, 0, 15, "Time in minutes");

            if (am == true && am1 == true && av == true && av1 == true)
            {

                this.objPlugin.SetValueForKey("Event Id", a.ToString());
                this.objPlugin.SetValueForKey("Time in minutes", b.ToString());
                this.Close();
            }
            else
            {
                string atext = null;
                if (am == false || am1 == false)
                {
                    atext = "Hey, we need an intger for " + msg + ".";
                    
                }
                if (av == false || av1 == false)
                {
                    atext += " The " + msg2 + " is out of range";
                }
                //else
                //{
                //    atext = "Hey, we need an intger for " + msg + ". \n The " + msg2 + " is out of range";
                //}
                MessageBox.Show(atext);
                atext = null;
                msg = null;
                msg2 = null;
            }
        }

        private bool checkRange(int x, int min, int max, string str)
        {

                if (x < min || x > max)
                {
                    if (msg2 == null)
                    {
                        msg2 = str;
                        return false;
                    }
                    else
                    {
                        msg2 += " and " + str;
                        return false;
                    }
                
            }
            return true;
        }

        private int validate(string xz, string box)
        {
            int val = -1;
            if (!int.TryParse(xz, out val))
            {
                if (msg == null)
                {
                    msg = box;
                }
                else
                {
                    msg += " and " + box;
                }
                if (box == "Event Id")
                { am = false; }
                if (box == "Time in minutes")
                { am1 = false; }
                return val;
            }
            if (box == "Event Id")
            { am = true; }
            if (box == "Time in minutes")
            { am1 = true; }
            return val;
        }

        private void buttonCancel_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
