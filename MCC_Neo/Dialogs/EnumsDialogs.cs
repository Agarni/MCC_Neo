using System;
using System.Collections.Generic;
using System.Text;

namespace MCC_Neo.Dialogs
{
    public enum DialogResult
    {
        Nada,
        Ok,
        Cancela,
        Sim,
        Nao,
        Botao1,
        Botao2
    }

    public enum ShowDialogButton
    {
        OK = 0,
        OKCancel = 1,
        YesNoCancel = 3,
        YesNo = 4
    }

    public enum ShowDialogImage
    {
        None = 0,
        Error = 16,
        Hand = 16,
        Stop = 16,
        Question = 32,
        Exclamation = 48,
        Warning = 48,
        Asterisk = 64,
        Information = 64
    }
}
