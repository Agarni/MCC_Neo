using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;

namespace MCC_Neo
{
    public class WindowBase : Window
    {
        private int topWorkingArea, leftWorkingArea = 0;
        private double topRestore, leftRestore = 0;
        private bool _contentLoaded;
        private readonly TopWindow topWindow = new TopWindow() { Name = "uscTopWindow" };

        public WindowBase() : base()
        {
            InitializeComponent();

            AplicarLayout();
            LimiteWorkingArea();
            DefinirBotaoMaximizar();
        }

        private void WindowBase_StateChanged(object sender, EventArgs e)
        {
            DefinirBotaoMaximizar();
        }

        private void AplicarLayout()
        {
            // Definindo visibilidade dos botões de sistema
            switch (this.WindowStyle)
            {
                case WindowStyle.ToolWindow:
                    topWindow.btnMinizar.Visibility = Visibility.Hidden;
                    topWindow.btnMaximizarRestaurar.Visibility = Visibility.Hidden;
                    break;

                case WindowStyle.None:
                    topWindow.btnMinizar.Visibility = Visibility.Hidden;
                    topWindow.btnMaximizarRestaurar.Visibility = Visibility.Hidden;
                    topWindow.btnFechar.Visibility = Visibility.Hidden;
                    break;
            }

            // Definindo eventos da Janela
            this.LocationChanged += WindowBase_LocationChanged;
            this.WindowStyle = WindowStyle.None;
            this.StateChanged += WindowBase_StateChanged;

            // Carregando conteiner principal
            Grid mainContainer = (Grid)this.Content;
            var linhasGrid = mainContainer.RowDefinitions.ToList();
            var colunas = mainContainer.ColumnDefinitions.Count;
            mainContainer.RowDefinitions.Clear();

            RowDefinition rowDefinitionTop = new RowDefinition() { Height = new GridLength(50) };
            mainContainer.RowDefinitions.Add(rowDefinitionTop);

            // Topo da janela
            topWindow.txtTituloJanela.Text = this.Title;
            topWindow.txtTituloJanela.MouseDown += Border_MouseDown;
            topWindow.brdTop.MouseDown += Border_MouseDown;
            topWindow.MouseLeftButtonDown += Border_MouseLeftButtonDown;
            topWindow.btnMinizar.Click += (s, r) => WindowState = WindowState.Minimized;
            topWindow.btnMaximizarRestaurar.Click += (s, r) => MaximizarRestaurar();
            topWindow.btnFechar.Click += (s, r) => Close();
            
            Grid.SetRow(topWindow, 0);
            if (colunas > 0)
                Grid.SetColumnSpan(topWindow, colunas);
            mainContainer.Children.Add(topWindow);

            if (linhasGrid.Count == 0)
            {
                mainContainer.RowDefinitions.Add(new RowDefinition());
            }
            else
            {
                foreach (var linha in linhasGrid)
                {
                    mainContainer.RowDefinitions.Add(linha);
                }
            }

            // Carregando componentes
            UIElementCollection element = mainContainer.Children;
            List<FrameworkElement> lstElement = element.Cast<FrameworkElement>().ToList();

            foreach (var contol in lstElement)
            {
                if (contol.Name != topWindow.Name)
                    Grid.SetRow(contol, Grid.GetRow(contol) + 1);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (topWindow.btnMaximizarRestaurar.Visibility == Visibility.Visible && e.ClickCount == 2)
            {
                MaximizarRestaurar();
            }
        }

        private void WindowBase_LocationChanged(object sender, EventArgs e)
        {
            LimiteWorkingArea();
        }

        public bool IsInDesignMode
        {
            get
            {
                return System.ComponentModel.DesignerProperties.GetIsInDesignMode(this);
            }
        }

        public void OpenResourceDictionary(string fileName)
        {
            ResourceDictionary dic = null;

            if (File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                    dic = (ResourceDictionary)XamlReader.Load(fs);

                this.Resources.MergedDictionaries.Add(dic);
            }
            else
            {
                throw new FileNotFoundException("Can't open resource file: " + fileName +
                  " in the method OpenResourceDictionary().");
            }
        }

        private void LimiteWorkingArea()
        {
            var interopWindow = new WindowInteropHelper(GetWindow(this));
            var hostingScreen = System.Windows.Forms.Screen.FromHandle(interopWindow.Handle);

            this.MaxHeight = hostingScreen.WorkingArea.Height + 14;
            this.MaxWidth = hostingScreen.WorkingArea.Width + 14;
            topWorkingArea = hostingScreen.WorkingArea.Top + 5;
            leftWorkingArea = hostingScreen.WorkingArea.Left + 5;

            topRestore = Top;
            leftRestore = Left;
        }

        private void MaximizarRestaurar()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.Top = topRestore;
                this.Left = leftRestore;
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                this.Top = topWorkingArea;
                this.Left = leftWorkingArea;
            }

            DefinirBotaoMaximizar();
        }

        private void DefinirBotaoMaximizar()
        {
            topWindow.btnMaximizarRestaurar.ToolTip = (WindowState == WindowState.Normal ? "Maximizar" : "Restaurar");
            topWindow.pciMaximizeRestore.Kind = (WindowState == WindowState.Normal ?
                MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize : MaterialDesignThemes.Wpf.PackIconKind.WindowRestore);
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;

            System.Uri resourceLocater = new System.Uri($"/MCC_neo;component/{this.GetType().Name}.xaml", System.UriKind.Relative);

            System.Windows.Application.LoadComponent(this, resourceLocater);
        }
    }
}
