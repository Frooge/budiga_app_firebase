using budiga_app.Core;
using budiga_app.DataAccess;
using budiga_app.MVVM.Model;
using budiga_app.MVVM.View;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace budiga_app.MVVM.ViewModel
{
    public class InventoryViewModel : ObservableObject
    {
        private static InventoryViewModel _instance;
        private ItemRepository _itemRepository;
        private ItemHistoryRepository _itemHistoryRepository;
        private ItemModel _item;
        public ItemModel Item { get; set; }
        public ItemHistoryModel ItemHistory { get; set; }
        
        public Action CloseHistoryAction { get; set; }
        public RelayCommand AddItemModalCommand { get; set; }
        public RelayCommand EditItemModalCommand { get; set; }
        public RelayCommand SearchItemCommand { get; set; }
        public RelayCommand ItemHistoryModalCommand { get; set; }
        public RelayCommand UndoActionCommand { get; set; }

        public static InventoryViewModel GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InventoryViewModel();
                }
                return _instance;
            }
        }

        public InventoryViewModel()
        {            
            Initialize();
            GetAllItem();
            GetAllHistory();
        }

        private void Initialize()
        {
            
            _item = new ItemModel();            
            Item = new ItemModel();
            ItemHistory = new ItemHistoryModel();
            _itemRepository = new ItemRepository();
            _itemHistoryRepository = new ItemHistoryRepository();
            AddItemModalCommand = new RelayCommand(param => AddItemModal());
            EditItemModalCommand = new RelayCommand(param => EditItemModal((ItemModel)param));
            SearchItemCommand = new RelayCommand(param => SearchItem((string)param));
            ItemHistoryModalCommand = new RelayCommand(param => ItemHistoryModal());
            UndoActionCommand = new RelayCommand(param => UndoAction((ItemHistoryModel)param));            
        }

        public void GetAllItem()
        {
            try
            {
                DataClass dataClass = DataClass.GetInstance;
                FirestoreConn conn = FirestoreConn.GetInstance;
                Query query;

                if (dataClass.LoggedInUser.Type.Equals("admin"))
                    query = conn.FirestoreDb.Collection("items").WhereEqualTo("IsDeleted", false);
                else
                    query = conn.FirestoreDb.Collection("items").WhereEqualTo("IsDeleted", false).WhereEqualTo("StoreId", dataClass.Store.Id);

                FirestoreChangeListener listener = query.Listen(snapshot =>
                {
                    _item.ItemRecords = new ObservableCollection<ItemModel>();
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        Dictionary<string, object> dict = documentSnapshot.ToDictionary();
                        App.Current.Dispatcher.Invoke((System.Action)delegate
                        {
                            _item.ItemRecords.Add(new ItemModel()
                            {
                                Id = dict["Id"].ToString(),
                                StoreId = dict["StoreId"].ToString(),
                                Barcode = dict["Barcode"].ToString(),
                                Name = dict["Name"].ToString(),
                                Brand = dict["Brand"].ToString(),
                                Price = Convert.ToDecimal(dict["Price"]),
                                Quantity = Convert.ToInt32(dict["Quantity"])
                            });
                        });
                    }
                    Item.ItemRecords = _item.ItemRecords;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        public void GetAllHistory()
        {
            try
            {
                DataClass dataClass = DataClass.GetInstance;
                FirestoreConn conn = FirestoreConn.GetInstance;
                Query query;

                if (dataClass.LoggedInUser.Type.Equals("admin"))
                    query = conn.FirestoreDb.Collection("item_history").Limit(50);
                else
                    query = conn.FirestoreDb.Collection("item_history").WhereEqualTo("StoreId", dataClass.Store.Id).Limit(50);

                FirestoreChangeListener listener = query.Listen(snapshot =>
                {
                    ItemHistory.ItemHistoryRecords = new ObservableCollection<ItemHistoryModel>();
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        Dictionary<string, object> dict = documentSnapshot.ToDictionary();
                        App.Current.Dispatcher.Invoke((System.Action)delegate
                        {
                            ItemHistory.ItemHistoryRecords.Add(new ItemHistoryModel()
                            {
                                Id = dict["Id"].ToString(),
                                ItemId = dict["ItemId"].ToString(),
                                StoreId = dict["StoreId"].ToString(),
                                UserFullName = dict["UserFullName"].ToString(),
                                Barcode = dict["Barcode"].ToString(),
                                Name = dict["Name"].ToString(),
                                Brand = dict["Brand"].ToString(),
                                Price = Convert.ToDecimal(dict["Price"]),
                                Quantity = Convert.ToInt32(dict["Quantity"]),
                                Action = dict["Action"].ToString(),
                                CommittedDate = ((Timestamp)dict["CommittedDate"]).ToDateTime()
                            });
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchItem(string searchTxt = "")
        {
            Item.ItemRecords = new ObservableCollection<ItemModel>(
                _item.ItemRecords.Where(i => i.Name.ToLower().Contains(searchTxt.ToLower())
                || i.Brand.ToLower().Contains(searchTxt.ToLower())
                || i.Barcode.ToLower().Contains(searchTxt.ToLower())).ToList());               
        }

        private void AddItemModal()
        {
            InventoryAddView inventoryAddView = new InventoryAddView();
            inventoryAddView.ShowDialog();
        }

        private void EditItemModal(ItemModel item)
        {
            InventoryEditView inventoryEditView = new InventoryEditView(item);
            inventoryEditView.ShowDialog();
        }

        private void ItemHistoryModal()
        {
            InventoryHistoryView inventoryHistoryView = new InventoryHistoryView();
            inventoryHistoryView.ShowDialog();
        }

        public async Task<bool> AddItem(ItemModel item)
        {
            bool result = true;          
            if(!await _itemRepository.AddItem(item)){ result = false; }
            if(result && !await _itemHistoryRepository.AddHistory(item, "ADDED")) { result = false; }
            return result;
        }

        public async Task<bool> UpdateItem(ItemModel item, ItemModel oldItem)
        {
            bool result = true;           
            if (!await _itemRepository.UpdateItem(item)) { result = false; }
            if (result && !await _itemHistoryRepository.AddHistory(oldItem, "UPDATED")) { result = false; }
            return result;
        }

        public async Task<bool> DeleteItem(ItemModel item)
        {
            bool result = true;           
            if (!await _itemRepository.DeleteItem(item.Id)) { result = false; }
            if (result && !await _itemHistoryRepository.AddHistory(item, "DELETED")) { result = false; }
            return result;
        }       

        private async void UndoAction(ItemHistoryModel item)
        {
            if (MessageBox.Show("Continue Action?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (!await _itemHistoryRepository.UndoAction(item)) 
                {
                    CloseHistoryAction();
                }
            }
            
        }
    }
}
