using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;

namespace MMM.Models
{
    public class MRModel:BindableBase
    {
        private string _MRNo;
        public string MRNo
        { 
            get { return _MRNo; }
            set { SetProperty(ref _MRNo, value); }
        }

        private string _cusMRNo;
        public string CusMRNo
        {
            get { return _cusMRNo; }
            set { SetProperty(ref _cusMRNo, value); }
        }
    
    
    }
}
