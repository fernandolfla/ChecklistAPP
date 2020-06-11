using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChecklistAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalMaster : ContentPage
    {
        public ListView ListView;
        public string nometatico = AppSettings.Token.Usuario.Nome;

        public PrincipalMaster()
        {
            InitializeComponent();

            BindingContext = new PrincipalMasterViewModel();
            ListView = MenuItemsListView;
            txtNomeTatico.Text = nometatico;
        }

        class PrincipalMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<PrincipalMasterMenuItem> MenuItems { get; set; }

            public PrincipalMasterViewModel()
            {
                
                MenuItems = new ObservableCollection<PrincipalMasterMenuItem>(new[]
                {
                    new PrincipalMasterMenuItem { Id = 0, Title = "Checklist" , TargetType = typeof(Checklist)},
                    new PrincipalMasterMenuItem { Id = 1, Title = "Abastecimento" , TargetType = typeof(Abastecimento)},
                    new PrincipalMasterMenuItem { Id = 2, Title = "Manutenção" , TargetType = typeof(Manutencao)},
                    new PrincipalMasterMenuItem { Id = 3, Title = "Problemas" , TargetType = typeof(Problemas) },
                });
            }

            

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}