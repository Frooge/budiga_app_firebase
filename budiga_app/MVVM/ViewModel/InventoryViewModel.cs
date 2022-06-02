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

        private ItemModel _item;
        public ItemModel Item { get; set; }
        public ItemHistoryModel ItemHistory { get; set; }
        
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
        }

        private void Initialize()
        {
            _item = new ItemModel();
            Item = new ItemModel();
            AddItemModalCommand = new RelayCommand(param => AddItemModal());
            EditItemModalCommand = new RelayCommand(param => EditItemModal((ItemModel)param));
            SearchItemCommand = new RelayCommand(param => SearchItem((string)param));
            ItemHistoryModalCommand = new RelayCommand(param => ItemHistoryModal());
            UndoActionCommand = new RelayCommand(param => UndoAction((ItemHistoryModel)param));            
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
            InventoryHistoryView inventoryHistoryView = new InventoryHistoryView(this);
            inventoryHistoryView.ShowDialog();
        }

        public void GetAllItem()
        {
            try
            {
                FirestoreConn conn = FirestoreConn.GetInstance;
                Query query = conn.FirestoreDb.Collection("items").WhereEqualTo("IsDeleted", false);
                FirestoreChangeListener listener = query.Listen(snapshot =>
                {
                    _item.ItemRecords = new ObservableCollection<ItemModel>();
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        Dictionary<string, object> dict = documentSnapshot.ToDictionary();
                        _item.ItemRecords.Add(new ItemModel()
                        {
                            Id = dict["Id"].ToString(),
                            Barcode = dict["Barcode"].ToString(),
                            Name = dict["Name"].ToString(),
                            Brand = dict["Brand"].ToString(),
                            Price = Convert.ToDecimal(dict["Price"]),
                            Quantity = Convert.ToInt32(dict["Quantity"])
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
                FirestoreConn conn = FirestoreConn.GetInstance;
                Query query = conn.FirestoreDb.Collection("item_history");
                FirestoreChangeListener listener = query.Listen(snapshot =>
                {
                    ItemHistory.ItemHistoryRecords = new ObservableCollection<ItemHistoryModel>();
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        Dictionary<string, object> dict = documentSnapshot.ToDictionary();
                        ItemHistory.ItemHistoryRecords.Add(new ItemHistoryModel()
                        {
                            Id = dict["Id"].ToString(),
                            Barcode = dict["Barcode"].ToString(),
                            Name = dict["Name"].ToString(),
                            Brand = dict["Brand"].ToString(),
                            Price = Convert.ToDecimal(dict["Price"]),
                            Quantity = Convert.ToInt32(dict["Quantity"])
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void AddItem(ItemModel item)
        {
            try
            {
                FirestoreConn conn = FirestoreConn.GetInstance;
                DocumentReference docRef = conn.FirestoreDb.Collection("items").Document(item.Id);
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "Id", item.Id },
                    { "Barcode", item.Barcode },
                    { "Name", item.Name },
                    { "Brand", item.Brand },
                    { "Price", (double)item.Price },
                    { "Quantity", item.Quantity },
                    { "IsDeleted", item.IsDeleted },
                };
                await docRef.SetAsync(dict);
                MessageBox.Show("Successfully added item!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void UpdateItem(ItemModel item)
        {
            try
            {
                FirestoreConn conn = FirestoreConn.GetInstance;
                DocumentReference docRef = conn.FirestoreDb.Collection("items").Document(item.Id);
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "Id", item.Id },
                    { "Barcode", item.Barcode },
                    { "Name", item.Name },
                    { "Brand", item.Brand },
                    { "Price", (double)item.Price },
                    { "Quantity", item.Quantity },
                    { "IsDeleted", item.IsDeleted },
                };
                await docRef.SetAsync(dict);
                MessageBox.Show("Successfully updated item!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void DeleteItem(string id)
        {
            try
            {
                FirestoreConn conn = FirestoreConn.GetInstance;
                DocumentReference docRef = conn.FirestoreDb.Collection("items").Document(id);
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "IsDeleted", true }
                };
                await docRef.SetAsync(dict, SetOptions.MergeAll);
                MessageBox.Show("Successfully deleted item!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UndoAction(ItemHistoryModel item)
        {

        }
    }
}
