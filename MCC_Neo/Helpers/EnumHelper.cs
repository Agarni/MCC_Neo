using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MCC_Neo.Helpers
{
    public class EnumHelper : DependencyObject
    {
        public static Type GetEnum(DependencyObject obj)
        {
            return (Type)obj.GetValue(EnumProperty);
        }

        public static void SetEnum(DependencyObject obj, string value)
        {
            obj.SetValue(EnumProperty, value);
        }

        public static void RefreshComboBoxSelection(object sender, object selectedItem)
        {
            if (selectedItem != null)
            {
                var control = sender as FrameworkElement;
                var controlChilds = FindFirstChild<TextBlock>(control);
                if (controlChilds != null)
                {
                    controlChilds.Text = ((DisplayAttribute)selectedItem.GetType().GetMember(selectedItem.ToString())
                        .First().GetCustomAttributes(false).Where(x => x.GetType() == typeof(DisplayAttribute)).FirstOrDefault()).Name;
                }
            }
        }

        public static T FindFirstChild<T>(DependencyObject parent) where T : DependencyObject
        {
            // Confirm parent and childName are valid.
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindFirstChild<T>(child);
                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild != null) break;
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }

        // Using a DependencyProperty as the backing store for Enum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnumProperty =
            DependencyProperty.RegisterAttached("Enum", typeof(Type), typeof(EnumHelper), new PropertyMetadata(null, OnEnumChanged));

        private static void OnEnumChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

            if (sender is ItemsControl control)
            {
                if (e.NewValue != null)
                {
                    var _enum = Enum.GetValues(e.NewValue as Type);
                    control.ItemsSource = _enum;
                }
            }
        }

        public static bool GetMoreDetails(DependencyObject obj)
        {
            return (bool)obj.GetValue(MoreDetailsProperty);
        }

        public static void SetMoreDetails(DependencyObject obj, bool value)
        {
            obj.SetValue(MoreDetailsProperty, value);
        }

        // Using a DependencyProperty as the backing store for MoreDetails.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MoreDetailsProperty =
            DependencyProperty.RegisterAttached("MoreDetails", typeof(bool), typeof(EnumHelper), new PropertyMetadata(false, OnMoreDetailsChanged));

        private static void OnMoreDetailsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is FrameworkElement control)
            {
                var enumobject = control.DataContext;
                var fieldInfo = enumobject.GetType().GetField(enumobject.ToString());

                var array = fieldInfo.GetCustomAttributes(false);

                if (array.Length == 0)
                {
                    if (control is TextBlock)
                    {
                        ((TextBlock)control).Text = enumobject.ToString();
                    }
                    else if (control is ContentControl)
                    {
                        ((ContentControl)control).Content = enumobject;
                    }
                    return;
                }

                foreach (var o in array)
                {
                    if (o is DescriptionAttribute)
                    {
                        control.ToolTip = ((DescriptionAttribute)o).Description;
                    }
                    else if (o is DisplayAttribute)
                    {
                        if (control is TextBlock)
                        {
                            ((TextBlock)control).Text = ((DisplayAttribute)o).Name;
                        }
                        else if (control is ContentControl)
                        {
                            ((ContentControl)control).Content = ((DisplayAttribute)o).Name;
                        }
                    }
                }
            }
        }
    }
}
