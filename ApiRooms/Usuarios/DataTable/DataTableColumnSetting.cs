using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRooms.Usuarios.DataTable
{
    [Serializable]
    public class DataTableColumnSetting
    {
        public string Name { get; set; }

        public DataTableSortDirectionEnum? SortDirection { get; set; }
    }
}