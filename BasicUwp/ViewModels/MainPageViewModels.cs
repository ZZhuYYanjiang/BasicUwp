using BasicUwp.Models;
using BasicUwp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicUwp.ViewModels
{
    public class MainPageViewModels : ViewModelBase
    {
        /// <summary>
        /// 选择的联系人
        /// </summary>
        private Contact _selectContact;

        /// <summary>
        /// 联系人
        /// </summary>
        public Contact SelectContact
        {
            get => _selectContact;
            set => Set(nameof(_selectContact), ref _selectContact, value);
        }

        /// <summary>
        /// 联系人集合
        /// </summary>
        public ObservableCollection<Contact> ContactCollection
        {
            get;
            private set;
        }

        /// <summary>
        /// 刷新命令
        /// </summary>
        private RelayCommand _listCommand;

        /// <summary>
        /// 刷新命令
        /// </summary>
        public RelayCommand ListCommand =>
                    _listCommand ?? (_listCommand = new RelayCommand(async () =>
                    {
                        ContactCollection.Clear();
                        var contacts = await _contactService.ListAsync();
                        foreach (var contact in contacts)
                        {
                            ContactCollection.Add(contact);
                        }
                    }));

        /// <summary>
        /// 更新命令。
        /// </summary>
        private RelayCommand<Contact> _updateCommand;

        /// <summary>
        /// 更新命令。
        /// </summary>
        public RelayCommand<Contact> UpdateCommand =>
            _updateCommand ?? (_updateCommand =
                new RelayCommand<Contact>(async contact => {
                    await _contactService.UpdateAsync(contact);
                }));

        /// <summary>
        /// 联系人服务
        /// </summary>
        private IContactService _contactService;

        public MainPageViewModel(IContactService contactService)
        {
            this._contactService = contactService;
        }

        public MainPageViewModel():this(new ContactServices())
        {

        }
    }
}
