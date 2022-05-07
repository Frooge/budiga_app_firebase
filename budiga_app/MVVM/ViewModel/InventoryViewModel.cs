using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budiga_app.MVVM.ViewModel
{
    public class InventoryViewModel
    {
        private ItemRepository itemRepository;
        public ItemModel itemModel { get; set; }

        public InventoryViewModel()
        {
            itemRepository = new ItemRepository();
            itemModel = new ItemModel
            {
                ItemRecords = itemRepository.GetAllItems()
            };
        }
    }
}
