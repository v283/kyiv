using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using SQLite;

namespace kyiv.Models
{
    public class NoteModel
    {
        private int _id;
        private string _text;
        private string _name;

        [PrimaryKey, AutoIncrement]
        public int Id {
            get;
            set;
        }
        public string Text {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
    }
}
