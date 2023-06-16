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
using ViewLab10.ViewModel;

namespace ViewLab10.View
{
    /// <summary>
    /// Логика взаимодействия для UserControlMain.xaml
    /// </summary>
    public partial class UserControlMain : UserControl
    {
        public UserControlMain()
        {
            this.DataContext = ViewModelContainer.MainViewModel;
            InitializeComponent();
        }

    }
}
