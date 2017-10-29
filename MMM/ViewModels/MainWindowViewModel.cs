using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;
using MMM.Models;

namespace MMM.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        public MainWindowViewModel()
        {
            MRs.Add(new MRModel() { KHIMRNo = "A11111", CusMRNo = "QN798797" });
            MRs.Add(new MRModel() { KHIMRNo = "A11112", CusMRNo = "QN798707" });
            MRs.Add(new MRModel() { KHIMRNo = "A11113", CusMRNo = "QN798717" });
        }
        private ObservableCollection<MRModel> _mrs = new ObservableCollection<MRModel>();
        public ObservableCollection<MRModel> MRs
        {
            get { return _mrs; }
            set { SetProperty(ref _mrs, value); }
        }
    }
}
