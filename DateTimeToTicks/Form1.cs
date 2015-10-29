using System;
using System.Globalization;
using System.Windows.Forms;

namespace DateTimeToTicks
{
    public partial class Form1 : Form
    {
        private readonly string[] _formats = new[] { "dd.MM.yyyy HH:mm:ss", "d.M.yyyy HH:mm:ss", "dd.MM.yyyy", "d.M.yyyy" };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button2.Enabled = false;

                var dateTimeText = txtInput.Text;

                DateTime dateTime = DateTime.MinValue;
                var wasParsed = false;

                foreach (var format in _formats)
                {  
                    wasParsed = DateTime.TryParseExact(dateTimeText, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                    if (wasParsed) break;
                }

                if (wasParsed && dateTime != DateTime.MinValue)
                {
                    txtHelper.Text =
                        string.Format("Day: {0} ({6})\nMonth: {1}\nYear: {2}\nHour: {3}\nMin: {4}\nSec: {5}",
                            dateTime.Day, dateTime.Month, dateTime.Year, dateTime.Hour, dateTime.Minute, dateTime.Second,
                            dateTime.DayOfWeek);

                    txtResult.Text = dateTime.Ticks.ToString();

                    button2.Enabled = true;
                }
                else
                {
                    throw new InvalidOperationException("Could not parse input date and time with the given format");
                }    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button2.Text = "Copy ticks";
            }
        }

        private void txtHelper_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtResult.Text);
            button2.Text = "Copied";
        }
    }
}
