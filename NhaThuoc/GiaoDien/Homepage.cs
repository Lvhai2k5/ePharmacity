using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NhaThuoc
{
    public partial class Homepage : Form
    {
        private Timer transitionTimer;
        private int transitionStep = 0;
        private const int TRANSITION_STEPS = 20;
        private const int TRANSITION_INTERVAL = 20;
        private bool isTransitioning = false;
        private string currentPanel = "login";
        private string targetPanel = "login";
        public Homepage()
        {
            InitializeComponent();
            this.Size = new Size(1000, 500);
            panelLogin.Size = new Size(1000, 500);
            panelRegister.Size = new Size(1000, 500);
            panelForgotpassword.Size = new Size(1000, 500);
            SetupTransition();
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelRegister);
            this.Controls.Add(this.panelForgotpassword);
            panelLogin.Location = new Point(0, 0);
            panelRegister.Location = new Point(this.Width, 0);
            panelForgotpassword.Location = new Point(-this.Width, 0);
            this.linkLabelRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRegister_LinkClicked);
            this.linkLabelForgotpassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelForgotpassword_LinkClicked);
            this.linkLabelLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLogin_LinkClicked);
            this.linkLabel1Login.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1Login_LinkClicked);
            currentPanel = "login";
        }

        //private void panel1_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    int halfWidth = panelLogin.Width / 2;

        //    // Nửa trái
        //    Rectangle leftRect = new Rectangle(0, 0, halfWidth, panelLogin.Height);
        //    using (SolidBrush leftBrush = new SolidBrush(ColorTranslator.FromHtml("#f1dacc")))
        //        g.FillRectangle(leftBrush, leftRect);

        //    // Nửa phải
        //    Rectangle rightRect = new Rectangle(halfWidth, 0, halfWidth, panelLogin.Height);
        //    using (SolidBrush rightBrush = new SolidBrush(ColorTranslator.FromHtml("#38b6ff")))
        //        g.FillRectangle(rightBrush, rightRect);
        //}
        private void ShowPanel(Panel panel)
        {
            panelLogin.Visible = false;
            panelRegister.Visible = false;
            panelForgotpassword.Visible = false;
            panel.Visible = true;
            panel.BringToFront();
        }


        private void SetupTransition()
        {
            transitionTimer = new Timer();
            transitionTimer.Interval = TRANSITION_INTERVAL;
            transitionTimer.Tick += TransitionTimer_Tick;
        }

        private void TransitionTimer_Tick(object sender, EventArgs e)
        {
            if (isTransitioning)
            {
                transitionStep++;
                float progress = (float)transitionStep / TRANSITION_STEPS;

                // Easing function (ease-in-out)
                float easedProgress = progress < 0.5f ?
                    2 * progress * progress :
                    1 - (float)Math.Pow(-2 * progress + 2, 2) / 2;

                if (targetPanel == "register")
                {
                    this.BackColor = ColorTranslator.FromHtml("#38b6ff");
                    // Slide login panel to left, register panel from right
                    panelLogin.Location = new Point((int)(-this.Width * easedProgress), 0);
                    panelRegister.Location = new Point((int)(this.Width * (1 - easedProgress)), 0);
                    
                }
                else if (targetPanel == "login")
                {
                    if (currentPanel == "register")
                    {
                        //panelLogin.Paint += panel1_Paint;
                        //this.BackColor = ColorTranslator.FromHtml("#f1dacc");
                        // Slide register panel to right, login panel from left
                        panelRegister.Location = new Point((int)(this.Width * easedProgress), 0);
                        panelLogin.Location = new Point((int)(-this.Width * (1 - easedProgress)), 0);
                    }
                    else if (currentPanel == "forgot")
                    {
                        //panelLogin.Paint += panel1_Paint;
                        // this.BackColor = ColorTranslator.FromHtml("#f1dacc");
                        // Slide forgot panel to left, login panel from right
                        panelForgotpassword.Location = new Point((int)(-this.Width * easedProgress), 0);
                        panelLogin.Location = new Point((int)(this.Width * (1 - easedProgress)), 0);
                    }
                }
                else if (targetPanel == "forgot")
                {
                    this.BackColor = ColorTranslator.FromHtml("#f1dacc");
                    // Slide login panel to right, forgot panel from left
                    panelLogin.Location = new Point((int)(this.Width * easedProgress), 0);
                    panelForgotpassword.Location = new Point((int)(-this.Width * (1 - easedProgress)), 0);
                }

                if (transitionStep >= TRANSITION_STEPS)
                {
                    transitionTimer.Stop();
                    isTransitioning = false;
                    transitionStep = 0;
                    currentPanel = targetPanel;

                    // Reset panel positions
                    if (currentPanel == "login")
                    {
                        panelLogin.Location = new Point(0, 0);
                        panelRegister.Location = new Point(this.Width, 0);
                        panelForgotpassword.Location = new Point(-this.Width, 0);
                    }
                    else if (currentPanel == "register")
                    {
                        panelRegister.Location = new Point(0, 0);
                        panelLogin.Location = new Point(-this.Width, 0);
                        panelForgotpassword.Location = new Point(-this.Width, 0);
                    }
                    else if (currentPanel == "forgot")
                    {
                        panelForgotpassword.Location = new Point(0, 0);
                        panelLogin.Location = new Point(this.Width, 0);
                        panelRegister.Location = new Point(this.Width, 0);
                    }
                }
            }
        }

        private void StartTransition(string newPanel)
        {
            if (!isTransitioning && currentPanel != newPanel)
            {
                targetPanel = newPanel;
                isTransitioning = true;
                transitionStep = 0;
                transitionTimer.Start();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Y <= 40 && e.X >= this.Width - 40)
            {
                this.Close();
            }
            base.OnMouseClick(e);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1Login_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartTransition("login");
        }

        private void linkLabelLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartTransition("login");
        }

        private void linkLabelForgotpassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartTransition("forgot");
        }

        private void linkLabelRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartTransition("register");
        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            panelLogin.Location = new Point(0, 0);
            panelRegister.Location = new Point(this.Width, 0);
            panelForgotpassword.Location = new Point(-this.Width, 0);
        }

        private void panelForgotpassword_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
