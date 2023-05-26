using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8618 
namespace WebApp.Models.ViewModels
{
    public class ShoppingCardVM
    {
        public IEnumerable<ShoppingCard> ShoppingCardList { get; set; }

        public double OrderTotal { get; set; }
    }
}
