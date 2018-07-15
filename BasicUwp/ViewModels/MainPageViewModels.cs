using BasicUwp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicUwp.ViewModels
{
    public class MainPageViewModels:ViewmodelBase
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

        public RelayCommand ListCommand => _listCommand ?? (_listCommand = new RelayCommand(() => { /**/}));
    }
}
