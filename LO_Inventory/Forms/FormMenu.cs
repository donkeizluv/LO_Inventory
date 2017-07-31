using LO_Inventory.Forms.Viewers.ConcreteViewers;
using LO_Inventory.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using static LO_Inventory.Forms.LoginForm;

namespace LO_Inventory.Forms
{
    public partial class FormMenu : Form
    {
        private AccountRole _role;

        public AccountRole Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
                IsAdmin = _role == AccountRole.Admin;
            }
        }

        private bool _isAdmin = false;

        public bool IsAdmin
        {
            get
            {
                return _isAdmin;
            }
            set
            {
                _isAdmin = value;
                EnableAdminControls(_isAdmin);
            }
        }

        public string Username { get; private set; }
        public int UserId { get; private set; }
        private List<IViewer> _formList = new List<IViewer>();
        public ActionLogger ActionLogger { get; private set; } = new ActionLogger();

        private void EnableAdminControls(bool isAdmin)
        {
            buttonPer.Enabled = isAdmin;
            //buttonUser.Enabled = isAdmin;
        }

        public FormMenu(string userName, AccountRole role)
        {
            InitializeComponent();
            Role = role;
            if (Role == AccountRole.Error || Role == AccountRole.NotAuthorized) throw new InvalidProgramException();
            Username = userName;
            UserId = GetUserId(Username);
            if (UserId == -1)
            {
                MessageBox.Show(this, "Cannot connect to db.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;

            }
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            SetVer();
            SetStatus(Role, Username);
        }

        private int GetUserId(string username)
        {
            using (var context = new InventoryDbEntities())
            {
                if (!IdTranslater.GetUserId(username, out var id, context))
                {
                    return -1;
                }
                return id ?? -1;
            }
        }

        private void SetStatus(AccountRole role, string userName)
        {
            labelRole.Text = role.ToString();
            labelUsername.Text = userName;
        }

        private void ExitToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetVer()
        {
            labelVer.Text = $"Ver {Program.GetVer}";
        }

        private void ButtonCabinet_Click(object sender, EventArgs e)
        {
            ShowForm<CabinetViewer>("Cabinets");
        }

        private void ButtonCabinetType_Click(object sender, EventArgs e)
        {
            ShowForm<CabinetTypeViewer>("Cabinet Types");
        }

        private void ButtonItem_Click(object sender, EventArgs e)
        {
            ShowForm<ItemViewer>("Items");
        }
        private void ButtonItemCat_Click(object sender, EventArgs e)
        {
            ShowForm<ItemCategoryViewer>("Item Category");
        }
        private void ButtonTran_Click(object sender, EventArgs e)
        {
            ShowForm<TransactionViewer>("Transactions");
        }

        private void ButtonPer_Click(object sender, EventArgs e)
        {
            if (!IsAdmin)
            {
                MessageBox.Show("Unauthorized");
                Environment.FailFast(string.Empty);
            }
            ShowForm<PermissionViewer>("Permissions");
        }

        private void ButtonUser_Click(object sender, EventArgs e)
        {
            ShowForm<UserViewer>("User");
        }

        private void ButtonOrder_Click(object sender, EventArgs e)
        {
            ShowForm<OrderViewer>("Orders");
        }

        private void ButtonProvider_Click(object sender, EventArgs e)
        {
            ShowForm<ProviderViewer>("Providers");
        }

        private void ShowForm<T>(string name) where T : IViewer, new()
        {
            var newForm = new T();
            newForm.Controller.UserId = UserId;
            newForm.Controller.Username = Username;
            newForm.Text = name;
            newForm.ActionLogger = ActionLogger;
            foreach (var form in _formList)
            {
                if (form is T)
                {
                    form.Show();
                    form.Focus();
                    newForm.Dispose();
                    return;
                }
            }
            _formList.Add(newForm);
            newForm.Show();
            newForm.Focus();
            newForm.RefeshMainGrid();
        }

        private void ShowLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActionLogger.Show();
        }
    }
}