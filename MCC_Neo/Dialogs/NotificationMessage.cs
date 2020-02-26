using MaterialDesignThemes.Wpf;
using MCC_Neo.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace MCC_Neo.Dialogs
{
    public class NotificationMessage : INotifyPropertyChanged
    {
        private string _titulo;
        public string Titulo
        {
            get => _titulo;
            set
            {
                if (value != _titulo)
                {
                    _titulo = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _mensagem;
        public string Mensagem
        {
            get => _mensagem;
            set
            {
                if (value != _mensagem)
                {
                    _mensagem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class ErrorNotificationMessage : NotificationMessage
    {
        public ErrorNotificationMessage()
        {
            Titulo = "Erro";
        }
    }

    public class InfoNotificationMessage : NotificationMessage
    {
        public InfoNotificationMessage()
        {
            Titulo = "Informação";
        }
    }

    public class ShowDialogMessage : NotificationMessage
    {
        private ShowDialogButton _messageBoxButton = ShowDialogButton.OK;
        public ShowDialogButton MessageBoxButton
        {
            get => _messageBoxButton;
            set
            {
                if (value != _messageBoxButton)
                {
                    _messageBoxButton = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ShowDialogImage _messageBoxImage = ShowDialogImage.Error;
        public ShowDialogImage MessageBoxImage
        {
            get => _messageBoxImage;
            set
            {
                if (value != _messageBoxImage)
                {
                    _messageBoxImage = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Visibility OkVisibility => (MessageBoxButton.EqualsAny(ShowDialogButton.OK, ShowDialogButton.OKCancel) ?
            Visibility.Visible : Visibility.Collapsed);
        public Visibility CancelVisibility => (MessageBoxButton.EqualsAny(ShowDialogButton.OKCancel, ShowDialogButton.YesNoCancel) ?
            Visibility.Visible : Visibility.Collapsed);
        public Visibility YesVisibility => (MessageBoxButton.EqualsAny(ShowDialogButton.YesNo, ShowDialogButton.YesNoCancel) ?
                    Visibility.Visible : Visibility.Collapsed);
        public Visibility NoVisibility { get => YesVisibility; }
        public PackIconKind Icone
        {
            get
            {
                return MessageBoxImage switch
                {
                    ShowDialogImage.Exclamation => PackIconKind.Alert,

                    ShowDialogImage.Information | ShowDialogImage.Asterisk => PackIconKind.Information,

                    ShowDialogImage.Question => PackIconKind.QuestionMark,

                    ShowDialogImage.Error => PackIconKind.CloseOctagon,

                    _ => PackIconKind.MessageAlert,
                };
            }
        }
        public Brush CorIcone
        {
            get
            {
                return MessageBoxImage switch
                {
                    ShowDialogImage.Exclamation => Brushes.Orange,

                    ShowDialogImage.Question => Brushes.Blue,

                    ShowDialogImage.Error => Brushes.Red,

                    _ => Brushes.Black,
                };
            }
        }

        public ShowDialogMessage()
        {
            Titulo = "Informação";
        }
    }
}
