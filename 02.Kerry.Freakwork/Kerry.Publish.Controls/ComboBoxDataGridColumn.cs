using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Kerry.Publish.Controls
{
    [DefaultProperty("Columns")]
    [ContentProperty("Columns")]
    public class ComboBoxDataGridColumn : DataGridComboBoxColumn
    {
        private ObservableCollection<DataGridTextColumn> columns;
        private  EditorComboBoxDataGridControl comboBox ;
       
        public ComboBoxDataGridColumn()
        {
            comboBox = new EditorComboBoxDataGridControl();
        }
        
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ComboBoxDataGridColumn.ItemsSourceProperty)
            {
                comboBox.ItemsSource = ItemsSource;
            }
            else if (e.Property == ComboBoxDataGridColumn.SelectedValuePathProperty)
            {
                
                comboBox.SelectedValuePath = SelectedValuePath;
                

            }
            else if (e.Property == ComboBoxDataGridColumn.DisplayMemberPathProperty)
            {
                comboBox.DisplayMemberPath = DisplayMemberPath;
            }
            
            base.OnPropertyChanged(e);
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

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
           if(comboBox.Columns.Count==0)
           {
                for (int i = 0; i < columns.Count; i++)
                    comboBox.Columns.Add(columns[i]);
            }
            return comboBox;
        }
        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            DataGridCell cell=editingEventArgs.Source as DataGridCell;
           if (cell != null)
           {
               object obj = ((DataRowView)editingElement.DataContext).Row[this.SelectedValuePath];
               comboBox.SelectedValue= obj;
           }

            return comboBox.SelectedItem;
        }
        protected override bool CommitCellEdit(FrameworkElement editingElement)
        {
            ((DataRowView)editingElement.DataContext).Row[this.SelectedValuePath] = comboBox.SelectedValue;
            return true;
        }
    }
}
