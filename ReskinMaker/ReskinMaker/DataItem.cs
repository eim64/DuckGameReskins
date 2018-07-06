using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReskinMaker
{
    class ItemData
    {
        public string Name;
        public UserControl control;
        public string helpMessage = "";

        public bool required = false;


        public ItemData(string name)
        {
            Name = name;
        }


        public bool isValid(out string Message)
        {
            bool v = valid(out Message);
            return required ? v : true;
        }

        public virtual bool valid(out string Message)
        {
            Message = "";
            return true;
        }

        public virtual DataChunk getData()
        {
            return null;
        }

        public virtual void parseData(DataChunk data)
        {

        }

    }
}
