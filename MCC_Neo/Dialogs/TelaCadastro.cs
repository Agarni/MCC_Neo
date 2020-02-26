using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MCC_Neo.Dialogs
{
    public class TelaCadastro : INotifyPropertyChanged
    {
        public string TituloTela { get; set; }
        public object ConteudoTela { get; set; }
        private bool _cadastroHabilitado = true;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CadastroHabilitado
        {
            get => _cadastroHabilitado;
            set
            {
                if (value == _cadastroHabilitado)
                    return;

                _cadastroHabilitado = value;
                NotifyPropertyChanged();
            }
        }

        public TelaCadastro()
        {

        }
    }
}