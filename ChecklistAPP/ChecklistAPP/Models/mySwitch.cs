using ChecklistAPP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChecklistAPP.Models
{
    public class mySwitch : Switch
    {

        public static readonly BindableProperty nameProperty =
        BindableProperty.Create("name", typeof(string), typeof(CheckItens), null);

        public string name
        {
            get { return (string)GetValue(nameProperty); }
            set { SetValue(nameProperty, value); }
        }
    }
}
