using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;


        private IUserRepository userRepository;

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        //propiedad
        public ViewModelBase CurrentChildView
        { get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //--> comandos
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }
        public ICommand ShowContabilidadViewCommand { get; }
        public ICommand ShowReportesViewCommand { get; }
        public ICommand ShowConfiguracionViewCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            //inicializar los comandos
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
            ShowContabilidadViewCommand = new ViewModelCommand(ExecuteShowContabilidadViewCommand);
            ShowReportesViewCommand = new ViewModelCommand(ExecuteShowReportesViewCommand);
            ShowConfiguracionViewCommand  = new ViewModelCommand(ExecuteShowConfiguracionViewCommand);

            //Default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Principal";
            Icon = IconChar.Home;
        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new CustomerViewModel();
            Caption = "Clientes";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowContabilidadViewCommand(object obj)
        {
            CurrentChildView = new ContabilidadViewModel();
            Caption = "Contabilidad";
            Icon = IconChar.ListNumeric;
        }

        private void ExecuteShowReportesViewCommand(object obj)
        {
            CurrentChildView = new ReportesViewModel();
            Caption = "Reportes";
            Icon = IconChar.PieChart;
        }

        private void ExecuteShowConfiguracionViewCommand(object obj)
        {
            CurrentChildView = new ConfiguracionViewModel();
            Caption = "Configuración";
            Icon = IconChar.Tools;
        }



        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"Bienvenido {user.Name} {user.LastName} ";
                CurrentUserAccount.ProfilePicture = null;               
            }
            else
            {
                CurrentUserAccount.DisplayName="Usuario invalido, no loguedo";
                //Hide child views.
            }
        }
    }
}
