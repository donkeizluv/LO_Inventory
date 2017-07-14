using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LO_Inventory.Forms
{
    public partial class LoginForm : Form
    {
        //private Regex ConnectionServerNameRex = new Regex(@"\b(data source=)([^;]+)", RegexOptions.Compiled);
        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        public static extern bool LogonUser(string userName, string domainName, string password, int LogonType, int LogonProvider, ref IntPtr phToken);

        public bool IsValidateCredentials(string userName, string password, string domain)
        {
            IntPtr tokenHandler = IntPtr.Zero;
            bool isValid = LogonUser(userName, domain, password, 3, 0, ref tokenHandler);
            return isValid;
        }

        private const string DomainName = "sgvf.sgcf";
        public bool Authorized { get; private set; } = false;
        public AccountRole Role { get; private set; } = AccountRole.NotAuthorized;
        public string Username { get; private set; } = string.Empty;

        //0 must be error bc default(int)
        public enum AccountRole
        {
            NotAuthorized = -1, //user is not allowed
            Error = 0, //db error
            Admin = 1, //full control
            UserInput = 2, //mainly for input purposes
            UserRead = 3 //read only
        }

        public LoginForm()
        {
            InitializeComponent();
            SetVer();
            SetServer();
            var test = new InventoryDbEntities();
        }
        private void SetVer()
        {
            labelVer.Text = $"Ver {Program.GetVer}";
        }
        private void SetServer()
        {
            var server = string.Empty;
            try
            {
                using (var context = new InventoryDbEntities())
                {
                    server = context.Database.Connection.DataSource.Split('\\').First();
                }
            }
            catch (Exception)
            {
                server = "Unknown";
            }

            labelDataSourse.Text = $"Server: {server}";
        }
           
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            DoLogin();
            if(Authorized)
            {
                Close();
            }
        }

        public void DoLogin()
        {
            var username = textBoxUsername.Text;
            if (string.IsNullOrEmpty(textBoxPwd.Text) || string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Invalid log in infomation");
                return;
            }
            try
            {
#if (DEBUG != true)
                //unconment this in offical release!!!!!!!!!!!!!!
                //if (string.Compare(DomainName, AdHelper.GetDomainName(), true) != 0) throw new UnauthorizedAccessException("Unauthorized enviroment.");
#endif
                Role = Login(username, textBoxPwd.Text);
                if (Role == AccountRole.Error)
                {
                    Authorized = false;
                    return;
                }
                if (Role == AccountRole.NotAuthorized)
                {
                    Authorized = false;
                    throw new UnauthorizedAccessException("User is not permitted to use program.");
                }
                Username = username;
                Authorized = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Login Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Authorized = false;
                Role = AccountRole.NotAuthorized;
            }
        }

        /// <summary>
        /// returns Roles type in Roles table
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <AccountRole></returns>
        private AccountRole Login(string userName, string pwd)
        {
            if (!LoginDomain(userName, pwd)) throw new UnauthorizedAccessException("Username or Pwd is incorrect.");
            return HelperMethods.ExecuteDbRequest(() => GetLoginLevel(userName));

            //using (var db = new InventoryDbEntities())
            //{
            //    return db.GetLoginLevel(userName).First() ?? -1;
            //    //if (loginLevel.First() == -1) throw new UnauthorizedAccessException("User is not authorized to use program.");
            //}
        }

        private AccountRole GetLoginLevel(string userName)
        {
            using (var db = new InventoryDbEntities())
            {
                return (AccountRole)(db.GetLoginLevel(userName).First() ?? -1);
            }
        }

        private bool LoginDomain(string userName, string pwd)
        {
            return IsValidateCredentials(userName, pwd, DomainName);
        }

        private void TextBoxPwd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ButtonLogin_Click(this, EventArgs.Empty);
            }
        }

        private void TextBoxUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ButtonLogin_Click(this, EventArgs.Empty);
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(DialogResult != DialogResult.OK)
                DialogResult = DialogResult.Cancel;
        }
    }
}