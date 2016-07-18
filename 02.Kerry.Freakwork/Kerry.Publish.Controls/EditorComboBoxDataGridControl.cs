using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Kerry.Publish.Controls
{
    [DefaultProperty("Columns")]
    [ContentProperty("Columns")]
    public class EditorComboBoxDataGridControl : ComboBox
    {
        private const string partPopupDataGrid = "PART_PopupDataGrid";
        private ObservableCollection<DataGridTextColumn> columns;
        private DataGrid popupDataGrid;

        static EditorComboBoxDataGridControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditorComboBoxDataGridControl), new FrameworkPropertyMetadata(typeof(EditorComboBoxDataGridControl)));
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<DataGridTextColumn> Columns
        {
            get
            {
                if (this.columns == null)
                {
                    this.columns = new ObservableCollection<DataGridTextColumn>();
                }
                return this.columns;
            }
        }

        public override void OnApplyTemplate()
        {
            if (popupDataGrid == null)
            {
                popupDataGrid = this.Template.FindName(partPopupDataGrid, this) as DataGrid;
                if (popupDataGrid != null && columns != null)
                {
                    for (int i = 0; i < columns.Count; i++)
                    {
                        popupDataGrid.Columns.Add(columns[i]);
                    }

                    popupDataGrid.MouseDown += new MouseButtonEventHandler(popupDataGrid_MouseDown);
                    popupDataGrid.SelectionChanged += new SelectionChangedEventHandler(popupDataGrid_SelectionChanged);

                }
            }

            base.OnApplyTemplate();
        }

        void popupDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null)
                {
                    this.SelectedItem = dg.SelectedItem;
                    this.SelectedValue = dg.SelectedValue;
                    this.SelectedIndex = dg.SelectedIndex;
                    this.SelectedValuePath = dg.SelectedValuePath;

                }
            }
        }


        void popupDataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (dg != null)
            {
                DependencyObject dep = (DependencyObject)e.OriginalSource;

                while ((dep != null) &&!(dep is DataGridCell) && !(dep is DataGridColumnHeader))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                    return;

                if (dep is DataGridColumnHeader)
                {
                    // do something
                }

                if (dep is DataGridCell)
                {
                    this.IsDropDownOpen = false;
                }
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (popupDataGrid == null)
                return;

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                if (IsDropDownOpen)
                {
                    popupDataGrid.SelectedItem = this.SelectedItem;
                    if (popupDataGrid.SelectedItem != null)
                        popupDataGrid.ScrollIntoView(popupDataGrid.SelectedItem);
                }
            }
        }
        protected override void OnDropDownOpened(EventArgs e)
        {
            popupDataGrid.SelectedItem = this.SelectedItem;

            base.OnDropDownOpened(e);

            if (popupDataGrid.SelectedItem != null)
                popupDataGrid.ScrollIntoView(popupDataGrid.SelectedItem);
        }

    }
}
