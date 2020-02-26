using MaterialDesignThemes.Wpf;
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

        public static System.Windows.Forms.DialogResult ExibirMensagem(string mensagem)
        {
            return BlurMessageBox.MsgBox.Show(mensagem, "MainDialogHost");
        }

        public static bool EqualsAny(this object objeto, params object[] valores)
        {
            foreach (var busca in valores)
            {
                if (objeto.Equals(busca))
                    return true;
            }

            return false;
        }
    }
}
