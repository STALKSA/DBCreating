using Microsoft.EntityFrameworkCore;
using StudentsManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentsManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext _db = new AppDbContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        ~MainWindow() { _db.Dispose(); }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
             studentsDataGrid.ItemsSource = await _db.Students.ToListAsync();
            visitsDataGrid.ItemsSource = await _db.Visits
                .Include(visit => visit.Student)
                .ToListAsync();

        }


        private async void studentsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            //await using var db = new AppDbContext();
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name  = "",
                Email = ""
            };

            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
            e.NewItem = student;
        }

        private async void studentsDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            //await using var db = new AppDbContext();
            await _db.SaveChangesAsync();
        }

        private async void visitsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            if(studentsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Выберите студента", "Внимание", MessageBoxButton.OK);
                return;
            }
            var visit = new Visit()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Student = (Student)studentsDataGrid.SelectedItem
            };

            await _db.Visits.AddAsync(visit);
            await _db.SaveChangesAsync();
            e.NewItem = visit;
        }

        private async void visitsDataGrid_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(e.Command == DataGrid.DeleteCommand)
            { 
                var selectedItem = visitsDataGrid.SelectedItem as Visit;
                 _db.Remove(selectedItem!);
                await _db.SaveChangesAsync();
                e.CanExecute = true;
            }


        }
    }
}