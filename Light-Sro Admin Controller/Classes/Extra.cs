using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Light_Sro_Admin_Controller.Classes
{
    public sealed class Extra
    {


        public void CopySelectedValuesToClipboard(ListView listView)
        {
            var builder = new StringBuilder();
            foreach (ListViewItem item in listView.SelectedItems)
                builder.AppendLine($"{item.SubItems[1].Text}\t{item.SubItems[2].Text}\t{item.SubItems[3].Text}");
            Clipboard.SetText(builder.ToString(), TextDataFormat.Text);


           // Clipboard.SetText(builder.ToString());
        }

    }
}
