using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeView.wpf.Dialogs
{
    public interface IViewDialog
    {
        void ShowDialog(String Title);
        void CloseDialog();
    }
}
