using System;
using System.Text;
using System.Windows.Forms;

namespace vigeneretest
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        static void VigenereEncrypt(ref StringBuilder s, string key)
        {
            for (int i = 0; i < s.Length; i++) s[i] = Char.ToUpper(s[i]);
            key = key.ToUpper();
            int j = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (Char.IsLetter(s[i]))
                {
                    s[i] = (char)(s[i] + key[j] - 'A');
                    if (s[i] > 'Z') s[i] = (char)(s[i] - 'Z' + 'A' - 1);
                }
                j = j + 1 == key.Length ? 0 : j + 1;
            }
        }

        static void VigenereDecrypt(ref StringBuilder s, string key)
        {
            for (int i = 0; i < s.Length; i++) s[i] = Char.ToUpper(s[i]);
            key = key.ToUpper();
            int j = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (Char.IsLetter(s[i]))
                {
                    s[i] = s[i] >= key[j] ?
                              (char)(s[i] - key[j] + 'A') :
                              (char)('A' + ('Z' - key[j] + s[i] - 'A') + 1);
                }
                j = j + 1 == key.Length ? 0 : j + 1;
            }
        }
        private string Replace(string str)
        {
            string[] coming = { "ı", "İ", "ö", "Ö", "ş", "Ş", "ğ", "Ğ", "ü", "Ü", "ç", "Ç" };
            string[] newstr = { "i", "I", "o", "O", "s", "S", "g", "G", "u", "U", "c", "C" };
            for (int i = 0; i < coming.Length; i++)
            {

                str = str.Replace(coming[i], newstr[i]);

            }
            return str;
        }
        private void Encode()
        {
            try
            {
                if (String.IsNullOrEmpty(txtCipherKey.Text))
                {
                    MessageBox.Show("Cipher Key area is not to be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCipherKey.Focus();
                }
                else
                {
                    StringBuilder s = new StringBuilder(Replace(rtxtEnter.Text));
                    VigenereEncrypt(ref s, txtCipherKey.Text);
                    rtxtVigenere.Text = s.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Decode()
        {
            try
            {
                if (String.IsNullOrEmpty(txtCipherKey.Text))
                {
                    MessageBox.Show("Cipher Key area is not to be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCipherKey.Focus();
                }
                else {
                    StringBuilder s = new StringBuilder(rtxtEnter.Text);
                    VigenereDecrypt(ref s, txtCipherKey.Text);
                    rtxtVigenere.Text = s.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            Encode();
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            Decode();
        }

        private void txtCipherKey_Click(object sender, EventArgs e)
        {
            txtCipherKey.Clear();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.Shift && e.KeyCode == Keys.E)
            {
                Encode();
            }
            else if(e.Control && e.Shift && e.KeyCode == Keys.D)
            {
                Decode();
            }
        }
    }
}
