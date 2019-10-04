using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Helpers
{
    public static class HelpersMCC
    {
        public static System.Drawing.Color SysColorToDrawColor(System.Windows.Media.Color cor)
        {
            return System.Drawing.Color.FromArgb(cor.A, cor.R, cor.G, cor.B);
        }

        public static System.Windows.Media.Color DrawnColorToSysColor(System.Drawing.Color cor)
        {
            return System.Windows.Media.Color.FromArgb(cor.A, cor.R, cor.G, cor.B);
        }

        public static System.Windows.Media.Color HexaToSysColor(string corHexa)
        {
            try
            {
                var cor = DrawnColorToSysColor(System.Drawing.ColorTranslator.FromHtml(corHexa));

                return cor;
            }
            catch
            {
                return System.Windows.Media.Colors.Black;
            }
        }
    }
}
