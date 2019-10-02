using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace MCC_Neo.ViewModels
{
    public class SubItem
    {
        public SubItem(string name, UserControl screen = null)
        {
            Name = name;
            Screen = screen;
        }
        public string Name { get; private set; }
        public UserControl Screen { get; private set; }
    }
}
