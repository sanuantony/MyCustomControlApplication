using CustomControlLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomControlApplication.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>
        {
            new Person
            {
                FirstName="Sanu",
                LastName="Antony",
                MiddleName="",
                Phone="9995537668",
                Email="sanuantony@gmail.com"
            },
            new Person
            {
                FirstName="Anupama",
                LastName="Antony",
                MiddleName="",
                Phone="8125537668",
                Email="sanuantony@gmail.com"
            },
            new Person
            {
                FirstName="Test",
                LastName="Antony",
                MiddleName="Middle",
                Phone="999551235",
                Email="sanuantony@gmail.com"
            }
        };

        public List<ExpandableGridColumn> Columns { get; set; } = new List<ExpandableGridColumn>
        {
            new ExpandableGridColumn
            {
                Header="First Name",
                Order= 1,
                ColumnName="FirstName",
                Width=100,
                Visibility = true
            },
            new ExpandableGridColumn
            {
                Header="Middle Name",
                Order= 2,
                ColumnName="MiddleName",
                Width=100,
                Visibility = true
            },
            new ExpandableGridColumn
            {
                Header="Last Name",
                Order= 3,
                ColumnName="LastName",
                Width=100,
                Visibility = true
            },
            new ExpandableGridColumn
            {
                Header="Phone",
                Order= 4,
                ColumnName="Phone",
                Width=100,
                Visibility = false
            },
            new ExpandableGridColumn
            {
                Header="Email",
                Order= 5,
                ColumnName="Email",
                Width=100,
                Visibility = false
            }
        };

        public void LoadGridClick()
        {

        }
    }
}
