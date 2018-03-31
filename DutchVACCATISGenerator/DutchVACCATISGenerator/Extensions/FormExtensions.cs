using System.Drawing;
using System.Windows.Forms;

namespace DutchVACCATISGenerator.Extensions
{
    public static class FormExtensions
    {
        public static void SetRelativeBottom(this Form form, Rectangle bounds)
        {
            form.Left = bounds.Left;
            form.Top = bounds.Bottom;
            form.Refresh();
        }

        public static void SetRelativeRight(this Form form, Rectangle bounds)
        {
            form.Left = bounds.Right;
            form.Top = bounds.Top;
            form.Refresh();
        }
    }
}
