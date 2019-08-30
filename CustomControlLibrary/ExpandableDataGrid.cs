using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CustomControlLibrary
{
    [TemplatePart(Name = "ColumnList", Type = typeof(List<ExpandableGridColumn>))]
    [TemplatePart(Name = "GridName", Type = typeof(string))]
    public class ExpandableDataGrid : DataGrid
    {
        #region Private Properties
        private const string DataGridPart = "PART_DataGrid";
        private DataGrid dataGrid = null;
        #endregion

        #region DependencyProperties

        public string GridName
        {
            get { return (string)GetValue(GridNameProperty); }
            set { SetValue(GridNameProperty, value); }
        }

        public static readonly DependencyProperty GridNameProperty =
            DependencyProperty.Register("GridName", typeof(string), typeof(ExpandableDataGrid), new PropertyMetadata(""));

        public List<ExpandableGridColumn> ColumnList
        {
            get { return (List<ExpandableGridColumn>)GetValue(ColumnListProperty); }
            set { SetValue(ColumnListProperty, value); }
        }

        public static readonly DependencyProperty ColumnListProperty =
               DependencyProperty.Register("ColumnList", typeof(List<ExpandableGridColumn>), typeof(ExpandableDataGrid));

        #endregion DependencyProperties

        #region Overrided Methods        
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            dataGrid = (DataGrid)FindName(DataGridPart);
            if (ColumnList != null && dataGrid != null)
            {
                SetVisibility(sizeInfo.NewSize.Width);
            }
        }
        #endregion Overrided Methods

        #region Private Methods        

        /// <summary>
        /// Setting the visibility based on width of datagrid while rendering.
        /// </summary>
        /// <param name="newSize"></param>
        private void SetVisibility(double newSize)
        {
            for (int i = 0; i < ColumnList.Count; i++)
                if (ColumnList[i].IsAlwaysVisible == true)
                    ColumnList[i].Visibility = true;
            double totalWidthOfAllColumns = ColumnList.Sum(x => x.Width);
            double widthOfVisibleColumns = ColumnList.Where(y => y.Visibility.Equals(true)).Sum(x => x.Width);
            int indexToShow = FindIndexNextVisibleColumn();
            int indexToHide = FindIndexNextColumnToHide();
            if (totalWidthOfAllColumns <= newSize)
            {
                for (int i = 0; i < dataGrid.Columns.Count; i++)
                    dataGrid.Columns[i].Visibility = Visibility.Visible;
            }
            else if (widthOfVisibleColumns <= newSize)
            {

                while ((widthOfVisibleColumns + ColumnList[indexToShow].Width) <= newSize && indexToShow != -1)
                {
                    if (indexToShow != -1)
                    {
                        ColumnList[indexToShow].Visibility = true;
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            var item = ColumnList[indexToShow];
                            if (item.ColumnName == ((Binding)(dataGrid.Columns[i].ClipboardContentBinding))?.Path?.Path)
                            {
                                if (!item.IsAlwaysVisible)
                                    dataGrid.Columns[i].Visibility = item.Visibility ? Visibility.Visible : Visibility.Hidden;
                            }
                        }
                    }
                    widthOfVisibleColumns = ColumnList.Where(y => y.Visibility.Equals(true)).Sum(x => x.Width);
                    indexToShow = FindIndexNextVisibleColumn();
                }
            }
            else
            {
                while (widthOfVisibleColumns > newSize && indexToHide != -1)
                {
                    if (indexToHide != -1)
                    {
                        ColumnList[indexToHide].Visibility = false;
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            var item = ColumnList[indexToHide];
                            if (item.ColumnName == ((Binding)(dataGrid.Columns[i].ClipboardContentBinding))?.Path?.Path)
                            {
                                if (!item.IsAlwaysVisible)
                                    dataGrid.Columns[i].Visibility = item.Visibility ? Visibility.Visible : Visibility.Hidden;
                            }
                        }
                    }
                    widthOfVisibleColumns = ColumnList.Where(y => y.Visibility.Equals(true)).Sum(x => x.Width);
                    indexToHide = FindIndexNextColumnToHide();
                }
            }
        }


        /// <summary>
        /// Returns index of next visible
        /// if -1 is returned all columns are already visible
        /// </summary>
        /// <returns>int</returns>
        private int FindIndexNextVisibleColumn()
        {

            var newCList = ColumnList?.Where(x => x.IsAlwaysVisible.Equals(false))?.OrderBy(y => y.Order).ToList();
            return ColumnList.IndexOf(newCList.FirstOrDefault(x => x.Visibility.Equals(false)));
        }

        /// <summary>
        /// Returns index of next column to be hidden
        /// if -1 is returned all columns are already hidden except always visible columns
        /// </summary>
        /// <returns>int</returns>
        private int FindIndexNextColumnToHide()
        {
            var newCList = ColumnList?.Where(x => x.IsAlwaysVisible.Equals(false))?.OrderBy(y => y.Order).ToList();
            return ColumnList.IndexOf(newCList.LastOrDefault(x => x.Visibility.Equals(true)));
        }
        #endregion Private Methods
    }
}
