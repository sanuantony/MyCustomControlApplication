﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CustomControlLibrary
{
    public class ExpandableDataGrid : DataGrid
    {
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
        private void SetVisibility(double newSize)
        {
            double totalWidth = ColumnList.Where(x => x.Visibility.Equals(true)).Sum(y => y.Width);
            int indexofNextVisibileColumn = ColumnList.IndexOf(ColumnList.FirstOrDefault(x => x.Visibility.Equals(false)));
            if (indexofNextVisibileColumn > 1)
                totalWidth += ColumnList[indexofNextVisibileColumn].Width;
            if (totalWidth < newSize)
            {
                if (indexofNextVisibileColumn > 0)
                {
                    ColumnList[indexofNextVisibileColumn].Visibility = true;
                    for (int i = 0; i < dataGrid.Columns.Count; i++)
                    {
                        foreach (var item in ColumnList)
                        {
                            if (item.ColumnName == ((Binding)(dataGrid.Columns[i].ClipboardContentBinding)).Path.Path)
                            {
                                if (item.IsAlwaysVisible)
                                {
                                    dataGrid.Columns[i].Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    dataGrid.Columns[i].Visibility = item.Visibility ? Visibility.Visible : Visibility.Hidden;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (indexofNextVisibileColumn == -1)
                {
                    ColumnList[ColumnList.Count - 1].Visibility = false;
                    indexofNextVisibileColumn = ColumnList.Count - 1;
                }
                else if (indexofNextVisibileColumn > 1)
                    ColumnList[indexofNextVisibileColumn - 1].Visibility = false;
                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    foreach (var item in ColumnList)
                    {
                        if (item.ColumnName == ((Binding)(dataGrid.Columns[i].ClipboardContentBinding))?.Path?.Path)
                        {
                            dataGrid.Columns[i].Visibility = item.Visibility ? Visibility.Visible : Visibility.Hidden;
                        }
                    }
                }
            }
        }

        #endregion Private Methods
    }
}
