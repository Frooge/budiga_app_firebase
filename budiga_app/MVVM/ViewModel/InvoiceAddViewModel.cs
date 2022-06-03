using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace budiga_app.MVVM.ViewModel
//{
//    public class InvoiceAddViewModel
//    {
//        //private ItemRepository itemRepository;
//        //private ItemModel _item;
//        //public ItemModel Item { get; set; }
//        //public RelayCommand SearchItemCommand { get; set; }

//        //public InvoiceAddViewModel()
//        //{
//        //    itemRepository = new ItemRepository();
//        //    _item = new ItemModel();
//        //    Item = new ItemModel();
//        //    SearchItemCommand = new RelayCommand(param => SearchItem((string)param));
//        //    GetAll();
//        //}        

//        //public void GetAll()
//        //{
//        //    //_item.ItemRecords = itemRepository.GetAllItems();
//        //    Item.ItemRecords = _item.ItemRecords;
//        //}

//        //private void SearchItem(string searchTxt = "")
//        //{
//        //    Item.ItemRecords = new ObservableCollection<ItemModel>(
//        //        _item.ItemRecords.Where(i => i.Name.ToLower().Contains(searchTxt.ToLower())
//        //        || i.Brand.ToLower().Contains(searchTxt.ToLower())
//        //        || i.Barcode.ToLower().Contains(searchTxt.ToLower())).ToList());
//        //}

//    }
//}
