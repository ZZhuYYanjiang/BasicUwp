using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BasicUwp.Models;
using Newtonsoft.Json;

namespace BasicUwp.Services
{
    /// <summary>
    /// 联系人服务
    /// </summary>
    public class ContactServices : IContactService
    {
        /******** 私有变量 ********/
        /// <summary>
        /// 服务端点
        /// </summary>
        private const string ServicePoint = @"http://localhost:59421/api/Contacts";
        /******** 公开属性 ********/

        /******** 继承方法 ********/
        /// <summary>
        /// 列出所有联系人
        /// </summary>
        /// <returns>所有联系人</returns>
        public async Task<IEnumerable<Contact>> ListAsync()
        {
            using (var Client = new HttpClient())
            {
                var json = await Client.GetStringAsync(ServicePoint);
                return JsonConvert.DeserializeObject<Contact[]>(json);            }
        }

        /// <summary>
        /// 更新所有联系人
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Contact contact)
        {
            using (var Client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(contact);
                await Client.PutAsync(ServicePoint + "/" + contact.Id,new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }
        /******** 公开方法 ********/

        /******** 私有方法 ********/
    }
}
