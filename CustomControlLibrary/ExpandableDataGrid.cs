using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControlLibrary
{
    public class ExpandableDataGrid : DataGrid
    {

        //static ExpandableDataGrid()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpandableDataGrid), new FrameworkPropertyMetadata(typeof(ExpandableDataGrid)));
        //}

        #region Private Properties
        private const string DataGridPart = "PART_DataGrid";
        private DataGrid dataGrid = null;
        #endregion

        #region DependencyProperties
        public List<ExpandableGridColumn> ColumnList
        {
            get { return (List<ExpandableGridColumn>)GetValue(ColumnListProperty); }
            set { SetValue(ColumnListProperty, value); }
        }

        public static readonly DependencyProperty ColumnListProperty =
               DependencyProperty.Register("ColumnList", typeof(List<ExpandableGridColumn>), typeof(ExpandableDataGrid));



        //public ObservableCollection<Person> ItemsSource
        //{
        //    get { return (ObservableCollection<Person>)GetValue(ItemsSourceProperty); }
        //    set { SetValue(ItemsSourceProperty, value); }
        //}

        //public static readonly DependencyProperty ItemsSourceProperty =
        //    DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<object>), typeof(ExpandableDataGrid));


        //public ObservableCollection<DataGridColumn> Columns
        //{
        //    get { return (ObservableCollection<DataGridColumn>)GetValue(ColumnsProperty); }
        //    set { SetValue(ColumnsProperty, value); }
        //}
        //public static readonly DependencyProperty ColumnsProperty =
        //    DependencyProperty.Register("Columns", typeof(ObservableCollection<DataGridColumn>), typeof(ExpandableDataGrid));


        #endregion DependencyProperties

        #region Overrided Methods
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (ColumnList?.Count > 0)
            {

            }
            dataGrid = GetTemplateChild(DataGridPart) as DataGrid;            
            if (dataGrid != null)
            {

            }

        }
        #endregion Overrided Methods
    }
}
