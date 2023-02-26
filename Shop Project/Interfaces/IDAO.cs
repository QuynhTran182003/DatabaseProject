using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Project.Interfaces
{
    public interface IDAO<T>
    {
        void GetById(int id, DataGridView dataview);
        void Save(T ele);
        void Update(int id, T newEle);
        void Delete(int id);
    }
}
