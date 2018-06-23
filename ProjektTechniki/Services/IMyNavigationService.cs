 using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjektTechniki.Services
{
    /// <summary>
    /// Interfejs publiczny pomocny przy nawigowaniu między okienkami i strona w aplikacji
    /// </summary>
    public interface IMyNavigationService:INavigationService
    {
        object Parameter { get; }
    }
}
