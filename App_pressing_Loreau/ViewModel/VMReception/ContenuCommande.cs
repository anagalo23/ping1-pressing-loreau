using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_pressing_Loreau.Helper;

namespace App_pressing_Loreau.View
{
    class ContenuCommande: ObservableObject
    {

        private string productName;

        public string ProductName
        {
            get
            {
                return this.productName;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.productName = value;
                    OnPropertyChanged("ProductName");
                }
            }
        }


    }
}
