using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class InvoiceAddViewModel
    {
        private ItemRepository itemRepository;
        public ItemModel Item { get; set; }

        public InvoiceAddViewModel()
        {
            itemRepository = new ItemRepository();
            Item = new ItemModel();
            GetAll();
        }        

        public void GetAll()
        {
            Item.ItemRecords = itemRepository.GetAllItems();
        }

    }
}
