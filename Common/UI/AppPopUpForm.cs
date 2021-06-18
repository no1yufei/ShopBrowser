﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.UI
{
    public partial class AppPopUpForm : Form
    {
        public AppPopUpForm(Control userForm)
        {
            InitializeComponent();
            userForm.Dock = DockStyle.Fill;
            Controls.Add(userForm);
            
        }
    }
}
