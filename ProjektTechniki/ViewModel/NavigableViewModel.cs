using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTechniki.ViewModel
{
    public abstract class NavigableViewModel : ViewModelBase
    {
        public abstract void OnNavigatedTo(object parameter = null);
        public abstract void OnNavigatingTo(object parameter = null);
    }
}
